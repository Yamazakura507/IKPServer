namespace IKP.Application.Common.Query.Enums;

/// <summary>
/// Defines the logical operators supported by universal filters.
/// </summary>
public enum UniversalFilterLogic
{
    /// <summary>
    /// All expressions must match.
    /// </summary>
    And = 0,
    /// <summary>
    /// At least one expression must match.
    /// </summary>
    Or = 1
}
