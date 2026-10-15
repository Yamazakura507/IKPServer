namespace IKP.Application.Auth.Models;

/// <summary>
/// Contains access and refresh tokens issued to a user.
/// </summary>
public sealed class AuthTokenResponse
{
    /// <summary>
    /// Gets or sets the short-lived bearer access token.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the opaque refresh token.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the access-token lifetime in seconds.
    /// </summary>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// Gets or sets the authenticated user profile.
    /// </summary>
    public AuthUserSummary User { get; set; } = new();
}
