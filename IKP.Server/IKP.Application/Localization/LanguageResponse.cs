namespace IKP.Api.Models.Localization;

/// <summary>
/// Represents a language returned to a client.
/// </summary>
public sealed class LanguageResponse
{
    public Guid Id { get; init; }
    public string Code { get; init; } = null!;
    public string CultureCode { get; init; } = null!;
    public string NameKey { get; init; } = null!;
    public string? DescriptionKey { get; init; }
    public string NativeName { get; init; } = null!;
    public Guid? FileId { get; init; }
    public int SortOrder { get; init; }
    public bool IsActive { get; init; }
    public bool IsSystem { get; init; }
    public bool IsDefault { get; init; }
    public bool IsRightToLeft { get; init; }
    public string? ActivePackageVersion { get; init; }
}
