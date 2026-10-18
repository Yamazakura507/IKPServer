using System.Buffers;
using System.Security.Cryptography;
using IKP.Application.Files.Exceptions;
using IKP.Application.Files.Interfaces;
using IKP.Application.Files.Models;
using IKP.Domain.Entities.Files;
using IKP.Infrastructure.Persistence;
using IKP.Infrastructure.Services.Redis.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace IKP.Infrastructure.Services.Files;

/// <summary>
/// Implements metadata management and streaming file storage while keeping physical content outside PostgreSQL.
/// </summary>
public sealed class FileService : IFileService
{
    private readonly AppDbContext db;
    private readonly IFileStorageProvider storage;
    private readonly IRedisDistributedLockService distributedLock;
    private readonly FileStorageOptions options;

    /// <summary>
    /// Initializes the file service.
    /// </summary>
    public FileService(AppDbContext db, IFileStorageProvider storage, IRedisDistributedLockService distributedLock, IOptions<FileStorageOptions> options)
    {
        this.db = db;
        this.storage = storage;
        this.distributedLock = distributedLock;
        this.options = options.Value;
    }

    /// <inheritdoc />
    public async ValueTask<FileUploadSessionResponse> CreateUploadSessionAsync(CreateFileUploadRequest request, Guid userId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        ValidateRequest(request);

        if (request.ExpectedSize is < 0) throw new InvalidOperationException("ExpectedSize cannot be negative.");
        if (request.ExpectedSize > options.MaximumFileSizeBytes) throw new FileStorageException(413, "FILE_TOO_LARGE", "The file exceeds the configured maximum size.");

        DateTime now = DateTime.UtcNow;
        FileUploadSession session = new()
        {
            OriginalFileName = NormalizeFileName(request.FileName),
            ContentType = NormalizeContentType(request.ContentType),
            ExpectedSize = request.ExpectedSize,
            StorageKey = $"tmp/{Guid.NewGuid():N}.upload",
            BytesReceived = 0,
            LastActivityAt = now,
            ExpiresAt = now.AddMinutes(options.UploadSessionLifetimeMinutes),
            CreatedBy = userId
        };

        await using Stream stream = await storage.CreateWriteStreamAsync(session.StorageKey, cancellationToken);
        try
        {
            db.FileUploadSessions.Add(session);
            await db.SaveChangesAsync(cancellationToken);
            return ToUploadResponse(session);
        }
        catch
        {
            await storage.DeleteAsync(session.StorageKey, CancellationToken.None);
            throw;
        }
    }

    /// <inheritdoc />
    public async ValueTask<FileUploadSessionResponse> AppendUploadAsync(Guid uploadId, long offset, Stream content, long? contentLength, Guid userId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(content);

        if (offset < 0) throw new InvalidOperationException("The upload offset cannot be negative.");

        FileUploadSession session = await db.FileUploadSessions.FirstOrDefaultAsync(x => x.Id == uploadId && !x.IsCompleted, cancellationToken)
            ?? throw new FileStorageException(404, "UPLOAD_NOT_FOUND", "The upload session was not found.");

        if (session.ExpiresAt <= DateTime.UtcNow) throw new FileStorageException(410, "UPLOAD_EXPIRED", "The upload session has expired.");
        if (session.CreatedBy != userId) throw new FileStorageException(403, "UPLOAD_FORBIDDEN", "The upload session belongs to another user.");
        if (session.BytesReceived != offset) throw new FileStorageException(409, "UPLOAD_OFFSET_MISMATCH", $"The server expects offset {session.BytesReceived}.");
        if (contentLength is < 0) throw new InvalidOperationException("Content length cannot be negative.");
        if (contentLength.HasValue && session.BytesReceived + contentLength.Value > options.MaximumFileSizeBytes) throw new FileStorageException(413, "FILE_TOO_LARGE", "The file exceeds the configured maximum size.");
        if (session.ExpectedSize.HasValue && contentLength.HasValue && session.BytesReceived + contentLength.Value > session.ExpectedSize.Value) throw new FileStorageException(409, "UPLOAD_SIZE_EXCEEDED", "The upload exceeds the expected content length.");

        try
        {
            long copied;

            await using (Stream target = await storage.OpenAppendStreamAsync(session.StorageKey, cancellationToken))
            {
                copied = await CopyWithLimitAsync(content, target, options.MaximumFileSizeBytes - session.BytesReceived, cancellationToken);
            }

            session.BytesReceived += copied;
            session.LastActivityAt = DateTime.UtcNow;
            session.ExpiresAt = session.LastActivityAt.AddMinutes(options.UploadSessionLifetimeMinutes);

            if (session.ExpectedSize.HasValue && session.BytesReceived > session.ExpectedSize.Value)
                throw new FileStorageException(409, "UPLOAD_SIZE_EXCEEDED", "The upload exceeds the expected content length.");

            await db.SaveChangesAsync(cancellationToken);
        }
        catch (FileStorageException exception) when (exception.Code is "FILE_TOO_LARGE" or "UPLOAD_SIZE_EXCEEDED")
        {
            await storage.DeleteAsync(session.StorageKey, CancellationToken.None);

            await using Stream resetStream = await storage.CreateWriteStreamAsync(session.StorageKey, CancellationToken.None);

            session.BytesReceived = 0;
            session.LastActivityAt = DateTime.UtcNow;
            session.ExpiresAt = session.LastActivityAt.AddMinutes(options.UploadSessionLifetimeMinutes);

            await db.SaveChangesAsync(CancellationToken.None);

            throw;
        }

        return ToUploadResponse(session);
    }

