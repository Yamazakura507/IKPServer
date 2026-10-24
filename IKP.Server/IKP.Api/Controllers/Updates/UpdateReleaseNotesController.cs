using IKP.Application.Auth.Authorization;
using IKP.Application.Updates;
using IKP.Application.Updates.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKP.Api.Controllers.Updates;

[ApiController]
[Route("api/updates/release-notes")]
[Authorize(Policy=AuthorizationPolicyNames.UpdatesRead)]

public sealed class UpdateReleaseNotesController(IUpdateReleaseNoteService service) : ControllerBase
{
    [HttpPost("version/{versionId:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.UpdatesManage)]
    public async Task<IActionResult> Create(Guid versionId, CreateReleaseNoteRequest request, CancellationToken cancellationToken) => Ok(await service.CreateAsync(versionId, request, cancellationToken));
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.UpdatesManage)]
    public async Task<IActionResult> Update(Guid id, UpdateReleaseNoteRequest request, CancellationToken cancellationToken) => Ok(await service.UpdateAsync(id, request, cancellationToken));
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.UpdatesManage)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id,cancellationToken);

        return NoContent();
    }
}
