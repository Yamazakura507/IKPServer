using Microsoft.AspNetCore.Authorization;

namespace IKP.Api.Authorization;

/// <summary>
/// Represents a required server authorization permission.
/// </summary>
public sealed class PermissionRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Initializes a permission requirement.
    /// </summary>
    /// <param name="permissionCode">The stable permission code.</param>
    public PermissionRequirement(string permissionCode)
    {
        if (string.IsNullOrWhiteSpace(permissionCode)) throw new ArgumentException("Permission code is required.", nameof(permissionCode));

        PermissionCode = permissionCode;
    }

    /// <summary>
    /// Gets the required permission code.
    /// </summary>
    public string PermissionCode { get; }
}
