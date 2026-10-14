using IKP.Api.Models.Query;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Infrastructure.Query
{
    /// <summary>
    /// Provides common asynchronous query operations used by API collection endpoints.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Executes a paged query without materializing the complete result set.
        /// </summary>
        /// <typeparam name="T">Projected item type.</typeparam>
        /// <param name="query">Query to execute.</param>
        /// <param name="request">Paging request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Paged query result.</returns>
        public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(
            this IQueryable<T> query,
            PagedQueryRequest request,
            CancellationToken cancellationToken = default)
        {
            int page = request.GetPage();
            int pageSize = request.GetPageSize();

            long totalCount = await query.LongCountAsync(cancellationToken);
            int totalPages = totalCount == 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);

            List<T> items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResponse<T>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Items = items
            };
        }
    }
}