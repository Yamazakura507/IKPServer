using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics;

/// <summary>
/// Provides diagnostic area resolution read operations.
/// </summary>
[ApiController]
[Route("api/diagnostics/area-resolutions")]
public sealed class DiagnosticAreaResolutionsController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<DiagnosticAreaResolution, object?>>> Fields = new Dictionary<string, Expression<Func<DiagnosticAreaResolution, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id, 
        ["diagnosticAreaId"] = x => x.DiagnosticAreaId, 
        ["diagnosticAreaCode"] = x => x.DiagnosticArea!.Code,
        ["sourceId"] = x => x.SourceId, 
        ["sourceCode"] = x => x.Source!.Code, 
        ["ruleId"] = x => x.RuleId, 
        ["ruleCode"] = x => x.Rule!.Code,
        ["confidence"] = x => x.Confidence, 
        ["version"] = x => x.Version
    };

    private readonly IDbContextFactory<AppDbContext> dbContextFactory;
    /// <summary>
    /// Initializes a new controller.
    /// </summary>
    public DiagnosticAreaResolutionsController(IDbContextFactory<AppDbContext> dbContextFactory) => this.dbContextFactory = dbContextFactory;
    
    /// <summary>
    /// Queries area resolutions.
    /// </summary>
    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.DiagnosticAreaResolutions.AsNoTracking()
                            .ToPagedResponseAsync(request, Fields, x => 
                                new 
                                { 
                                    x.Id, 
                                    x.DiagnosticAreaId, 
                                    DiagnosticAreaCode = x.DiagnosticArea!.Code, 
                                    x.SourceId, 
                                    SourceCode = x.Source!.Code,
                                    x.RuleId, 
                                    RuleCode = x.Rule!.Code, 
                                    x.Confidence, 
                                    x.Version 
                                }, cancellationToken));
    }
    
    /// <summary>
    /// Gets one area resolution.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        object? result = await db.DiagnosticAreaResolutions.AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => 
            new 
            { 
                x.Id, 
                x.DiagnosticAreaId, 
                DiagnosticAreaCode = x.DiagnosticArea!.Code, 
                x.SourceId, 
                SourceCode = x.Source!.Code,
                x.RuleId, 
                RuleCode = x.Rule!.Code, 
                x.Confidence, 
                x.Version })
            .SingleOrDefaultAsync(cancellationToken);
        
        return result is null ? NotFound() : Ok(result);
    }
}
