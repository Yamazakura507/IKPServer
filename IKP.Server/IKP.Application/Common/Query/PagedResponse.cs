namespace IKP.Application.Common.Query;

/// <summary>
/// Represents one page of query results.
/// </summary>
public sealed class PagedResponse<T>
{
    /// <summary>
    /// Gets the result items.
    /// </summary>
    public IReadOnlyList<T> Items { get; init; } = [];

    /// <summary>
    /// Gets the one-based page number.
    /// </summary>
    public int Page { get; init; }

    /// <summary>
    /// Gets the page size.
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// Gets the total number of matching records.
    /// </summary>
    public long TotalCount { get; init; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; init; }
}
