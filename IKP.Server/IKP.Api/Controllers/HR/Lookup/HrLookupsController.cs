using IKP.Application.Auth.Authorization;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.HR.Lookup;

[ApiController,Route("api/hr/lookups"),Authorize(Policy=AuthorizationPolicyNames.EmployeesRead)]
public sealed class HrLookupsController(IDbContextFactory<AppDbContext> factory):ControllerBase
{
    [HttpGet("competency-section-types")] 
    public Task<IActionResult> CompetencySectionTypes(CancellationToken cancellationToken) => 
        Lookup(db => db.CompetencySectionType
                        .Where(x => x.IsActive && !x.IsDeleted)
                        .OrderBy(x => x.SortOrder)
                        .Select(x => new{ x.Id, x.Name, x.Description, x.SortOrder}), cancellationToken);

    [HttpGet("competency-assessment-types")] 
    public Task<IActionResult> CompetencyAssessmentTypes(CancellationToken cancellationToken) => 
        Lookup(db => db.CompetencyAssessmentType
                        .Where(x=>!x.IsDeleted)
                        .OrderBy(x=>x.Name)
                        .Select(x => new { x.Id, x.Name }), cancellationToken);
   
    [HttpGet("attestation-section-types")]
    public Task<IActionResult> AttestationSectionTypes(CancellationToken cancellationToken) => 
        Lookup(db => db.AttestationSectionType
                        .Where(x => !x.IsDeleted)
                        .OrderBy(x=>x.Name)
                        .Select(x=>new { x.Id, x.Name, x.ForPromotion}), cancellationToken);
    
    [HttpGet("attestation-question-types")] 
    public Task<IActionResult> AttestationQuestionTypes(CancellationToken cancellationToken) => 
        Lookup(db => db.AttestationQuestionType
                        .Where(x=>!x.IsDeleted)
                        .OrderBy(x=>x.Name)
                        .Select(x => new { x.Id, x.Name, x.Description }), cancellationToken);
    
    [HttpGet("development-plan-types")] 
    public Task<IActionResult> DevelopmentPlanTypes(CancellationToken cancellationToken) => 
        Lookup(db => db.DevelopmentPlanType
                        .Where(x => !x.IsDeleted)
                        .OrderBy(x => x.Name)
                        .Select(x => new{ x.Id, x.Name }), cancellationToken);
    
    [HttpGet("development-item-types")] 
    public Task<IActionResult> DevelopmentItemTypes(CancellationToken cancellationToken) => 
        Lookup(db=>db.DevelopmentItemType.Where(x=>!x.IsDeleted).OrderBy(x=>x.Name).Select(x=>new{x.Id,x.Name}),cancellationToken);
    
    [HttpGet("development-needs")] 
    public Task<IActionResult> DevelopmentNeeds(CancellationToken cancellationToken) => 
        Lookup(db=>db.DevelopmentNeeds.Where(x=>!x.IsDeleted).OrderBy(x=>x.Name).Select(x=>new{x.Id,x.Name,x.TypeId}),cancellationToken);
    
    [HttpGet("development-methods")] 
    public Task<IActionResult> DevelopmentMethods(CancellationToken cancellationToken) => 
        Lookup(db=>db.DevelopmentMethods.Where(x=>!x.IsDeleted).OrderBy(x=>x.Name).Select(x=>new{x.Id,x.Name,x.IsControl}),cancellationToken);
   
    [HttpGet("development-control-methods")] 
    public Task<IActionResult> DevelopmentControlMethods(CancellationToken cancellationToken)=>
        Lookup(db=>db.DevelopmentControlMethods.Where(x=>!x.IsDeleted).OrderBy(x=>x.Name).Select(x=>new{x.Id,x.Name}),cancellationToken);
    
    [HttpGet("education-types")] 
    public Task<IActionResult> EducationTypes(CancellationToken cancellationToken)=>
        Lookup(db=>db.EducationType.Where(x=>!x.IsDeleted).OrderBy(x=>x.Name).Select(x=>new{x.Id,x.Name,x.Description}),cancellationToken);
   
    [HttpGet("employee-contact-types")] 
    public Task<IActionResult> EmployeeContactTypes(CancellationToken cancellationToken)=>
        Lookup(db=>db.EmployeeContactType.Where(x=>x.IsActive&&!x.IsDeleted).OrderBy(x=>x.SortOrder).Select(x=>new{x.Id,x.Name,x.Description,x.SortOrder}),cancellationToken);
   
    [HttpGet("employment-record-types")] 
    public Task<IActionResult> EmploymentRecordTypes(CancellationToken cancellationToken)=>
        Lookup(db=>db.EmploymentRecordType.Where(x=>x.IsActive&&!x.IsDeleted).OrderBy(x=>x.SortOrder).Select(x=>new{x.Id,x.Name,x.Description,x.SortOrder}),cancellationToken);
    
    private async Task<IActionResult> Lookup<T>(Func<AppDbContext,IQueryable<T>> query, CancellationToken ct) 
    { 
        await using AppDbContext db = await factory.CreateDbContextAsync(ct);

        return Ok(await query(db).ToListAsync(ct));
    }
}
