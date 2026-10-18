using IKP.Application.Files.Models;

namespace IKP.Application.Files.Interfaces;

/// <summary>
/// Provides logical file management and streaming upload/download operations.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Creates an upload session.
    /// </summary>
    ValueTask<FileUploadSessionResponse> CreateUploadSessionAsync(CreateFileUploadRequest request, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Appends one sequential chunk to an upload session.
    /// </summary>
    ValueTask<FileUploadSessionResponse> AppendUploadAsync(Guid uploadId, long offset, Stream content, long? contentLength, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finalizes an upload session and creates the logical file record.
    /// </summary>
    ValueTask<StoredFileResponse> CompleteUploadAsync(Guid uploadId, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads one complete request body without requiring the caller to manage a session.
    /// </summary>
    ValueTask<StoredFileResponse> UploadAsync(CreateFileUploadRequest request, Stream content, long? contentLength, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a logical file by identifier.
    /// </summary>
    ValueTask<StoredFileResponse?> GetAsync(Guid fileId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Opens a file content stream for download.
    /// </summary>
    ValueTask<(StoredFileResponse Metadata, Stream Content)?> OpenReadAsync(Guid fileId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft-deletes a logical file.
    /// </summary>
    ValueTask DeleteAsync(Guid fileId, Guid userId, CancellationToken cancellationToken = default);
}
