using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.App.HR.Employees.Interfaces;
using IKP.Application.App.HR.Employees.Models;
using IKP.Application.Auth.Authorization;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.App.HR;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Employees;

[ApiController]
[Route("api/employees")]
[Authorize(Policy = AuthorizationPolicyNames.EmployeesRead)]
public sealed class EmployeesController(IEmployeeService service, IDbContextFactory<AppDbContext> factory) : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<Employee, object?>>> Fields = new Dictionary<string, Expression<Func<Employee, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id, 
        ["firstName"] = x => x.FirstName, 
        ["lastName"] = x => x.LastName, 
        ["familyName"] = x => x.FamilyName,
        ["dateOfBirth"] = x => x.DateOfBirth, 
        ["hireDate"] = x => x.HireDate, 
        ["userId"] = x => x.UserId, 
        ["isDeleted"] = x => x.IsDeleted
    };

    [HttpPost("query")]
    public async Task<IActionResult> Query(UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using var db = await factory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.Employees
                    .AsNoTracking()
                    .Where(x => !x.IsDeleted)
                    .ToPagedResponseAsync(request, Fields, x => 
                    new EmployeeListItem
                    {
                        Id = x.Id, 
                        FirstName = x.FirstName, 
                        LastName = x.LastName, 
                        FamilyName = x.FamilyName, 
                        DateOfBirth = x.DateOfBirth, 
                        HireDate = x.HireDate, 
                        UserId = x.UserId
                    }, cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken) => (await service.GetAsync(id, cancellationToken)) is { } employee ? Ok(employee) : NotFound();

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> Create(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        EmployeeDetailsResponse employee = await service.CreateAsync(request, cancellationToken); return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> Update(Guid id, UpdateEmployeeRequest request, CancellationToken cancellationToken) => Ok(await service.UpdateAsync(id, request, cancellationToken));

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) { await service.DeleteAsync(id, cancellationToken); return NoContent(); }

    [HttpPost("{id:guid}/restore")]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken) { await service.RestoreAsync(id, cancellationToken); return NoContent(); }
}
