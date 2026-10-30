using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Auth.Authorization;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.App.HR.Lookup;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.HR.Organization;

[ApiController]
[Route("api/hr/positions")]
[Authorize(Policy = AuthorizationPolicyNames.EmployeesRead)]
public sealed class PositionsController(IDbContextFactory<AppDbContext> factory) : ControllerBase
{
    public sealed record SaveRequest(string Name, string? Description, int SortOrder, bool IsActive = true);

    private static readonly IReadOnlyDictionary<string, Expression<Func<Position, object?>>> Fields = 
        new Dictionary<string, Expression<Func<Position, object?>>>(StringComparer.OrdinalIgnoreCase) 
        { 
            ["id"] = x => x.Id,
            ["name"] = x => x.Name,
            ["description"] = x => x.Description,
            ["sortOrder"] = x => x.SortOrder,
            ["isActive"] = x => x.IsActive 
        };

    [HttpPost("query")] 
    public async Task<IActionResult> Query(UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);
        
        return Ok(await db.Positions
                        .AsNoTracking()
                        .Where(x => !x.IsDeleted)
                        .ToPagedResponseAsync(request, Fields, x => new { x.Id, x.Name, x.Description, x.SortOrder, x.IsActive }, cancellationToken)); 
    }
    
    [HttpGet("{id:guid}")] 
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);
        
        var x = await db.Positions
                        .AsNoTracking()
                        .Where(x => x.Id == id && !x.IsDeleted)
                        .Select(x => new { x.Id, x.Name, x.Description, x.SortOrder, x.IsActive })
                        .SingleOrDefaultAsync(cancellationToken);
        
        return x is null ? NotFound() : Ok(x);
    }
    
    [HttpPost]
    [Authorize(Policy=AuthorizationPolicyNames.EmployeesManage)] 
    public async Task<IActionResult> Create(SaveRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);
        
        Position result = new () { Name = request.Name.Trim(), Description = request.Description, SortOrder = request.SortOrder, IsActive = request.IsActive}; 
        
        db.Positions.Add(result);
        await db.SaveChangesAsync(cancellationToken);
        
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result.Id);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.EmployeesManage)] 
    public async Task<IActionResult> Update(Guid id, SaveRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);
        Position? result = await db.Positions.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        
        if(result is null) return NotFound();
        
        result.Name = request.Name.Trim();
        result.Description = request.Description;
        result.SortOrder = request.SortOrder;
        result.IsActive = request.IsActive;
        
        await db.SaveChangesAsync(cancellationToken);
        
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.EmployeesManage)] 
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        Position? result = await db.Positions.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        
        if(result is null) return NotFound();
        
        result.IsDeleted = true;
        result.DeletedAt = DateTime.UtcNow;
        
        await db.SaveChangesAsync(cancellationToken);
        
        return NoContent();
    }
}
