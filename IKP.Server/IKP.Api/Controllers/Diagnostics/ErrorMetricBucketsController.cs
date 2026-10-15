using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.Diagnostics.Statistics;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics;

/// <summary>
/// Provides diagnostic metric bucket read operations.
/// </summary>
[ApiController]
[Route("api/diagnostics/metric-buckets")]
public sealed class ErrorMetricBucketsController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<ErrorMetricBucket, object?>>> Fields = new Dictionary<string, Expression<Func<ErrorMetricBucket, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id, 
        ["errorGroupId"] = x => x.ErrorGroupId, 
        ["bucketStart"] = x => x.BucketStart, 
        ["bucketDurationMinutes"] = x => x.BucketDurationMinutes,
        ["bucketTypeId"] = x => x.BucketTypeId, 
        ["bucketTypeCode"] = x => x.BucketTypeDefinition.Code, 
        ["occurrences"] = x => x.Occurrences, 
        ["uniqueUsers"] = x => x.UniqueUsers,
        ["firstOccurrenceAt"] = x => x.FirstOccurrenceAt, 
        ["lastOccurrenceAt"] = x => x.LastOccurrenceAt, 
        ["calculatedAt"] = x => x.CalculatedAt
    };

    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    /// <summary>
    /// Initializes a new controller.
    /// </summary>
    public ErrorMetricBucketsController(IDbContextFactory<AppDbContext> dbContextFactory) => this.dbContextFactory = dbContextFactory;
    
    /// <summary>
    /// Queries metric buckets.
    /// </summary>
    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        return Ok(await db.ErrorMetricBuckets.AsNoTracking()
                            .ToPagedResponseAsync(request, Fields, x => 
                                new 
                                { 
                                    x.Id, 
                                    x.ErrorGroupId, 
                                    x.BucketStart, 
                                    x.BucketDurationMinutes, 
                                    x.BucketTypeId,
                                    BucketTypeCode = x.BucketTypeDefinition.Code, 
                                    x.Occurrences, 
                                    x.UniqueUsers, 
                                    x.FirstOccurrenceAt, 
                                    x.LastOccurrenceAt, 
                                    x.CalculatedAt 
                                }, cancellationToken));
    }

    /// <summary>
    /// Executes grouped aggregate statistics over metric buckets.
    /// </summary>
    [HttpPost("aggregate")]
    public async Task<IActionResult> Aggregate([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.ErrorMetricBuckets.AsNoTracking().ToAggregateResponseAsync(request, Fields, cancellationToken));
    }
}
