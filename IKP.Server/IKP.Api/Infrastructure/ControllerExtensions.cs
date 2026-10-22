using IKP.Domain.Common.Base;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Infrastructure
{
    public static class ControllerExtensions
    {
        public async static ValueTask<object?> GetLookupList<T>(this DbSet<T> dbSet, CancellationToken cancellationToken) where T : FullLookupLocalized
        {
            return await dbSet.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.SortOrder)
            .Select(x => new { x.Id, x.Code, x.NameKey, x.DescriptionKey, x.FileId }).ToListAsync(cancellationToken);
        }
    }
}
