using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Files;

/// <summary>
/// Represents immutable physical file content stored by a file storage provider.
/// </summary>
public sealed class FileBlob : Entity
{
    /// <summary>
    /// Gets or sets the SHA-256 content hash in uppercase hexadecimal form.
    /// </summary>
    public string ContentHash { get; set; } = null!;

    /// <summary>
    /// Gets or sets the exact content length in bytes.
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// Gets or sets the storage provider code that owns the physical object.
    /// </summary>
    public string StorageProvider { get; set; } = null!;

    /// <summary>
    /// Gets or sets the provider-specific immutable object key.
    /// </summary>
    public string StorageKey { get; set; } = null!;

    /// <summary>
    /// Gets or sets the UTC creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets files that reference this physical content.
    /// </summary>
    public ICollection<StoredFile> Files { get; set; } = new List<StoredFile>();
}
