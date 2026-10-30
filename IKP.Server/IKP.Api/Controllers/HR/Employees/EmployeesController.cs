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

namespace IKP.Api.Controllers.HR.Employees;

[ApiController]
[Route("api/employees")]
[Authorize(Policy = AuthorizationPolicyNames.EmployeesRead)]
public sealed class EmployeesController(IEmployeeService service, IDbContextFactory<AppDbContext> factory) : ControllerBase
{
    public sealed record EmploymentHistoryRequest(Guid DepartmentId, Guid PositionId, Guid? GradeId, Guid? ManagerEmployeeId, Guid RecordTypeId, DateOnly ValidFrom, DateOnly? ValidTo, string? Reason, string? Notes);
    public sealed record ContactRequest(Guid EmployeeContactTypeId, string Value, string? Label, bool IsPrimary);
    public sealed record EducationRequest(Guid EducationTypeId, string Organization, string? Faculty, string? Speciality, string? Qualification, int? GraduationYear, DateOnly? BeginOfDate, DateOnly? EndOfDate, string? ProofOfDocumentNumber, Guid? FileId);
    public sealed record QualificationRequest(string Name, string? Issuer, DateOnly? IssuedAt, DateOnly? ExpiresAt, string? Number, Guid? FileId);

    private static readonly IReadOnlyDictionary<string, Expression<Func<Employee, object?>>> Fields = new Dictionary<string, Expression<Func<Employee, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id, 
        ["firstName"] = x => x.FirstName, 
        ["lastName"] = x => x.LastName, 
        ["familyName"] = x => x.FamilyName,
        ["shortName"] = x => x.ShortName,
        ["nickName"] = x => x.NickName,
        ["dateOfBirth"] = x => x.DateOfBirth, 
        ["hireDate"] = x => x.HireDate, 
        ["userId"] = x => x.UserId, 
        ["isDeleted"] = x => x.IsDeleted
    };

    [HttpPost("query")]
    public async Task<IActionResult> Query(UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

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
                            ShortName = x.ShortName,
                            NickName = x.NickName,
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
        EmployeeDetailsResponse employee = await service.CreateAsync(request, cancellationToken); 
        
        return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> Update(Guid id, UpdateEmployeeRequest request, CancellationToken cancellationToken) => Ok(await service.UpdateAsync(id, request, cancellationToken));

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) 
    { 
        await service.DeleteAsync(id, cancellationToken); 
        
        return NoContent(); 
    }

    [HttpGet("{id:guid}/employment-history")]
    public async Task<IActionResult> EmploymentHistory(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        if (!await db.Employees.AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken)) return NotFound();

        return Ok(await db.EmployeeEmploymentHistory
            .AsNoTracking()
            .Where(x => x.EmployeeId == id)
            .OrderByDescending(x => x.ValidFrom)
            .Select(x => 
                new 
                { 
                    x.Id, 
                    x.EmployeeId, 
                    x.DepartmentId, 
                    x.PositionId, 
                    x.GradeId, 
                    x.ManagerEmployeeId, 
                    x.RecordTypeId, 
                    x.ValidFrom, 
                    x.ValidTo, 
                    x.Reason, 
                    x.Notes 
                })
            .ToListAsync(cancellationToken));
    }

    [HttpGet("{id:guid}/contacts")]
    public async Task<IActionResult> Contacts(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        if (!await db.Employees.AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken)) return NotFound();

        return Ok(await db.EmployeeContacts
            .AsNoTracking()
            .Where(x => x.EmployeeId == id && !x.IsDeleted)
            .OrderByDescending(x => x.IsPrimary)
            .Select(x => 
                new 
                { 
                    x.Id,
                    x.EmployeeContactTypeId, 
                    x.Value, 
                    x.Label, 
                    x.IsPrimary, 
                    x.IsVerified, 
                    x.VerifiedAt 
                })
            .ToListAsync(cancellationToken));
    }

    [HttpGet("{id:guid}/education")]
    public async Task<IActionResult> Education(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        if (!await db.Employees.AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken)) return NotFound();

        return Ok(await db.EmployeeEducation
            .AsNoTracking()
            .Where(x => x.EmployeeId == id && !x.IsDeleted)
            .OrderByDescending(x => x.EndOfDate)
            .Select(x => 
                new 
                { 
                    x.Id, 
                    x.EducationTypeId, 
                    x.Organization, 
                    x.Faculty, 
                    x.Speciality, 
                    x.Qualification, 
                    x.GraduationYear, 
                    x.BeginOfDate, 
                    x.EndOfDate, 
                    x.ProofOfDocumentNumber, 
                    x.FileId })
            .ToListAsync(cancellationToken));
    }

    [HttpGet("{id:guid}/qualifications")]
    public async Task<IActionResult> Qualifications(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        if (!await db.Employees.AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken)) return NotFound();

        return Ok(await db.EmployeeQualifications
                            .AsNoTracking()
                            .Where(x => x.EmployeeId == id && !x.IsDeleted)
                            .OrderByDescending(x => x.IssuedAt)
                            .Select(x => new { x.Id, x.Name, x.Issuer, x.IssuedAt, x.ExpiresAt, x.Number, x.FileId })
                            .ToListAsync(cancellationToken));
    }

    [HttpPost("{id:guid}/employment-history")]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> AddEmploymentHistory(Guid id, EmploymentHistoryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken); 
        
        if (!await db.Employees.AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken)) return NotFound();

        EmploymentHistory result =
            new () 
            { 
                EmployeeId = id, 
                DepartmentId = request.DepartmentId, 
                PositionId = request.PositionId, 
                GradeId = request.GradeId, 
                ManagerEmployeeId = request.ManagerEmployeeId, 
                RecordTypeId = request.RecordTypeId, 
                ValidFrom = request.ValidFrom, 
                ValidTo = request.ValidTo, 
                Reason = request.Reason, 
                Notes = request.Notes 
            };
        
        db.EmployeeEmploymentHistory.Add(result); 
        await db.SaveChangesAsync(cancellationToken); 
        
        return Ok(new { result.Id });
    }

    [HttpPost("{id:guid}/contacts")]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> AddContact(Guid id, ContactRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken); 
        
        if (!await db.Employees.AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken)) return NotFound();

        EmployeeContact result = new () 
        { 
            EmployeeId = id, 
            EmployeeContactTypeId = request.EmployeeContactTypeId, 
            Value = request.Value.Trim(), 
            Label = request.Label, 
            IsPrimary = request.IsPrimary 
        };
        
        db.EmployeeContacts.Add(result); 
        await db.SaveChangesAsync(cancellationToken); 
        
        return Ok(new { result.Id });
    }

    [HttpPost("{id:guid}/education")]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> AddEducation(Guid id, EducationRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken); 
        
        if (!await db.Employees.AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken)) return NotFound();

        EmployeeEducation result = new () 
        {
            EmployeeId = id, 
            EducationTypeId = request.EducationTypeId, 
            Organization = request.Organization.Trim(), 
            Faculty = request.Faculty, 
            Speciality = request.Speciality, 
            Qualification = request.Qualification, 
            GraduationYear = request.GraduationYear, 
            BeginOfDate = request.BeginOfDate, 
            EndOfDate = request.EndOfDate, 
            ProofOfDocumentNumber = request.ProofOfDocumentNumber, 
            FileId = request.FileId 
        };

        db.EmployeeEducation.Add(result); 
        await db.SaveChangesAsync(cancellationToken); 
        
        return Ok(new { result.Id });
    }

    [HttpPost("{id:guid}/qualifications")]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> AddQualification(Guid id, QualificationRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);
        
        if (!await db.Employees.AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken)) return NotFound();

        EmployeeQualification result = new () 
        { 
            EmployeeId = id, 
            Name = request.Name.Trim(), 
            Issuer = request.Issuer, 
            IssuedAt = request.IssuedAt, 
            ExpiresAt = request.ExpiresAt, 
            Number = request.Number, 
            FileId = request.FileId 
        };

        db.EmployeeQualifications.Add(result); 
        await db.SaveChangesAsync(cancellationToken); 
        
        return Ok(new { result.Id });
    }

    [HttpPost("{id:guid}/restore")]
    [Authorize(Policy = AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken) 
    { 
        await service.RestoreAsync(id, cancellationToken); 
        
        return NoContent(); 
    }
}
