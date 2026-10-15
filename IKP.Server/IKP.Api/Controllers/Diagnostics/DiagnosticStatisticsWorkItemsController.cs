using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.Diagnostics.Statistics;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics;

/// <summary>
/// Provides diagnostic statistics work item monitoring operations.
/// </summary>
[ApiController]
[Route("api/diagnostics/statistics-work-items")]
public sealed class DiagnosticStatisticsWorkItemsController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<DiagnosticStatisticsWorkItem, object?>>> Fields = new Dictionary<string, Expression<Func<DiagnosticStatisticsWorkItem, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id, 
        ["errorGroupId"] = x => x.ErrorGroupId, 
        ["requestedAt"] = x => x.RequestedAt, 
        ["availableAt"] = x => x.AvailableAt,
        ["startedAt"] = x => x.StartedAt, 
        ["completedAt"] = x => x.CompletedAt, 
        ["attemptCount"] = x => x.AttemptCount, 
        ["statusId"] = x => x.StatusId,
        ["statusCode"] = x => x.Status.Code, 
        ["lastError"] = x => x.LastError
    };

    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    /// <summary>
    /// Initializes a new controller.
    /// </summary>
    public DiagnosticStatisticsWorkItemsController(IDbContextFactory<AppDbContext> dbContextFactory) => this.dbContextFactory = dbContextFactory;
    
    /// <summary>
    /// Queries statistics work items.
    /// </summary>
    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.DiagnosticStatisticsWorkItems.AsNoTracking()
                            .ToPagedResponseAsync(request, Fields, x => 
                                new 
                                { 
                                    x.Id, 
                                    x.ErrorGroupId, 
                                    x.RequestedAt, 
                                    x.AvailableAt, 
                                    x.StartedAt, 
                                    x.CompletedAt, 
                                    x.AttemptCount, 
                                    x.StatusId,
                                    StatusCode = x.Status.Code, 
                                    x.LastError 
                                }, cancellationToken));
    }
    
    /// <summary>
    /// Gets one work item.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        object? result = await db.DiagnosticStatisticsWorkItems.AsNoTracking()
                            .Where(x => x.Id == id)
                            .Select(x => 
                                new 
                                { 
                                    x.Id, 
                                    x.ErrorGroupId, 
                                    x.RequestedAt, 
                                    x.AvailableAt, 
                                    x.StartedAt, 
                                    x.CompletedAt,
                                    x.AttemptCount, 
                                    x.StatusId,
                                    StatusCode = x.Status.Code, 
                                    x.LastError 
                                })
                            .SingleOrDefaultAsync(cancellationToken);
        
        return result is null ? NotFound() : Ok(result);
    }
}
