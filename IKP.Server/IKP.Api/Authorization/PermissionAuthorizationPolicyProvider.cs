using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace IKP.Api.Authorization;

/// <summary>
/// Creates authorization policies dynamically from stable permission codes.
/// </summary>
public sealed class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    /// <summary>
    /// Initializes the provider.
    /// </summary>
    /// <param name="options">Authorization options.</param>
    public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    /// <inheritdoc />
    public override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddRequirements(new PermissionRequirement(policyName)).Build();

        return Task.FromResult<AuthorizationPolicy?>(policy);
    }
}
