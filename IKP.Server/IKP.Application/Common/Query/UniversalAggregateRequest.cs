using IKP.Application.Common.Query.Enums;

namespace IKP.Application.Common.Query;

/// <summary>
/// Defines one grouped aggregate.
/// </summary>
public sealed class UniversalAggregateRequest
{
    /// <summary>
    /// Gets or sets the response field alias.
    /// </summary>
    public string Alias { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the aggregate function.
    /// </summary>
    public UniversalAggregateFunction Function { get; set; }

    /// <summary>
    /// Gets or sets the source field. It is not required for Count.
    /// </summary>
    public string? Field { get; set; }
}
