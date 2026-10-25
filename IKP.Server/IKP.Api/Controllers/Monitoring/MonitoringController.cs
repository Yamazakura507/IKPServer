using IKP.Application.Auth.Authorization;
using IKP.Application.Monitoring.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKP.Api.Controllers.Monitoring;

[ApiController]
[Route("api/monitoring")]
[Authorize(Policy=AuthorizationPolicyNames.MonitoringRead)]
public sealed class MonitoringController(IMonitoringService service) : ControllerBase
{
    [HttpGet("health")]
    public async Task<IActionResult> Health([FromQuery]int limit = 50, CancellationToken cancellationToken = default) => Ok(await service.GetRecentHealthAsync(limit, cancellationToken));

    [Authorize(Policy=AuthorizationPolicyNames.MonitoringManage)]
    [HttpPost("health/check")]
    public async Task<IActionResult> Check([FromQuery]string component = "database", CancellationToken cancellationToken = default) => Ok(await service.CheckAsync(component, cancellationToken));

    [Authorize(Policy=AuthorizationPolicyNames.MonitoringManage)]
    [HttpPost("retention/run")]
    public async Task<IActionResult> Retention(CancellationToken cancellationToken = default) => Ok(new { Deleted = await service.ApplyRetentionAsync(cancellationToken) });
}
