using IKP.Application.Auth.Authorization;
using IKP.Domain.Entities.App.HR.CompetencyMatrix;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.HR.CompetencyMatrix;

[ApiController,Route("api/hr/competency-assessments"),Authorize(Policy=AuthorizationPolicyNames.EmployeesRead)]
public sealed class CompetencyAssessmentsController(IDbContextFactory<AppDbContext> factory):ControllerBase
{
    public sealed record ItemValue(decimal Value,string? Comment);
    public sealed record Item(Guid MatrixItemId,ItemValue? Reference,ItemValue? Manager,ItemValue? Employee);
    public sealed record CreateRequest(Guid EmployeeId,Guid MatrixVersionId,DateTime AssessmentDate,Guid AssessorEmployeeId,Guid AssessmentTypeId,string? Notes,IReadOnlyCollection<Item> Items);
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id,CancellationToken ct){await using var db=await factory.CreateDbContextAsync(ct);var x=await db.CompetencyAssessments.AsNoTracking().Where(x=>x.Id==id&&!x.IsDeleted).Select(x=>new{x.Id,x.EmployeeId,x.MatrixVersionId,x.AssessmentDate,x.AssessorEmployeeId,x.AssessmentTypeId,x.Notes,Items=x.Items.Where(i=>!i.IsDeleted).Select(i=>new{i.Id,i.MatrixItemId,Reference=i.ReferenceAssessmentValue==null?null:new{i.ReferenceAssessmentValue.Value,i.ReferenceAssessmentValue.Comment},Manager=i.ManagerAssessmentValue==null?null:new{i.ManagerAssessmentValue.Value,i.ManagerAssessmentValue.Comment},Employee=i.EmployeeAssessmentValue==null?null:new{i.EmployeeAssessmentValue.Value,i.EmployeeAssessmentValue.Comment}})}).SingleOrDefaultAsync(ct);return x is null?NotFound():Ok(x);}
    [HttpPost]
    [Authorize(Policy=AuthorizationPolicyNames.EmployeesManage)]
    public async Task<IActionResult>Create(CreateRequest r,CancellationToken ct){await using var db=await factory.CreateDbContextAsync(ct);var a=new CompetencyAssessment{EmployeeId=r.EmployeeId,MatrixVersionId=r.MatrixVersionId,AssessmentDate=r.AssessmentDate,AssessorEmployeeId=r.AssessorEmployeeId,AssessmentTypeId=r.AssessmentTypeId,Notes=r.Notes};foreach(var i in r.Items){var x=new CompetencyAssessmentItem{Assessment=a,MatrixItemId=i.MatrixItemId};if(i.Reference is not null)x.ReferenceAssessmentValue=new CompetencyAssessmentItemValue{Value=i.Reference.Value,Comment=i.Reference.Comment};if(i.Manager is not null)x.ManagerAssessmentValue=new CompetencyAssessmentItemValue{Value=i.Manager.Value,Comment=i.Manager.Comment};if(i.Employee is not null)x.EmployeeAssessmentValue=new CompetencyAssessmentItemValue{Value=i.Employee.Value,Comment=i.Employee.Comment};a.Items.Add(x);}db.CompetencyAssessments.Add(a);await db.SaveChangesAsync(ct);return CreatedAtAction(nameof(Get),new{id=a.Id},new{a.Id});}
}
