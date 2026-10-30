using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Auth.Authorization;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.App.HR.CompetencyMatrix;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.HR.CompetencyMatrix;

[ApiController]
[Route("api/hr/competency-matrices")]
[Authorize(Policy = AuthorizationPolicyNames.EmployeesRead)]
public sealed class CompetencyMatricesController(IDbContextFactory<AppDbContext> factory) : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<Domain.Entities.App.HR.CompetencyMatrix.CompetencyMatrix, object?>>> Fields = new Dictionary<string, Expression<Func<Domain.Entities.App.HR.CompetencyMatrix.CompetencyMatrix, object?>>>(StringComparer.OrdinalIgnoreCase)
    { ["id"] = x=>x.Id,["name"] = x=>x.Name,["isActive"] = x=>x.IsActive,["isDeleted"] = x=>x.IsDeleted };

    [HttpPost("query")]
    public async Task<IActionResult> Query(UniversalQueryRequest request,CancellationToken ct){await using var db=await factory.CreateDbContextAsync(ct);return Ok(await db.CompetencyMatrices.AsNoTracking().Where(x=>!x.IsDeleted).ToPagedResponseAsync(request,Fields,x=>new{x.Id,x.Name,x.IsActive},ct));}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id,CancellationToken ct){await using var db=await factory.CreateDbContextAsync(ct);var x=await db.CompetencyMatrices.AsNoTracking().Where(x=>x.Id==id&&!x.IsDeleted).Select(x=>new{x.Id,x.Name,x.IsActive,Versions=x.Versions.Where(v=>!v.IsDeleted).OrderByDescending(v=>v.VersionNumber).Select(v=>new{v.Id,v.VersionNumber,v.EffectiveFrom,v.EffectiveTo,v.IsPublished,v.ChangeNotes})}).SingleOrDefaultAsync(ct);return x is null?NotFound():Ok(x);}
    public sealed record CreateMatrixRequest(string Name,bool IsActive=true);
    public sealed record CreateVersionRequest(int VersionNumber,DateTime EffectiveFrom,DateTime? EffectiveTo,string? ChangeNotes,bool IsPublished=false);
    public sealed record CreateSectionRequest(Guid SectionTypeId,string Name,int SortOrder);
    public sealed record CreateItemRequest(Guid SectionId,Guid? ParentItemId,string Name,string? Description,int SortOrder);
    public sealed record CreateTargetRequest(Guid PositionId,Guid? GradeId,decimal? ReferenceValue,string? ReferenceText);

    [HttpPost]
    [Authorize(Policy=AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> Create(CreateMatrixRequest r,CancellationToken ct){await using var db=await factory.CreateDbContextAsync(ct);var x=new Domain.Entities.App.HR.CompetencyMatrix.CompetencyMatrix { Name=r.Name.Trim(),IsActive=r.IsActive};db.CompetencyMatrices.Add(x);await db.SaveChangesAsync(ct);return CreatedAtAction(nameof(Get),new{id=x.Id},new{x.Id,x.Name});}
    [HttpPost("{id:guid}/versions")]
    [Authorize(Policy=AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> CreateVersion(Guid id,CreateVersionRequest r,CancellationToken ct){await using var db=await factory.CreateDbContextAsync(ct);if(!await db.CompetencyMatrices.AnyAsync(x=>x.Id==id&&!x.IsDeleted,ct))return NotFound();var x=new CompetencyMatrixVersion{MatrixId=id,VersionNumber=r.VersionNumber,EffectiveFrom=r.EffectiveFrom,EffectiveTo=r.EffectiveTo,ChangeNotes=r.ChangeNotes,IsPublished=r.IsPublished};db.CompetencyMatrixVersions.Add(x);await db.SaveChangesAsync(ct);return Ok(new{x.Id});}
    [HttpPost("versions/{versionId:guid}/sections")]
    [Authorize(Policy=AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> CreateSection(Guid versionId,CreateSectionRequest r,CancellationToken ct){await using var db=await factory.CreateDbContextAsync(ct);if(!await db.CompetencyMatrixVersions.AnyAsync(x=>x.Id==versionId&&!x.IsDeleted,ct))return NotFound();var x=new CompetencyMatrixSection{MatrixVersionId=versionId,SectionTypeId=r.SectionTypeId,Name=r.Name.Trim(),SortOrder=r.SortOrder};db.CompetencyMatrixSections.Add(x);await db.SaveChangesAsync(ct);return Ok(new{x.Id});}
    [HttpPost("sections/{sectionId:guid}/items")]
    [Authorize(Policy=AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> CreateItem(Guid sectionId,CreateItemRequest r,CancellationToken ct){await using var db=await factory.CreateDbContextAsync(ct);if(!await db.CompetencyMatrixSections.AnyAsync(x=>x.Id==sectionId&&!x.IsDeleted,ct))return NotFound();var x=new CompetencyMatrixItem{SectionId=sectionId,ParentItemId=r.ParentItemId,Name=r.Name.Trim(),Description=r.Description,SortOrder=r.SortOrder};db.CompetencyMatrixItems.Add(x);await db.SaveChangesAsync(ct);return Ok(new{x.Id});}
    [HttpPost("items/{itemId:guid}/targets")]
    [Authorize(Policy=AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult> CreateTarget(Guid itemId,CreateTargetRequest r,CancellationToken ct){await using var db=await factory.CreateDbContextAsync(ct);if(!await db.CompetencyMatrixItems.AnyAsync(x=>x.Id==itemId&&!x.IsDeleted,ct))return NotFound();var x=new CompetencyMatrixTarget{MatrixItemId=itemId,PositionId=r.PositionId,GradeId=r.GradeId,ReferenceValue=r.ReferenceValue,ReferenceText=r.ReferenceText};db.CompetencyMatrixTargets.Add(x);await db.SaveChangesAsync(ct);return Ok(new{x.Id});}

    [HttpGet("versions/{versionId:guid}/tree")]
    public async Task<IActionResult> Tree(Guid versionId,CancellationToken ct){await using var db=await factory.CreateDbContextAsync(ct);var x=await db.CompetencyMatrixVersions.AsNoTracking().Where(v=>v.Id==versionId&&!v.IsDeleted).Select(v=>new{v.Id,v.MatrixId,v.VersionNumber,v.EffectiveFrom,v.EffectiveTo,v.IsPublished,v.ChangeNotes,Sections=v.Sections.Where(s=>!s.IsDeleted).OrderBy(s=>s.SortOrder).Select(s=>new{s.Id,s.SectionTypeId,s.Name,s.SortOrder,Items=s.Items.Where(i=>!i.IsDeleted).OrderBy(i=>i.SortOrder).Select(i=>new{i.Id,i.ParentItemId,i.Name,i.Description,i.SortOrder,Targets=i.Targets.Where(t=>!t.IsDeleted).Select(t=>new{t.Id,t.PositionId,t.GradeId,t.ReferenceValue,t.ReferenceText})})})}).SingleOrDefaultAsync(ct);return x is null?NotFound():Ok(x);}
}
