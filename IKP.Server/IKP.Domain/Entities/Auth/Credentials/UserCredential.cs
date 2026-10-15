using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Auth.Lookup;
using IKP.Domain.Entities.Auth.Users;

namespace IKP.Domain.Entities.Auth.Credentials;

/// <summary>
/// Stores a password hash or another method-specific credential without exposing secrets to audit logs.
/// </summary>
public class UserCredential : Entity
{
    /// <summary>
    /// Gets or sets the owner user identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the authentication method identifier.
    /// </summary>
    public Guid AuthenticationMethodId { get; set; }

    /// <summary>
    /// Gets or sets the encoded credential hash.
    /// </summary>
    public string SecretHash { get; set; } = null!;

    /// <summary>
    /// Gets or sets whether the credential is enabled.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the user must change the password at the next sign-in.
    /// </summary>
    public bool MustChangePassword { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp in UTC.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the most recent credential update timestamp in UTC.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the most recent successful use timestamp in UTC.
    /// </summary>
    public DateTime? LastUsedAt { get; set; }

    /// <summary>
    /// Gets or sets the user navigation property.
    /// </summary>
    public AuthUser User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the authentication method navigation property.
    /// </summary>
    public AuthenticationMethodDefinition AuthenticationMethod { get; set; } = null!;
}
