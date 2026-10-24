using IKP.Application.Auth.Authorization;
using IKP.Application.Reports;
using IKP.Application.Reports.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKP.Api.Controllers.Reports;

[ApiController]
[Route("api/reports/parameters")]
[Authorize(Policy=AuthorizationPolicyNames.ReportsRead)]
public sealed class ReportParametersController(IReportParameterService service) : ControllerBase
{
    [HttpPost("{reportId:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.ReportsManage)]
    public async Task<IActionResult> Create(Guid reportId, CreateReportParameterRequest request, CancellationToken cancellationToken) => Ok(await service.CreateAsync(reportId, request, cancellationToken));
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.ReportsManage)]
    public async Task<IActionResult> Update(Guid id, UpdateReportParameterRequest request, CancellationToken cancellationToken) => Ok(await service.UpdateAsync(id, request, cancellationToken));
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.ReportsManage)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id, cancellationToken);
        
        return NoContent();
    }
}