using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics.Lookup;

/// <summary>
/// Provides diagnostic area resolution rule lookup operations.
/// </summary>
[ApiController]
[Route("api/diagnostics/lookups/area-resolution-rules")]
public sealed class DiagnosticAreaResolutionRulesController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<DiagnosticAreaResolutionRule, object?>>> Fields = new Dictionary<string, Expression<Func<DiagnosticAreaResolutionRule, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id, 
        ["code"] = x => x.Code, 
        ["nameKey"] = x => x.NameKey, 
        ["descriptionKey"] = x => x.DescriptionKey, 
        ["isActive"] = x => x.IsActive,
        ["isSystem"] = x => x.IsSystem, 
        ["sortOrder"] = x => x.SortOrder
    };

    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    /// <summary>
    /// Initializes a new controller.
    /// </summary>
    public DiagnosticAreaResolutionRulesController(IDbContextFactory<AppDbContext> dbContextFactory) => this.dbContextFactory = dbContextFactory;
    
    /// <summary>
    /// Queries resolution rules.
    /// </summary>
    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.DiagnosticAreaResolutionRules.AsNoTracking()
                            .ToPagedResponseAsync(request, Fields, x => 
                            new 
                            { 
                                x.Id, 
                                x.Code, 
                                x.NameKey, 
                                x.DescriptionKey, 
                                x.IsActive, 
                                x.IsSystem, 
                                x.SortOrder 
                            }, cancellationToken));
    }
    
    /// <summary>
    /// Gets one resolution rule.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        object? result = await db.DiagnosticAreaResolutionRules.AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => 
                new 
                { 
                    x.Id, 
                    x.Code, 
                    x.NameKey, 
                    x.DescriptionKey, 
                    x.IsActive, 
                    x.IsSystem, 
                    x.SortOrder 
                })
            .SingleOrDefaultAsync(cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }
}
