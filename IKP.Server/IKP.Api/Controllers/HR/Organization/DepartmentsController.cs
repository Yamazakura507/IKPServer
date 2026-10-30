using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Auth.Authorization;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.App.HR;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.HR.Organization;

[ApiController]
[Route("api/hr/departments")]
[Authorize(Policy = AuthorizationPolicyNames.EmployeesRead)]
public sealed class DepartmentsController(IDbContextFactory<AppDbContext> factory) : ControllerBase
{
    public sealed record SaveDepartmentRequest(string Name, string? Description, Guid? ParentDepartmentId, Guid? HeadEmployeeId, int SortOrder, bool IsActive = true);

    private static readonly IReadOnlyDictionary<string, Expression<Func<Department, object?>>> Fields = 
        new Dictionary<string, Expression<Func<Department, object?>>>(StringComparer.OrdinalIgnoreCase)
        {
            ["id"] = x => x.Id, 
            ["name"] = x => x.Name, 
            ["description"] = x => x.Description,
            ["parentDepartmentId"] = x => x.ParentDepartmentId, 
            ["headEmployeeId"] = x => x.HeadEmployeeId,
            ["sortOrder"] = x => x.SortOrder, 
            ["isActive"] = x => x.IsActive, 
            ["isDeleted"] = x => x.IsDeleted
        };

    [HttpPost("query")]
    public async Task<IActionResult> Query(UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.Departments
                            .AsNoTracking()
                            .Where(x => !x.IsDeleted)
                            .ToPagedResponseAsync(request, Fields, x => 
                                new 
                                { 
                                    x.Id, 
                                    x.Name, 
                                    x.Description,
                                    x.ParentDepartmentId,
                                    x.HeadEmployeeId,
                                    x.SortOrder,
                                    x.IsActive 
                                }, cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        var result = await db.Departments
                            .AsNoTracking()
                            .Where(x => x.Id == id && !x.IsDeleted)
                            .Select(x => new { x.Id, x.Name, x.Description, x.ParentDepartmentId, x.HeadEmployeeId, x.SortOrder, x.IsActive }).SingleOrDefaultAsync(cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> Create(SaveDepartmentRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        Department entity = new () 
        { 
            Name = request.Name.Trim(), 
            Description = request.Description, 
            ParentDepartmentId = request.ParentDepartmentId, 
            HeadEmployeeId = request.HeadEmployeeId, 
            SortOrder = request.SortOrder, 
            IsActive = request.IsActive 
        };

        db.Departments.Add(entity); 
        await db.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity.Id);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> Update(Guid id, SaveDepartmentRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        Department? entity = await db.Departments.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken); 
        
        if (entity is null) return NotFound();

        entity.Name = request.Name.Trim(); 
        entity.Description = request.Description; 
        entity.ParentDepartmentId = request.ParentDepartmentId; 
        entity.HeadEmployeeId = request.HeadEmployeeId; 
        entity.SortOrder = request.SortOrder; 
        entity.IsActive = request.IsActive;

        await db.SaveChangesAsync(cancellationToken); 
        
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        Department? entity = await db.Departments.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken); 
        
        if (entity is null) return NotFound();

        entity.IsDeleted = true; 
        entity.DeletedAt = DateTime.UtcNow; 
        
        await db.SaveChangesAsync(cancellationToken); 
        
        return NoContent();
    }
}
