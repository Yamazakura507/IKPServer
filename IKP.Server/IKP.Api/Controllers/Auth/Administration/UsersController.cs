using System.Security.Claims;
using IKP.Application.Auth.Administration.Interfaces;
using IKP.Application.Auth.Administration.Models;
using IKP.Application.Auth.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKP.Api.Controllers.Auth.Administration;

/// <summary>
/// Provides administrator-only authentication user management.
/// </summary>
[ApiController]
[Route("api/auth/admin/users")]
[Authorize]
public sealed class UsersController : ControllerBase
{
    private readonly IUserAdministrationService service;

    /// <summary>
    /// Initializes the controller.
    /// </summary>
    public UsersController(IUserAdministrationService service)
    {
        this.service = service;
    }

    /// <summary>
    /// Gets all authentication users.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = AuthorizationPolicyNames.UsersRead)]
    public async Task<ActionResult<IReadOnlyList<AuthUserAdministrationResponse>>> GetUsers(CancellationToken cancellationToken) => Ok(await service.GetUsersAsync(cancellationToken));

    /// <summary>
    /// Creates an authentication user.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicyNames.UsersCreate)]
    public async Task<ActionResult<AuthUserAdministrationResponse>> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        Guid actorUserId = GetRequiredUserId();
        AuthUserAdministrationResponse response = await service.CreateUserAsync(request, actorUserId, cancellationToken);

        return Created($"api/auth/admin/users/{response.Id}", response);
    }

    /// <summary>
    /// Updates an authentication user's profile.
    /// </summary>
    [HttpPut("{userId:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.UsersUpdate)]
    public async Task<ActionResult<AuthUserAdministrationResponse>> UpdateUser(Guid userId, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        AuthUserAdministrationResponse response = await service.UpdateUserAsync(userId, request, GetRequiredUserId(), cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Activates or deactivates an authentication user.
    /// </summary>
    [HttpPut("{userId:guid}/status")]
    [Authorize(Policy = AuthorizationPolicyNames.UsersManageStatus)]
    public async Task<IActionResult> SetStatus(Guid userId, [FromBody] SetUserStatusRequest request, CancellationToken cancellationToken)
    {
        await service.SetUserStatusAsync(userId, request, GetRequiredUserId(), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Resets an authentication user's password.
    /// </summary>
    [HttpPost("{userId:guid}/password/reset")]
    [Authorize(Policy = AuthorizationPolicyNames.UsersResetPassword)]
    public async Task<IActionResult> ResetPassword(Guid userId, [FromBody] ResetUserPasswordRequest request, CancellationToken cancellationToken)
    {
        await service.ResetUserPasswordAsync(userId, request, GetRequiredUserId(), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Replaces an authentication user's role assignments.
    /// </summary>
    [HttpPut("{userId:guid}/roles")]
    [Authorize(Policy = AuthorizationPolicyNames.UserRolesManage)]
    public async Task<IActionResult> SetRoles(Guid userId, [FromBody] SetUserRolesRequest request, CancellationToken cancellationToken)
    {
        await service.SetUserRolesAsync(userId, request, GetRequiredUserId(), cancellationToken);

        return NoContent();
    }

    private Guid GetRequiredUserId()
    {
        string? value = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(value, out Guid userId)) throw new InvalidOperationException("The authenticated user identifier is missing.");

        return userId;
    }
}
