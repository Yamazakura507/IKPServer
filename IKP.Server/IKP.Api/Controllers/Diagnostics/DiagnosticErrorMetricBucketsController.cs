using IKP.Api.Diagnostics.Models.Statistics;
using IKP.Api.Infrastructure.Query;
using IKP.Api.Models.Query;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics
{
    /// <summary>
    /// Provides read-only querying of persisted diagnostic time-series buckets.
    /// </summary>
    [ApiController]
    [Route("api/diagnostics/error-metric-buckets")]
    public sealed class DiagnosticErrorMetricBucketsController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        public DiagnosticErrorMetricBucketsController(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<DiagnosticErrorMetricBucketResponse>>> Get(
            [FromQuery] PagedQueryRequest request,
            Guid? errorGroupId,
            Guid? bucketTypeId,
            DateTime? from,
            DateTime? to,
            CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            IQueryable<Domain.Entities.Diagnostics.Statistics.ErrorMetricBucket> query = db.ErrorMetricBuckets.AsNoTracking();

            if (errorGroupId.HasValue) query = query.Where(x => x.ErrorGroupId == errorGroupId.Value);
            if (bucketTypeId.HasValue) query = query.Where(x => x.BucketTypeId == bucketTypeId.Value);
            if (from.HasValue) query = query.Where(x => x.BucketStart >= from.Value);
            if (to.HasValue) query = query.Where(x => x.BucketStart <= to.Value);

            IQueryable<DiagnosticErrorMetricBucketResponse> projected = query.Select(x => new DiagnosticErrorMetricBucketResponse
            {
                Id = x.Id,
                ErrorGroupId = x.ErrorGroupId,
                BucketStart = x.BucketStart,
                BucketDurationMinutes = x.BucketDurationMinutes,
                BucketTypeId = x.BucketTypeId,
                BucketTypeCode = x.BucketTypeDefinition.Code,
                Occurrences = x.Occurrences,
                UniqueUsers = x.UniqueUsers,
                FirstOccurrenceAt = x.FirstOccurrenceAt,
                LastOccurrenceAt = x.LastOccurrenceAt,
                CalculatedAt = x.CalculatedAt
            });

            projected = request.SortDescending
                ? projected.OrderByDescending(x => x.BucketStart)
                : projected.OrderBy(x => x.BucketStart);

            return Ok(await projected.ToPagedResponseAsync(request, cancellationToken));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DiagnosticErrorMetricBucketResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            DiagnosticErrorMetricBucketResponse? item = await db.ErrorMetricBuckets.AsNoTracking().Where(x => x.Id == id).Select(x => new DiagnosticErrorMetricBucketResponse
            {
                Id = x.Id, ErrorGroupId = x.ErrorGroupId, BucketStart = x.BucketStart, BucketDurationMinutes = x.BucketDurationMinutes,
                BucketTypeId = x.BucketTypeId, BucketTypeCode = x.BucketTypeDefinition.Code, Occurrences = x.Occurrences,
                UniqueUsers = x.UniqueUsers, FirstOccurrenceAt = x.FirstOccurrenceAt, LastOccurrenceAt = x.LastOccurrenceAt, CalculatedAt = x.CalculatedAt
            }).SingleOrDefaultAsync(cancellationToken);

            return item is null ? NotFound() : Ok(item);
        }
    }
}