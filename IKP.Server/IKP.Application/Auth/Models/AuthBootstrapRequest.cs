namespace IKP.Application.Auth.Models;

/// <summary>
/// Contains the one-time first-administrator bootstrap request.
/// </summary>
public sealed class AuthBootstrapRequest
{
    /// <summary>
    /// Gets or sets the configured one-time bootstrap secret.
    /// </summary>
    public string BootstrapSecret { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the initial administrator sign-in name.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the optional email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the initial password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