    /// <inheritdoc />
    public async ValueTask<StoredFileResponse> CompleteUploadAsync(Guid uploadId, Guid userId, CancellationToken cancellationToken = default)
    {
        FileUploadSession session = await db.FileUploadSessions.FirstOrDefaultAsync(x => x.Id == uploadId && !x.IsCompleted, cancellationToken)
            ?? throw new FileStorageException(404, "UPLOAD_NOT_FOUND", "The upload session was not found.");

        if (session.ExpiresAt <= DateTime.UtcNow) throw new FileStorageException(410, "UPLOAD_EXPIRED", "The upload session has expired.");
        if (session.CreatedBy != userId) throw new FileStorageException(403, "UPLOAD_FORBIDDEN", "The upload session belongs to another user.");
        if (session.ExpectedSize.HasValue && session.BytesReceived != session.ExpectedSize.Value) throw new FileStorageException(409, "UPLOAD_INCOMPLETE", "The received byte count does not match the expected content length.");

        string contentHash;

        await using (Stream source = await storage.OpenReadAsync(session.StorageKey, cancellationToken))
        {
            contentHash = await ComputeSha256Async(source, cancellationToken);
        }

        await using IRedisDistributedLockHandle? redisLock = await distributedLock.TryAcquireAsync($"ikp-files-blob:{contentHash}:{session.BytesReceived}", TimeSpan.FromMinutes(10), cancellationToken);
        await using IDbContextTransaction transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        await db.Database.ExecuteSqlInterpolatedAsync($"SELECT pg_advisory_xact_lock(hashtextextended({contentHash}, 8));", cancellationToken);

        FileBlob? blob = await db.FileBlobs.FirstOrDefaultAsync(x => x.ContentHash == contentHash && x.Size == session.BytesReceived, cancellationToken);
        bool movedPhysicalObject = false;
        string finalKey = BuildFinalKey(contentHash);

        try
        {
            if (blob is null)
            {
                if (!await storage.ExistsAsync(finalKey, cancellationToken))
                {
                    await storage.MoveAsync(session.StorageKey, finalKey, cancellationToken);
                    movedPhysicalObject = true;
                }

                blob = new FileBlob
                {
                    ContentHash = contentHash,
                    Size = session.BytesReceived,
                    StorageProvider = storage.ProviderCode,
                    StorageKey = finalKey,
                    CreatedAt = DateTime.UtcNow
                };

                db.FileBlobs.Add(blob);
            }

            StoredFile file = new()
            {
                Blob = blob,
                OriginalFileName = session.OriginalFileName,
                ContentType = session.ContentType,
                Extension = GetExtension(session.OriginalFileName),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId
            };

            db.Set<StoredFile>().Add(file);
            db.FileUploadSessions.Remove(session);
            await db.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            if (!string.Equals(session.StorageKey, finalKey, StringComparison.Ordinal)) await storage.DeleteAsync(session.StorageKey, cancellationToken);

            return ToResponse(file, blob);
        }
        catch
        {
            await transaction.RollbackAsync(CancellationToken.None);

            if (movedPhysicalObject) await storage.DeleteAsync(finalKey, CancellationToken.None);

            throw;
        }
    }

    /// <inheritdoc />
    public async ValueTask<StoredFileResponse> UploadAsync(CreateFileUploadRequest request, Stream content, long? contentLength, Guid userId, CancellationToken cancellationToken = default)
    {
        CreateFileUploadRequest uploadRequest = new()
        {
            FileName = request.FileName,
            ContentType = request.ContentType,
            ExpectedSize = request.ExpectedSize ?? contentLength
        };

        FileUploadSessionResponse session = await CreateUploadSessionAsync(uploadRequest, userId, cancellationToken);

        try
        {
            await AppendUploadAsync(session.Id, 0, content, contentLength, userId, cancellationToken);

            return await CompleteUploadAsync(session.Id, userId, cancellationToken);
        }
        catch
        {
            await CleanupUploadSessionAsync(session.Id, cancellationToken);

            throw;
        }
    }

