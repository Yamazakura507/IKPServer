using IKP.Application.Auth.Authorization;
using IKP.Application.Updates.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKP.Api.Controllers.Updates;

[ApiController]
[Route("api/updates/access")]
[Authorize(Policy=AuthorizationPolicyNames.UpdatesRead)]
public sealed class UpdateProductAccessController(IUpdateProductAccessService service) : ControllerBase
{
    [HttpGet("users/{userId:guid}")]
    public async Task<IActionResult> Get (Guid userId, CancellationToken cancellationToken ) => Ok(await service.GetAllowedProductIdsAsync(userId,cancellationToken));
    
    [HttpPost("users/{userId:guid}/products/{productId:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.UpdatesManage)]
    public async Task<IActionResult> Add (Guid userId, Guid productId, CancellationToken cancellationToken)
    {
        await service.AddAsync(userId, productId, cancellationToken);

        return NoContent();
    }
    
    [HttpDelete("users/{userId:guid}/products/{productId:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.UpdatesManage)]
    public async Task<IActionResult>Remove(Guid userId, Guid productId, CancellationToken cancellationToken)
    {
        await service.RemoveAsync(userId, productId, cancellationToken);
        
        return NoContent();
    }
}
