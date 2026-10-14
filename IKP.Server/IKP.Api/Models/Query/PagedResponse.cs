namespace IKP.Api.Models.Query
{
    /// <summary>
    /// Represents a paged API collection.
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    public sealed class PagedResponse<T>
    {
        /// <summary>
        /// Gets the one-based page number.
        /// </summary>
        public int Page { get; init; }

        /// <summary>
        /// Gets the page size.
        /// </summary>
        public int PageSize { get; init; }

        /// <summary>
        /// Gets the total number of matching items.
        /// </summary>
        public long TotalCount { get; init; }

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public int TotalPages { get; init; }

        /// <summary>
        /// Gets the page items.
        /// </summary>
        public IReadOnlyList<T> Items { get; init; } = [];

        /// <summary>
        /// Gets a value indicating whether another page exists.
        /// </summary>
        public bool HasNextPage => Page < TotalPages;

        /// <summary>
        /// Gets a value indicating whether a previous page exists.
        /// </summary>
        public bool HasPreviousPage => Page > 1;
    }
}