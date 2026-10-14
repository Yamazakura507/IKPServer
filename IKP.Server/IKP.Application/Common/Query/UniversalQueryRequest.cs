using IKP.Application.Common.Query.Enums;

namespace IKP.Application.Common.Query;

/// <summary>
/// Defines a reusable query contract for pageable API lists.
/// </summary>
public sealed class UniversalQueryRequest
{
    /// <summary>
    /// Gets or sets the one-based page number.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the requested page size.
    /// </summary>
    public int PageSize { get; set; } = 50;

    /// <summary>
    /// Gets or sets the free-text search request.
    /// </summary>
    public UniversalSearchRequest? Search { get; set; }

    /// <summary>
    /// Gets or sets the filter expression.
    /// </summary>
    public UniversalFilterGroupRequest? Filter { get; set; }

    /// <summary>
    /// Gets or sets the ordered sort definitions.
    /// </summary>
    public List<UniversalSortRequest> Sorts { get; set; } = [];

    /// <summary>
    /// Gets or sets optional grouping and aggregate definitions.
    /// </summary>
    public UniversalAggregationRequest? Aggregation { get; set; }
}
