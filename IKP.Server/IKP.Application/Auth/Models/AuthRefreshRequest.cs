namespace IKP.Application.Auth.Models;

/// <summary>
/// Contains a refresh-token request.
/// </summary>
public sealed class AuthRefreshRequest
{
    /// <summary>
    /// Gets or sets the opaque refresh token.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}
