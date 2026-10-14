namespace IKP.Application.Common.Query.Enums;

/// <summary>Defines free-text search modes.</summary>
public enum UniversalSearchMode
{
    /// <summary>
    /// Searches anywhere in the field.
    /// </summary> 
    Contains,

    /// <summary>
    /// Matches the complete field value.
    /// </summary> 
    Exact,

    /// <summary>
    /// Matches from the beginning of the field.
    /// </summary> 
    StartsWith,

    /// <summary>
    /// Matches at the end of the field.
    /// </summary> 
    EndsWith,

    /// <summary>
    /// Matches values whose Levenshtein distance is within the configured threshold.
    /// </summary>
    Levenshtein
}
