namespace IKP.Application.Auth.Models;

/// <summary>Contains a password sign-in request.</summary>
public sealed class AuthLoginRequest
{
    /// <summary>
    /// Gets or sets the username or email address.
    /// </summary>
    public string UserNameOrEmail { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
