using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Audit;
using IKP.Domain.Entities.Auth.Permissions;

namespace IKP.Domain.Entities.Auth.Roles;

/// <summary>
/// Defines a named authorization role that can be assigned to users.
/// </summary>
public class AuthRole : FullLookupLocalized, IAuditable, ISoftDelete
{
    /// <inheritdoc/>
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc/>
    public Guid? CreatedBy { get; set; }

    /// <inheritdoc/>
    public DateTime? UpdatedAt { get; set; }

    /// <inheritdoc/>
    public Guid? UpdatedBy { get; set; }

    /// <inheritdoc />
    public bool IsDeleted { get; set; }

    /// <inheritdoc />
    public DateTime? DeletedAt { get; set; }

    /// <inheritdoc />
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// Gets users assigned to this role.
    /// </summary>
    public ICollection<AuthUserRole> UserRoles { get; set; } = new List<AuthUserRole>();

    /// <summary>
    /// Gets permissions granted by this role.
    /// </summary>
    public ICollection<AuthRolePermission> RolePermissions { get; set; } = new List<AuthRolePermission>();
}
