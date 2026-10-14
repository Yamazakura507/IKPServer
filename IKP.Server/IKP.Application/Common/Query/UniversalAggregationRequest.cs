namespace IKP.Application.Common.Query;

/// <summary>
/// Defines a grouped universal query.
/// </summary>
public sealed class UniversalAggregationRequest
{
    /// <summary>
    /// Gets or sets the single field used as the grouping key.
    /// </summary>
    public string GroupBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets aggregate definitions.
    /// </summary>
    public List<UniversalAggregateRequest> Aggregates { get; set; } = [];
}
