using IKP.Application.Files.Interfaces;
using IKP.Application.Files.Models;
using Microsoft.Extensions.Options;

namespace IKP.Infrastructure.Services.Files;

/// <summary>
/// Stores physical files on a local filesystem root using immutable content-addressed keys.
/// The same abstraction can later be implemented for NAS, SAN, S3, or MinIO without changing metadata or API contracts.
/// </summary>
public sealed class LocalFileStorageProvider : IFileStorageProvider
{
    private readonly FileStorageOptions options;
    private readonly string rootPath;

    /// <summary>
    /// Initializes the local storage provider.
    /// </summary>
    public LocalFileStorageProvider(IOptions<FileStorageOptions> options)
    {
        this.options = options.Value;
        rootPath = Path.GetFullPath(this.options.RootPath);

        Directory.CreateDirectory(rootPath);
        Directory.CreateDirectory(Path.Combine(rootPath, "tmp"));
        Directory.CreateDirectory(Path.Combine(rootPath, "objects"));
    }

    /// <inheritdoc />
    public string ProviderCode => options.ProviderCode;

    /// <inheritdoc />
    public ValueTask<Stream> CreateWriteStreamAsync(string key, CancellationToken cancellationToken = default)
    {
        string path = GetPath(key);

        EnsureParentDirectory(path);
        Stream stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None, options.CopyBufferSizeBytes, FileOptions.Asynchronous | FileOptions.SequentialScan);
        
        return ValueTask.FromResult(stream);
    }

    /// <inheritdoc />
    public ValueTask<Stream> OpenAppendStreamAsync(string key, CancellationToken cancellationToken = default)
    {
        string path = GetPath(key);

        EnsureParentDirectory(path);
        Stream stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read, options.CopyBufferSizeBytes, FileOptions.Asynchronous | FileOptions.SequentialScan);
        
        return ValueTask.FromResult(stream);
    }

    /// <inheritdoc />
    public ValueTask<Stream> OpenReadAsync(string key, CancellationToken cancellationToken = default)
    {
        string path = GetPath(key);
        Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, options.CopyBufferSizeBytes, FileOptions.Asynchronous | FileOptions.SequentialScan);
        
        return ValueTask.FromResult(stream);
    }

    /// <inheritdoc />
    public ValueTask MoveAsync(string sourceKey, string destinationKey, CancellationToken cancellationToken = default)
    {
        string sourcePath = GetPath(sourceKey);
        string destinationPath = GetPath(destinationKey);

        EnsureParentDirectory(destinationPath);
        File.Move(sourcePath, destinationPath);

        return ValueTask.CompletedTask;
    }

    /// <inheritdoc />
    public ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        string path = GetPath(key);

        if (File.Exists(path)) File.Delete(path);

        return ValueTask.CompletedTask;
    }

    /// <inheritdoc />
    public ValueTask<bool> ExistsAsync(string key, CancellationToken cancellationToken = default) => ValueTask.FromResult(File.Exists(GetPath(key)));

    private string GetPath(string key)
    {
        if (string.IsNullOrWhiteSpace(key) || key.Contains('\0') || Path.IsPathRooted(key)) throw new ArgumentException("The storage key is invalid.", nameof(key));

        string normalized = key.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
        string fullPath = Path.GetFullPath(Path.Combine(rootPath, normalized));
        string rootPrefix = rootPath.EndsWith(Path.DirectorySeparatorChar) ? rootPath : rootPath + Path.DirectorySeparatorChar;

        if (!fullPath.StartsWith(rootPrefix, StringComparison.Ordinal)) throw new ArgumentException("The storage key escapes the configured storage root.", nameof(key));

        return fullPath;
    }

    private static void EnsureParentDirectory(string path)
    {
        string? directory = Path.GetDirectoryName(path);

        if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);
    }
}
