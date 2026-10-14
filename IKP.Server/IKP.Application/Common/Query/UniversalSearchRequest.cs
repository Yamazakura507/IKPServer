using IKP.Application.Common.Query.Enums;

namespace IKP.Application.Common.Query;

/// <summary>
/// Defines free-text search behavior.
/// </summary>
public sealed class UniversalSearchRequest
{
    /// <summary>
    /// Gets or sets the text to search for.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the allowed searchable fields.
    /// </summary>
    public List<string> Fields { get; set; } = [];

    /// <summary>
    /// Gets or sets the search mode.
    /// </summary>
    public UniversalSearchMode Mode { get; set; } = UniversalSearchMode.Contains;

    /// <summary>
    /// Gets or sets whether comparison is case-sensitive.
    /// </summary>
    public bool CaseSensitive { get; set; }

    /// <summary>
    /// Gets or sets whether whitespace is ignored.
    /// </summary>
    public bool IgnoreWhitespace { get; set; }

    /// <summary>
    /// Gets or sets the maximum Levenshtein distance. Lower values are stricter.
    /// </summary>
    public int LevenshteinMaxDistance { get; set; } = 2;
}
