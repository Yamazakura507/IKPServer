using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Localization;

/// <summary>
/// Represents a language supported by the IKP client applications.
/// </summary>
public sealed class Language : FullLookupLocalized
{
    /// <summary>
    /// Gets or sets the BCP-47 culture code used by the client.
    /// </summary>
    public string CultureCode { get; set; } = null!;

    /// <summary>
    /// Gets or sets the language name in its native language.
    /// </summary>
    public string NativeName { get; set; } = null!;

    /// <summary>
    /// Gets or sets a value indicating whether the language is the server default language.
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the language is written right to left.
    /// </summary>
    public bool IsRightToLeft { get; set; }
}
