using IKP.Domain.Entities.Auth.Roles;
using IKP.Domain.Common.Interfaces.Audit;

namespace IKP.Domain.Entities.Auth.Permissions;

/// <summary>
/// Links an authorization role to a permission.
/// </summary>
public class AuthRolePermission : IAuditable, ISoftDelete
{
    /// <summary>
    /// Gets or sets the role identifier.
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// Gets or sets the permission identifier.
    /// </summary>
    public Guid PermissionId { get; set; }

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
    /// Gets the role navigation property.
    /// </summary>
    public AuthRole Role { get; set; } = null!;

    /// <summary>
    /// Gets the permission navigation property.
    /// </summary>
    public AuthPermission Permission { get; set; } = null!;
    
}
