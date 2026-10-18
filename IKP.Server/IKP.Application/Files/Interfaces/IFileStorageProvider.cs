namespace IKP.Application.Files.Interfaces;

/// <summary>
/// Abstracts physical file storage from PostgreSQL metadata and the API layer.
/// </summary>
public interface IFileStorageProvider
{
    /// <summary>
    /// Gets the stable provider code.
    /// </summary>
    string ProviderCode { get; }

    /// <summary>
    /// Creates a new temporary write stream for an upload.
    /// </summary>
    ValueTask<Stream> CreateWriteStreamAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Opens a temporary object for sequential append.
    /// </summary>
    ValueTask<Stream> OpenAppendStreamAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Opens an existing physical object for asynchronous reading.
    /// </summary>
    ValueTask<Stream> OpenReadAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Moves a completed temporary object to its final immutable key.
    /// </summary>
    ValueTask MoveAsync(string sourceKey, string destinationKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a physical object when it is no longer referenced.
    /// </summary>
    ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks whether a physical object exists.
    /// </summary>
    ValueTask<bool> ExistsAsync(string key, CancellationToken cancellationToken = default);
}
