using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Auth.Authorization;
using IKP.Application.Common.Query;
using IKP.Application.Reports;
using IKP.Application.Reports.Interfaces;
using IKP.Domain.Entities.Reports.Lookups;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Reports;

[ApiController]
[Route("api/reports/definitions")]
[Authorize(Policy=AuthorizationPolicyNames.ReportsRead)]
public sealed class ReportDefinitionsController(IReportDefinitionService service, IDbContextFactory<AppDbContext> factory) : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<ReportDefinition, object?>>> Fields = 
        new Dictionary<string, Expression<Func<ReportDefinition, object?>>>(StringComparer.OrdinalIgnoreCase)
        {
            ["id"] = x => x.Id,
            ["code"] = x => x.Code,
            ["categoryId"] = x => x.CategoryId,
            ["categoryCode"] = x => x.Category.Code,
            ["isActive"] = x => x.IsActive,["isSystem"] = x => x.IsSystem
        };

    [HttpPost("query")]
    public async Task<IActionResult> Query(UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);
        
        return Ok(await db.ReportDefinitions
            .AsNoTracking()
            .ToPagedResponseAsync(request, Fields, x => 
                new
                {
                    x.Id,
                    x.CategoryId,
                    CategoryCode = x.Category.Code, 
                    x.Code, 
                    x.NameKey, 
                    x.DescriptionKey, 
                    x.IsHtmlDescription, 
                    x.IsActive, 
                    x.IsSystem
                }, 
            cancellationToken));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        ReportDefinitionResponse? reportDefinition = await service.GetAsync(id, cancellationToken);
        
        return reportDefinition is null?NotFound():Ok(reportDefinition);
    }
    
    [HttpPost]
    [Authorize(Policy=AuthorizationPolicyNames.ReportsManage)]
    public async Task<IActionResult> Create(CreateReportDefinitionRequest request,CancellationToken cancellationToken)
    {
        ReportDefinitionResponse reportDefinition = await service.CreateAsync(request, cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = reportDefinition.Id }, reportDefinition); 
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.ReportsManage)]
    public async Task<IActionResult> Update(Guid id, UpdateReportDefinitionRequest request, CancellationToken cancellationToken) => Ok(await service.UpdateAsync(id, request, cancellationToken));
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.ReportsManage)]
    public async Task<IActionResult>Delete(Guid id, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id,cancellationToken);
        
        return NoContent();
    }
}