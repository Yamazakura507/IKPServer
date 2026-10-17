using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Auth.Permissions;

/// <summary>
/// Defines an authorization permission identified by a stable machine-readable code.
/// </summary>
public class AuthPermission : FullLookupLocalized
{
    /// <summary>
    /// Gets roles that grant this permission.
    /// </summary>
    public ICollection<AuthRolePermission> RolePermissions { get; set; } = new List<AuthRolePermission>();
}
