namespace IKP.Application.Files.Models;

/// <summary>
/// Describes a stored logical file and its immutable content metadata.
/// </summary>
public sealed class StoredFileResponse
{
    /// <summary>
    /// Gets the logical file identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the physical blob identifier.
    /// </summary>
    public Guid BlobId { get; init; }

    /// <summary>
    /// Gets the original file name.
    /// </summary>
    public string OriginalFileName { get; init; } = null!;

    /// <summary>
    /// Gets the content type.
    /// </summary>
    public string ContentType { get; init; } = null!;

    /// <summary>
    /// Gets the normalized extension without a leading dot.
    /// </summary>
    public string? Extension { get; init; }

    /// <summary>
    /// Gets the file size in bytes.
    /// </summary>
    public long Size { get; init; }

    /// <summary>
    /// Gets the SHA-256 content hash.
    /// </summary>
    public string ContentHash { get; init; } = null!;

    /// <summary>
    /// Gets the UTC creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets the creator user identifier.
    /// </summary>
    public Guid? CreatedBy { get; init; }
}
