namespace IKP.Application.Auth.Models;

/// <summary>
/// Contains the public, non-secret profile returned after authentication.
/// </summary>
public sealed class AuthUserSummary
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sign-in name.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;
}
