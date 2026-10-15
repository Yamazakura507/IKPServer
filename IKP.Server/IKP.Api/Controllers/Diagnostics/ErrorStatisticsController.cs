using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.Diagnostics.Statistics;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics;

/// <summary>Provides aggregated diagnostic error statistics.</summary>
[ApiController]
[Route("api/diagnostics/error-statistics")]
public sealed class ErrorStatisticsController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<ErrorStatistics, object?>>> Fields = new Dictionary<string, Expression<Func<ErrorStatistics, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id, 
        ["errorGroupId"] = x => x.ErrorGroupId, 
        ["totalOccurrences"] = x => x.TotalOccurrences, 
        ["uniqueUsers"] = x => x.UniqueUsers,
        ["affectedUserRatio"] = x => x.AffectedUserRatio, 
        ["occurrencesLastHour"] = x => x.OccurrencesLastHour, 
        ["occurrencesLastDay"] = x => x.OccurrencesLastDay,
        ["occurrencesLastWeek"] = x => x.OccurrencesLastWeek, 
        ["usersLastHour"] = x => x.UsersLastHour, 
        ["usersLastDay"] = x => x.UsersLastDay, 
        ["usersLastWeek"] = x => x.UsersLastWeek,
        ["growthRate"] = x => x.GrowthRate, 
        ["calculatedAt"] = x => x.CalculatedAt
    };

    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    /// <summary>
    /// Initializes a new controller.
    /// </summary>
    public ErrorStatisticsController(IDbContextFactory<AppDbContext> dbContextFactory) => this.dbContextFactory = dbContextFactory;
    
    /// <summary>
    /// Queries persisted error statistics.
    /// </summary>
    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.ErrorStatistics.AsNoTracking()
                            .ToPagedResponseAsync(request, Fields, x => 
                                new 
                                { 
                                    x.Id, 
                                    x.ErrorGroupId, 
                                    x.TotalOccurrences, 
                                    x.UniqueUsers, 
                                    x.AffectedUserRatio, 
                                    x.OccurrencesLastHour, 
                                    x.OccurrencesLastDay,
                                    x.OccurrencesLastWeek, 
                                    x.UsersLastHour, 
                                    x.UsersLastDay, 
                                    x.UsersLastWeek, 
                                    x.GrowthRate, 
                                    x.CalculatedAt 
                                }, cancellationToken));
    }
    
    /// <summary>
    /// Gets statistics for one error group.
    /// </summary>
    [HttpGet("by-group/{errorGroupId:guid}")]
    public async Task<IActionResult> GetByGroup(Guid errorGroupId, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        object? result = await db.ErrorStatistics.AsNoTracking()
            .Where(x => x.ErrorGroupId == errorGroupId)
            .Select(x => 
            new 
            { 
                x.Id, 
                x.ErrorGroupId, 
                x.TotalOccurrences, 
                x.UniqueUsers, 
                x.AffectedUserRatio, 
                x.OccurrencesLastHour,
                x.OccurrencesLastDay, 
                x.OccurrencesLastWeek, 
                x.UsersLastHour, 
                x.UsersLastDay, 
                x.UsersLastWeek, 
                x.GrowthRate, 
                x.CalculatedAt 
            }).SingleOrDefaultAsync(cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }
    /// <summary>
    /// Executes grouped aggregate statistics.
    /// </summary>
    [HttpPost("aggregate")]
    public async Task<IActionResult> Aggregate([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.ErrorStatistics.AsNoTracking().ToAggregateResponseAsync(request, Fields, cancellationToken));
    }
}
