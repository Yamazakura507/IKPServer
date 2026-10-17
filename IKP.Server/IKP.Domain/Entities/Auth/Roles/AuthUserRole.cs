using IKP.Domain.Entities.Auth.Users;
using IKP.Domain.Common.Interfaces.Audit;

namespace IKP.Domain.Entities.Auth.Roles;

/// <summary>
/// Links an authentication user to an authorization role.
/// </summary>
public class AuthUserRole : IAuditable, ISoftDelete
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the role identifier.
    /// </summary>
    public Guid RoleId { get; set; }

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
    /// Gets the user navigation property.
    /// </summary>
    public AuthUser User { get; set; } = null!;

    /// <summary>
    /// Gets the role navigation property.
    /// </summary>
    public AuthRole Role { get; set; } = null!;
}
