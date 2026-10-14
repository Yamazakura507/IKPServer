namespace IKP.Application.Common.Query;

/// <summary>
/// Represents one row of a grouped universal aggregate result.
/// </summary>
public sealed class UniversalAggregateResponse
{
    /// <summary>
    /// Gets the grouping field.
    /// </summary>
    public string GroupBy { get; init; } = string.Empty;

    /// <summary>
    /// Gets the aggregate aliases.
    /// </summary>
    public IReadOnlyList<string> AggregateAliases { get; init; } = [];

    /// <summary>
    /// Gets the grouped rows.
    /// </summary>
    public IReadOnlyList<UniversalAggregateRow> Rows { get; init; } = [];
}

/// <summary>
/// Represents one grouped row.
/// </summary>
public sealed class UniversalAggregateRow
{
    /// <summary>
    /// Gets the grouping value.
    /// </summary>
    public object? GroupValue { get; init; }

    /// <summary>
    /// Gets aggregate values by alias.
    /// </summary>
    public IReadOnlyDictionary<string, decimal?> Values { get; init; } = new Dictionary<string, decimal?>();
}
