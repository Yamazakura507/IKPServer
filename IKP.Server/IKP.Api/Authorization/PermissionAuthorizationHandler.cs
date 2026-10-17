using Microsoft.AspNetCore.Authorization;

namespace IKP.Api.Authorization;

/// <summary>
/// Authorizes requests when the authenticated JWT contains the requested permission claim.
/// </summary>
public sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    /// <inheritdoc />
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User.Claims.Any(x => x.Type == "permission" && string.Equals(x.Value, requirement.PermissionCode, StringComparison.Ordinal)))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
