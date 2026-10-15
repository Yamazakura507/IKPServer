using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.Diagnostics.Errors;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics;

/// <summary>
/// Provides diagnostic error group read and aggregate operations.
/// </summary>
[ApiController]
[Route("api/diagnostics/error-groups")]
public sealed class ErrorGroupsController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<ErrorGroup, object?>>> Fields = new Dictionary<string, Expression<Func<ErrorGroup, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id, 
        ["fingerprint"] = x => x.Fingerprint,
        ["fingerprintVersion"] = x => x.FingerprintVersion, 
        ["applicationId"] = x => x.ApplicationId,
        ["exceptionType"] = x => x.ExceptionType, 
        ["errorCode"] = x => x.ErrorCode, 
        ["title"] = x => x.Title, 
        ["summary"] = x => x.Summary,
        ["severityId"] = x => x.SeverityId,
        ["severityCode"] = x => x.SeverityDefinition.Code, 
        ["statusId"] = x => x.StatusId, 
        ["statusCode"] = x => x.StatusDefinition.Code,
        ["firstOccurredAt"] = x => x.FirstOccurredAt, 
        ["lastOccurredAt"] = x => x.LastOccurredAt, 
        ["firstSeenVersion"] = x => x.FirstSeenVersion, 
        ["lastSeenVersion"] = x => x.LastSeenVersion,
        ["createdAt"] = x => x.CreatedAt, 
        ["updatedAt"] = x => x.UpdatedAt
    };
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    /// <summary>
    /// Initializes a new controller.
    /// </summary>
    public ErrorGroupsController(IDbContextFactory<AppDbContext> dbContextFactory) => this.dbContextFactory = dbContextFactory;
    
    /// <summary>
    /// Queries error groups.
    /// </summary>
    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.ErrorGroups.AsNoTracking()
                            .ToPagedResponseAsync(request, Fields, x => 
                                new
                                {
                                    x.Id, 
                                    x.Fingerprint, 
                                    x.FingerprintVersion, 
                                    x.ApplicationId, 
                                    x.DiagnosticAreaResolutionId, 
                                    x.ExceptionType, 
                                    x.ErrorCode, 
                                    x.Title, 
                                    x.Summary,
                                    x.SeverityId, 
                                    SeverityCode = x.SeverityDefinition.Code, 
                                    x.StatusId, 
                                    StatusCode = x.StatusDefinition.Code, 
                                    x.FirstOccurredAt, 
                                    x.LastOccurredAt,
                                    x.FirstSeenVersion, 
                                    x.LastSeenVersion, 
                                    x.CreatedAt, 
                                    x.UpdatedAt, 
                                    CriticalityScore = x.Criticality == null ? (decimal?)null : x.Criticality.EffectiveScore,
                                    TotalOccurrences = x.Statistics == null ? (long?)null : x.Statistics.TotalOccurrences
                                }, cancellationToken));
    }
    
    /// <summary>
    /// Gets one error group with current statistics and criticality.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        object? result = await db.ErrorGroups.AsNoTracking()
                                .Where(x => x.Id == id)
                                .Select(x => 
                                    new
                                    {
                                        x.Id, 
                                        x.Fingerprint, 
                                        x.FingerprintVersion, 
                                        x.ApplicationId, 
                                        x.DiagnosticAreaResolutionId, 
                                        x.ExceptionType, 
                                        x.ErrorCode, 
                                        x.Title, 
                                        x.Summary,
                                        x.SeverityId, 
                                        SeverityCode = x.SeverityDefinition.Code, 
                                        x.StatusId, 
                                        StatusCode = x.StatusDefinition.Code, 
                                        x.FirstOccurredAt, 
                                        x.LastOccurredAt,
                                        x.FirstSeenVersion, 
                                        x.LastSeenVersion, 
                                        x.CreatedAt, 
                                        x.UpdatedAt,
                                        Criticality = x.Criticality == null 
                                                        ? null 
                                                        : new 
                                                            {
                                                                x.Criticality.AutomaticScore, 
                                                                x.Criticality.ManualScore, 
                                                                x.Criticality.EffectiveScore, 
                                                                x.Criticality.CalculationVersion, 
                                                                x.Criticality.CalculatedAt 
                                                            },
                                        Statistics = x.Statistics == null 
                                                        ? null 
                                                        : new 
                                                            { 
                                                                x.Statistics.TotalOccurrences, 
                                                                x.Statistics.UniqueUsers, 
                                                                x.Statistics.AffectedUserRatio, 
                                                                x.Statistics.OccurrencesLastHour, 
                                                                x.Statistics.OccurrencesLastDay,
                                                                x.Statistics.OccurrencesLastWeek, 
                                                                x.Statistics.UsersLastHour, 
                                                                x.Statistics.UsersLastDay, 
                                                                x.Statistics.UsersLastWeek, 
                                                                x.Statistics.GrowthRate, 
                                                                x.Statistics.CalculatedAt 
                                                            }
                                    })
                                .SingleOrDefaultAsync(cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Executes a grouped aggregate query over error groups.
    /// </summary>
    [HttpPost("aggregate")]
    public async Task<IActionResult> Aggregate([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.ErrorGroups.AsNoTracking().ToAggregateResponseAsync(request, Fields, cancellationToken));
    }
}
