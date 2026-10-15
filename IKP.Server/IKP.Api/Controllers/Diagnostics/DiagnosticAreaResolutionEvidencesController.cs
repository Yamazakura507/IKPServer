using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics;

/// <summary>
/// Provides diagnostic area resolution evidence read operations.
/// </summary>
[ApiController]
[Route("api/diagnostics/area-resolution-evidences")]
public sealed class DiagnosticAreaResolutionEvidencesController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<DiagnosticAreaResolutionEvidence, object?>>> Fields = new Dictionary<string, Expression<Func<DiagnosticAreaResolutionEvidence, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id, 
        ["diagnosticAreaResolutionId"] = x => x.DiagnosticAreaResolutionId, 
        ["mappingId"] = x => x.MappingId,
        ["ruleId"] = x => x.RuleId, 
        ["ruleCode"] = x => x.Rule.Code, 
        ["pattern"] = x => x.Pattern, 
        ["matchedValue"] = x => x.MatchedValue,
        ["priority"] = x => x.Priority, 
        ["confidence"] = x => x.Confidence, 
        ["contribution"] = x => x.Contribution
    };
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    /// <summary>
    /// Initializes a new controller.
    /// </summary>
    public DiagnosticAreaResolutionEvidencesController(IDbContextFactory<AppDbContext> dbContextFactory) => this.dbContextFactory = dbContextFactory;
    
    /// <summary>
    /// Queries resolution evidence.
    /// </summary>
    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.DiagnosticAreaResolutionEvidences.AsNoTracking()
                            .ToPagedResponseAsync(request, Fields, x => 
                                new 
                                { 
                                    x.Id, 
                                    x.DiagnosticAreaResolutionId, 
                                    x.MappingId, 
                                    x.RuleId, 
                                    RuleCode = x.Rule.Code, 
                                    x.Pattern, 
                                    x.MatchedValue, 
                                    x.Priority, 
                                    x.Confidence, 
                                    x.Contribution 
                                }, cancellationToken));
    }

    /// <summary>
    /// Gets one resolution evidence record.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        object? result = await db.DiagnosticAreaResolutionEvidences.AsNoTracking()
                                .Where(x => x.Id == id)
                                .Select(x => 
                                    new { 
                                        x.Id, 
                                        x.DiagnosticAreaResolutionId, 
                                        x.MappingId, 
                                        x.RuleId, 
                                        RuleCode = x.Rule.Code, 
                                        x.Pattern,
                                        x.MatchedValue, 
                                        x.Priority, 
                                        x.Confidence, 
                                        x.Contribution 
                                    })
                                .SingleOrDefaultAsync(cancellationToken);
        
        return result is null ? NotFound() : Ok(result);
    }
}
