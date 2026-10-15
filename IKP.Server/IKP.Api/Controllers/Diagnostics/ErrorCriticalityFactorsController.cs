using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.Diagnostics.Criticality;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics;

/// <summary>
/// Provides diagnostic criticality factor read operations.
/// </summary>
[ApiController]
[Route("api/diagnostics/criticality-factors")]
public sealed class ErrorCriticalityFactorsController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<ErrorCriticalityFactor, object?>>> Fields = new Dictionary<string, Expression<Func<ErrorCriticalityFactor, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id, 
        ["errorCriticalityId"] = x => x.ErrorCriticalityId, 
        ["definitionId"] = x => x.DefinitionId, 
        ["definitionCode"] = x => x.Definition.Code,
        ["score"] = x => x.Score, 
        ["weight"] = x => x.Weight, 
        ["contribution"] = x => x.Contribution, 
        ["value"] = x => x.Value, 
        ["calculatedAt"] = x => x.CalculatedAt
    };
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;
    
    /// <summary>
    /// Initializes a new controller.
    /// </summary>
    public ErrorCriticalityFactorsController(IDbContextFactory<AppDbContext> dbContextFactory) => this.dbContextFactory = dbContextFactory;
    
    /// <summary>
    /// Queries criticality factors.
    /// </summary>
    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        return Ok(await db.ErrorCriticalityFactors.AsNoTracking()
            .ToPagedResponseAsync(request, Fields, x => 
                new 
                { 
                    x.Id, 
                    x.ErrorCriticalityId, 
                    x.DefinitionId, 
                    DefinitionCode = x.Definition.Code, 
                    x.Score, 
                    x.Weight, 
                    x.Contribution, 
                    x.Value, 
                    x.CalculatedAt 
                }, cancellationToken));
    }
}
