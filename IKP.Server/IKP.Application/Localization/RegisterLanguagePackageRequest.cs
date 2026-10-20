namespace IKP.Api.Models.Localization;

/// <summary>
/// Registers a previously uploaded language package file.
/// </summary>
public sealed class RegisterLanguagePackageRequest
{
    /// <summary>
    /// Gets or sets the package version.
    /// </summary>
    public string Version { get; set; } = null!;

    /// <summary>
    /// Gets or sets the uploaded package file identifier.
    /// </summary>
    public Guid FileId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the package is system-defined.
    /// </summary>
    public bool IsSystem { get; set; }
}