    /// <inheritdoc />
    public async ValueTask<StoredFileResponse?> GetAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        return await db.Set<StoredFile>().AsNoTracking().Where(x => x.Id == fileId).Select(x => new StoredFileResponse
        {
            Id = x.Id,
            BlobId = x.BlobId,
            OriginalFileName = x.OriginalFileName,
            ContentType = x.ContentType,
            Extension = x.Extension,
            Size = x.Blob.Size,
            ContentHash = x.Blob.ContentHash,
            CreatedAt = x.CreatedAt,
            CreatedBy = x.CreatedBy
        }).SingleOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<(StoredFileResponse Metadata, Stream Content)?> OpenReadAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        StoredFile? file = await db.Set<StoredFile>().AsNoTracking().Include(x => x.Blob).SingleOrDefaultAsync(x => x.Id == fileId, cancellationToken);
        
        if (file is null) return null;

        Stream content = await storage.OpenReadAsync(file.Blob.StorageKey, cancellationToken);

        return (ToResponse(file, file.Blob), content);
    }

    /// <inheritdoc />
    public async ValueTask DeleteAsync(Guid fileId, Guid userId, CancellationToken cancellationToken = default)
    {
        StoredFile? file = await db.Set<StoredFile>().FirstOrDefaultAsync(x => x.Id == fileId, cancellationToken);
        
        if (file is null) throw new FileStorageException(404, "FILE_NOT_FOUND", "The file was not found.");

        file.IsDeleted = true;
        file.DeletedAt = DateTime.UtcNow;
        file.DeletedBy = userId;

        await db.SaveChangesAsync(cancellationToken);
    }

    private async ValueTask CleanupUploadSessionAsync(Guid uploadId, CancellationToken cancellationToken)
    {
        FileUploadSession? session = await db.FileUploadSessions.IgnoreQueryFilters().SingleOrDefaultAsync(x => x.Id == uploadId, cancellationToken);
        
        if (session is null) return;

        await storage.DeleteAsync(session.StorageKey, CancellationToken.None);

        db.FileUploadSessions.Remove(session);

        await db.SaveChangesAsync(cancellationToken);
    }

    private async ValueTask<long> CopyWithLimitAsync(Stream source, Stream destination, long remainingLimit, CancellationToken cancellationToken)
    {
        byte[] buffer = ArrayPool<byte>.Shared.Rent(options.CopyBufferSizeBytes);
        long total = 0;

        try
        {
            while (true)
            {
                int read = await source.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken);
                if (read == 0) break;
                total += read;
                if (total > remainingLimit) throw new FileStorageException(413, "FILE_TOO_LARGE", "The file exceeds the configured maximum size.");
                await destination.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
            }

            await destination.FlushAsync(cancellationToken);

            return total;
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    private async ValueTask<string> ComputeSha256Async(Stream source, CancellationToken cancellationToken)
    {
        byte[] buffer = ArrayPool<byte>.Shared.Rent(options.CopyBufferSizeBytes);
        using IncrementalHash hash = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);

        try
        {
            while (true)
            {
                int read = await source.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken);

                if (read == 0) break;

                hash.AppendData(buffer, 0, read);
            }

            return Convert.ToHexString(hash.GetHashAndReset());
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    private static void ValidateRequest(CreateFileUploadRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FileName)) throw new FileStorageException(400, "FILE_NAME_REQUIRED", "A file name is required.");
        if (request.FileName.Length > 255) throw new FileStorageException(400, "FILE_NAME_TOO_LONG", "The file name exceeds 255 characters.");
    }

    private static string NormalizeFileName(string value)
    {
        string fileName = Path.GetFileName(value.Replace('\\', '/')).Trim();

        if (string.IsNullOrWhiteSpace(fileName) || fileName is "." or "..") throw new FileStorageException(400, "FILE_NAME_INVALID", "The file name is invalid.");
        
        return fileName;
    }

    private static string NormalizeContentType(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return "application/octet-stream";
        
        string contentType = value.Trim();

        if (contentType.Length > 255) throw new FileStorageException(400, "CONTENT_TYPE_TOO_LONG", "The content type exceeds 255 characters.");
        
        return contentType;
    }

    private static string? GetExtension(string fileName)
    {
        string extension = Path.GetExtension(fileName);
        
        if (string.IsNullOrEmpty(extension)) return null;
        
        extension = extension[1..].ToLowerInvariant();
        
        return extension.Length > 32 ? extension[..32] : extension;
    }

    private static string BuildFinalKey(string contentHash) => $"objects/{contentHash[..2]}/{contentHash[2..4]}/{contentHash}";

    private static FileUploadSessionResponse ToUploadResponse(FileUploadSession session) => new()
    {
        Id = session.Id,
        ExpectedSize = session.ExpectedSize,
        BytesReceived = session.BytesReceived,
        NextOffset = session.BytesReceived,
        ExpiresAt = session.ExpiresAt
    };

    private static StoredFileResponse ToResponse(StoredFile file, FileBlob blob) => new()
    {
        Id = file.Id,
        BlobId = blob.Id,
        OriginalFileName = file.OriginalFileName,
        ContentType = file.ContentType,
        Extension = file.Extension,
        Size = blob.Size,
        ContentHash = blob.ContentHash,
        CreatedAt = file.CreatedAt,
        CreatedBy = file.CreatedBy
    };
}
