using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Auth.Users;

namespace IKP.Domain.Entities.Auth.Sessions;

/// <summary>
/// Represents a refresh-token session. Refresh tokens are stored only as SHA-256 hashes.
/// </summary>
public class UserSession : Entity
{
    /// <summary>
    /// Gets or sets the owning user identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the SHA-256 hash of the current refresh token.
    /// </summary>
    public string RefreshTokenHash { get; set; } = null!;

    /// <summary>
    /// Gets or sets the creation timestamp in UTC.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the expiry timestamp in UTC.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Gets or sets the last successful refresh timestamp in UTC.
    /// </summary>
    public DateTime? LastUsedAt { get; set; }

    /// <summary>
    /// Gets or sets the revocation timestamp in UTC.
    /// </summary>
    public DateTime? RevokedAt { get; set; }

    /// <summary>
    /// Gets or sets the next session in the refresh-token rotation chain.
    /// </summary>
    public Guid? ReplacedBySessionId { get; set; }

    /// <summary>
    /// Gets or sets the client IP address when available.
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the user-agent header when available.
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// Gets or sets the owning user navigation property.
    /// </summary>
    public AuthUser User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the next session navigation property.
    /// </summary>
    public UserSession? ReplacedBySession { get; set; }
}
