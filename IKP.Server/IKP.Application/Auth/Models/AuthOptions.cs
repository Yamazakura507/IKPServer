namespace IKP.Application.Auth.Models;

/// <summary>
/// Configures authentication token lifetimes and sign-in protection.
/// </summary>
public sealed class AuthOptions
{
    /// <summary>
    /// Gets or sets the JWT issuer.
    /// </summary>
    public string Issuer { get; set; } = "IKP.Server";

    /// <summary>
    /// Gets or sets the JWT audience.
    /// </summary>
    public string Audience { get; set; } = "IKP.Client";

    /// <summary>
    /// Gets or sets the access-token lifetime in minutes.
    /// </summary>
    public int AccessTokenMinutes { get; set; } = 10;

    /// <summary>
    /// Gets or sets the refresh-token lifetime in days.
    /// </summary>
    public int RefreshTokenDays { get; set; } = 30;

    /// <summary>
    /// Gets or sets the maximum consecutive failed password attempts.
    /// </summary>
    public int MaximumFailedLoginAttempts { get; set; } = 5;

    /// <summary>
    /// Gets or sets the account lockout duration in minutes.
    /// </summary>
    public int LockoutMinutes { get; set; } = 15;

    /// <summary>
    /// Gets or sets the minimum password length.
    /// </summary>
    public int MinimumPasswordLength { get; set; } = 12;

    /// <summary>
    /// Gets or sets the one-time secret that permits initial administrator creation.
    /// </summary>
    public string? BootstrapSecret { get; set; }

    /// <summary>
    /// Gets or sets the signing key. Configure it outside source control.
    /// </summary>
    public string? SigningKey { get; set; }
}
