using IKP.Application.Auth.Administration.Interfaces;
using IKP.Application.Auth.Administration.Models;
using IKP.Application.Auth.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IKP.Api.Controllers.Auth.Administration;

/// <summary>
/// Provides administrator-only role and permission management.
/// </summary>
[ApiController]
[Route("api/auth/admin")]
[Authorize]
public sealed class RolesController : ControllerBase
{
    private readonly IUserAdministrationService service;

    /// <summary>
    /// Initializes the controller.
    /// </summary>
    public RolesController(IUserAdministrationService service)
    {
        this.service = service;
    }

    /// <summary>
    /// Gets all roles and their permissions.
    /// </summary>
    [HttpGet("roles")]
    [Authorize(Policy = AuthorizationPolicyNames.RolesRead)]
    public async Task<ActionResult<IReadOnlyList<AuthRoleAdministrationResponse>>> GetRoles(CancellationToken cancellationToken) => Ok(await service.GetRolesAsync(cancellationToken));

    /// <summary>
    /// Creates a custom role.
    /// </summary>
    [HttpPost("roles")]
    [Authorize(Policy = AuthorizationPolicyNames.RolesManage)]
    public async Task<ActionResult<AuthRoleAdministrationResponse>> CreateRole([FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
    {
        Guid actorUserId = GetRequiredUserId();
        AuthRoleAdministrationResponse response = await service.CreateRoleAsync(request, actorUserId, cancellationToken);

        return Created($"api/auth/admin/roles/{response.Id}", response);
    }

    /// <summary>
    /// Updates a custom role.
    /// </summary>
    [HttpPut("roles/{roleId:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.RolesManage)]
    public async Task<ActionResult<AuthRoleAdministrationResponse>> UpdateRole(Guid roleId, [FromBody] UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        AuthRoleAdministrationResponse response = await service.UpdateRoleAsync(roleId, request, GetRequiredUserId(), cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Replaces permissions assigned to a custom role.
    /// </summary>
    [HttpPut("roles/{roleId:guid}/permissions")]
    [Authorize(Policy = AuthorizationPolicyNames.RolePermissionsManage)]
    public async Task<IActionResult> SetRolePermissions(Guid roleId, [FromBody] SetRolePermissionsRequest request, CancellationToken cancellationToken)
    {
        await service.SetRolePermissionsAsync(roleId, request, GetRequiredUserId(), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Gets all permissions defined by the server.
    /// </summary>
    [HttpGet("permissions")]
    [Authorize(Policy = AuthorizationPolicyNames.PermissionsRead)]
    public async Task<ActionResult<IReadOnlyList<AuthPermissionAdministrationResponse>>> GetPermissions(CancellationToken cancellationToken) => Ok(await service.GetPermissionsAsync(cancellationToken));

    private Guid GetRequiredUserId()
    {
        string? value = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(value, out Guid userId)) throw new InvalidOperationException("The authenticated user identifier is missing.");

        return userId;
    }
}
