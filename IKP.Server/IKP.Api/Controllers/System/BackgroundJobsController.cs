using IKP.Application.Auth.Authorization;
using IKP.Application.System.Interfaces;
using IKP.Application.System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKP.Api.Controllers.System;

[ApiController]
[Route("api/system/background-jobs")]
[Authorize(Policy = AuthorizationPolicyNames.SystemRead)]
public sealed class BackgroundJobsController(IBackgroundJobService service) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        BackgroundJobResponse? response = await service.GetAsync(id, cancellationToken);

        return response is null ? NotFound() : Ok(response);
    }
}
