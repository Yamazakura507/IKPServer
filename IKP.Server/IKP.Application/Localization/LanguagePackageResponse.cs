namespace IKP.Api.Models.Localization;

/// <summary>
/// Represents language package metadata.
/// </summary>
public sealed class LanguagePackageResponse
{
    public Guid Id { get; init; }
    public Guid LanguageId { get; init; }
    public string LanguageCode { get; init; } = null!;
    public string Version { get; init; } = null!;
    public Guid FileId { get; init; }
    public string ContentHash { get; init; } = null!;
    public long Size { get; init; }
    public bool IsActive { get; init; }
    public bool IsSystem { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid? CreatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public Guid? UpdatedBy { get; init; }
}
