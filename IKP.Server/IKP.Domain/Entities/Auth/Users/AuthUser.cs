using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Audit;
using IKP.Domain.Entities.Auth.Credentials;
using IKP.Domain.Entities.Auth.Sessions;
using IKP.Domain.Entities.Auth.Roles;

namespace IKP.Domain.Entities.Auth.Users;

/// <summary>
/// Represents a person or service identity that can authenticate with the server.
/// </summary>
public class AuthUser : Entity, IAuditable, ISoftDelete
{
    /// <summary>
    /// Gets or sets the sign-in name.
    /// </summary>
    public string UserName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the normalized sign-in name used for unique lookups.
    /// </summary>
    public string NormalizedUserName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the optional email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the normalized email address.
    /// </summary>
    public string? NormalizedEmail { get; set; }

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    public string DisplayName { get; set; } = null!;

    /// <summary>
    /// Gets or sets whether this is the immutable first account created by one-time bootstrap.
    /// </summary>
    public bool IsBootstrapAccount { get; set; }

    /// <summary>
    /// Gets or sets whether the account may authenticate.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the number of consecutive failed password attempts.
    /// </summary>
    public int FailedLoginCount { get; set; }

    /// <summary>
    /// Gets or sets the end of the current sign-in lockout.
    /// </summary>
    public DateTime? LockoutEndAt { get; set; }

    /// <summary>
    /// Gets or sets the time of the most recent successful sign-in.
    /// </summary>
    public DateTime? LastLoginAt { get; set; }

    /// <inheritdoc />
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc />
    public Guid? CreatedBy { get; set; }

    /// <inheritdoc />
    public DateTime? UpdatedAt { get; set; }

    /// <inheritdoc />
    public Guid? UpdatedBy { get; set; }

    /// <inheritdoc />
    public bool IsDeleted { get; set; }

    /// <inheritdoc />
    public DateTime? DeletedAt { get; set; }

    /// <inheritdoc />
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// Gets the credentials registered for this account.
    /// </summary>
    public ICollection<UserCredential> Credentials { get; set; } = new List<UserCredential>();

    /// <summary>
    /// Gets the sessions issued to this account.
    /// </summary>
    public ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();

    /// <summary>
    /// Gets the authorization roles assigned to this account.
    /// </summary>
    public ICollection<AuthUserRole> UserRoles { get; set; } = new List<AuthUserRole>();
}
