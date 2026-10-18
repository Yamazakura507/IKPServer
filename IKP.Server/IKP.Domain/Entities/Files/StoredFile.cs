using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Audit;

namespace IKP.Domain.Entities.Files;

/// <summary>
/// Represents a logical file record that references immutable physical content.
/// </summary>
public sealed class StoredFile : Entity, IAuditable, ISoftDelete
{
    /// <summary>
    /// Gets or sets the referenced physical content identifier.
    /// </summary>
    public Guid BlobId { get; set; }

    /// <summary>
    /// Gets or sets the original client-provided file name.
    /// </summary>
    public string OriginalFileName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the normalized media type used when the file is downloaded.
    /// </summary>
    public string ContentType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the normalized file extension without the leading dot.
    /// </summary>
    public string? Extension { get; set; }

    /// <summary>
    /// Gets or sets the UTC creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the user that created the logical file record.
    /// </summary>
    public Guid? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the UTC last update timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the user that last updated the logical file record.
    /// </summary>
    public Guid? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets whether the logical file record is soft-deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the UTC deletion timestamp.
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Gets or sets the user that deleted the logical file record.
    /// </summary>
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// Gets the immutable physical content.
    /// </summary>
    public FileBlob Blob { get; set; } = null!;
}
