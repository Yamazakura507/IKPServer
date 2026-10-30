using IKP.Application.Auth.Authorization;
using IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Probation;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace IKP.Api.Controllers.HR.Development;
[ApiController,Route("api/hr/probation-plans"),Authorize(Policy=AuthorizationPolicyNames.EmployeesRead)]
public sealed class ProbationPlansController(IDbContextFactory<AppDbContext> factory):ControllerBase{
 public sealed record CreateRequest(Guid EmployeeId,Guid ManagerEmployeeId,DateOnly StartDate,DateOnly EndDate,string? Notes,IReadOnlyCollection<TaskRequest>? Tasks);
 public sealed record TaskRequest(int SortOrder,string Name,string? Description,DateOnly? Deadline,decimal CompletionPercent,string? WeeklyReport,string? Comment);
 [HttpGet("{id:guid}")] public async Task<IActionResult>Get(Guid id,CancellationToken ct){await using var db=await factory.CreateDbContextAsync(ct);var x=await db.ProbationPlans.AsNoTracking().Where(x=>x.Id==id&&!x.IsDeleted).Select(x=>new{x.Id,x.EmployeeId,x.ManagerEmployeeId,x.StartDate,x.EndDate,x.Notes,x.FinalCompletionPercent,x.FinalComment,Tasks=x.Tasks.Where(t=>!t.IsDeleted).OrderBy(t=>t.SortOrder).Select(t=>new{t.Id,t.SortOrder,t.Name,t.Description,t.Deadline,t.CompletionPercent,t.WeeklyReport,t.Comment})}).SingleOrDefaultAsync(ct);return x is null?NotFound():Ok(x);}
 [HttpPost,Authorize(Policy=AuthorizationPolicyNames.EmployeesManage)] public async Task<IActionResult>Create(CreateRequest r,CancellationToken ct){await using var db=await factory.CreateDbContextAsync(ct);var p=new ProbationPlan{EmployeeId=r.EmployeeId,ManagerEmployeeId=r.ManagerEmployeeId,StartDate=r.StartDate,EndDate=r.EndDate,Notes=r.Notes};foreach(var t in r.Tasks??[])p.Tasks.Add(new ProbationTask{SortOrder=t.SortOrder,Name=t.Name.Trim(),Description=t.Description,Deadline=t.Deadline,CompletionPercent=t.CompletionPercent,WeeklyReport=t.WeeklyReport,Comment=t.Comment});db.ProbationPlans.Add(p);await db.SaveChangesAsync(ct);return CreatedAtAction(nameof(Get),new{id=p.Id},new{p.Id});}
 [HttpPut("{id:guid}/tasks/{taskId:guid}"),Authorize(Policy=AuthorizationPolicyNames.EmployeesManage)] public async Task<IActionResult>UpdateTask(Guid id,Guid taskId,TaskRequest r,CancellationToken ct){await using var db=await factory.CreateDbContextAsync(ct);var t=await db.ProbationTasks.SingleOrDefaultAsync(x=>x.Id==taskId&&x.PlanId==id&&!x.IsDeleted,ct);if(t is null)return NotFound();t.SortOrder=r.SortOrder;t.Name=r.Name.Trim();t.Description=r.Description;t.Deadline=r.Deadline;t.CompletionPercent=r.CompletionPercent;t.WeeklyReport=r.WeeklyReport;t.Comment=r.Comment;await db.SaveChangesAsync(ct);return NoContent();}
}
