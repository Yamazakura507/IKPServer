using IKP.Application.Auth.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.Diagnostics.Criticality;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics;

/// <summary>
/// Provides diagnostic criticality read operations.
/// </summary>
[ApiController]
[Authorize(Policy = AuthorizationPolicyNames.DiagnosticsRead)]
[Route("api/diagnostics/criticalities")]
public sealed class ErrorCriticalitiesController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<ErrorCriticality, object?>>> Fields = new Dictionary<string, Expression<Func<ErrorCriticality, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id, 
        ["errorGroupId"] = x => x.ErrorGroupId, 
        ["automaticScore"] = x => x.AutomaticScore, 
        ["manualScore"] = x => x.ManualScore,
        ["effectiveScore"] = x => x.EffectiveScore, 
        ["calculationVersion"] = x => x.CalculationVersion, 
        ["calculatedAt"] = x => x.CalculatedAt,
        ["overrideUserId"] = x => x.OverrideUserId, 
        ["overrideAt"] = x => x.OverrideAt, 
        ["overrideReason"] = x => x.OverrideReason
    };

    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    /// <summary>
    /// Initializes a new controller.
    /// </summary>
    public ErrorCriticalitiesController(IDbContextFactory<AppDbContext> dbContextFactory) => this.dbContextFactory = dbContextFactory;

    /// <summary>
    /// Queries error criticality records.
    /// </summary>
    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        return Ok(await db.ErrorCriticalities.AsNoTracking()
                        .ToPagedResponseAsync(request, Fields, x => 
                            new 
                            { 
                                x.Id, 
                                x.ErrorGroupId, 
                                x.AutomaticScore, 
                                x.ManualScore, 
                                x.EffectiveScore, 
                                x.CalculationVersion, 
                                x.CalculatedAt, 
                                x.OverrideUserId, 
                                x.OverrideAt, 
                                x.OverrideReason 
                            }, cancellationToken));
    }

    /// <summary>
    /// Gets one criticality record including factors.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        object? result = await db.ErrorCriticalities.AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => 
                new 
                { 
                    x.Id, 
                    x.ErrorGroupId, 
                    x.AutomaticScore, 
                    x.ManualScore, 
                    x.EffectiveScore, 
                    x.CalculationVersion, 
                    x.CalculatedAt, 
                    x.OverrideUserId, 
                    x.OverrideAt, 
                    x.OverrideReason, 
                    Factors = x.Factors.Select(y => 
                        new 
                        { 
                            y.Id, 
                            y.DefinitionId, 
                            DefinitionCode = y.Definition.Code, 
                            y.Score, 
                            y.Weight, 
                            y.Contribution, 
                            y.Value, 
                            y.CalculatedAt 
                        }).ToList() 
                })
            .SingleOrDefaultAsync(cancellationToken);
        
        return result is null ? NotFound() : Ok(result);
    }
}
