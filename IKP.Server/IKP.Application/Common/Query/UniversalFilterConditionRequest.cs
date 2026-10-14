using IKP.Application.Common.Query.Enums;

namespace IKP.Application.Common.Query;

/// <summary>
/// Defines one field filter.
/// </summary>
public sealed class UniversalFilterConditionRequest
{
    /// <summary>
    /// Gets or sets the field name.
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the comparison operator.
    /// </summary>
    public UniversalFilterOperator Operator { get; set; }

    /// <summary>
    /// Gets or sets the primary value.
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets a list of values used by IN and NOT IN.
    /// </summary>
    public List<string> Values { get; set; } = [];

    /// <summary>
    /// Gets or sets the second value used by BETWEEN.
    /// </summary>
    public string? ValueTo { get; set; }
}
