namespace IKP.Api.Models.Query
{
    /// <summary>
    /// Defines common paging, searching and sorting parameters for API collection queries.
    /// </summary>
    public sealed class PagedQueryRequest
    {
        /// <summary>
        /// Gets or sets the one-based page number.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the maximum number of items per page.
        /// </summary>
        public int PageSize { get; set; } = 50;

        /// <summary>
        /// Gets or sets an optional free-text search value.
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        /// Gets or sets the requested resource-specific sort field.
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether sorting is descending.
        /// </summary>
        public bool SortDescending { get; set; }

        /// <summary>
        /// Normalizes and validates the requested page size.
        /// </summary>
        /// <returns>The normalized page size.</returns>
        public int GetPageSize() => PageSize < 1 ? 1 : Math.Min(PageSize, 250);

        /// <summary>
        /// Normalizes the requested page number.
        /// </summary>
        /// <returns>The normalized page number.</returns>
        public int GetPage() => Page < 1 ? 1 : Page;
    }
}