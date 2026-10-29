using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Audit;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.Files;

namespace IKP.Domain.Entities.Localization;

/// <summary>
/// Represents an immutable language package stored through the Files bounded context.
/// </summary>
public sealed class LanguagePackage : Entity, IAuditable, IHasSystemState, IHasActiveState
{
    /// <summary>
    /// Gets or sets the language identifier.
    /// </summary>
    public Guid LanguageId { get; set; }

    /// <summary>
    /// Gets or sets the package version.
    /// </summary>
    public string Version { get; set; } = null!;

    /// <summary>
    /// Gets or sets the stored file identifier containing the package.
    /// </summary>
    public Guid FileId { get; set; }

    /// <summary>
    /// Gets or sets the SHA-256 hash of the package content.
    /// </summary>
    public string ContentHash { get; set; } = null!;

    /// <summary>
    /// Gets or sets the package size in bytes.
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this package is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this package is system-defined.
    /// </summary>
    public bool IsSystem { get; set; }

    /// <summary>
    /// Gets or sets the UTC creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the creating user identifier.
    /// </summary>
    public Guid? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the UTC update timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the updating user identifier.
    /// </summary>
    public Guid? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the language navigation property.
    /// </summary>
    public Language Language { get; set; } = null!;
    public StoredFile File { get; set; } = null!;
}
