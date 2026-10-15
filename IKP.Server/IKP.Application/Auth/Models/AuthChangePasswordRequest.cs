namespace IKP.Application.Auth.Models;

/// <summary>
/// Contains a request to change the authenticated user's password.
/// </summary>
public sealed class AuthChangePasswordRequest
{
    /// <summary>
    /// Gets or sets the current password.
    /// </summary>
    public string CurrentPassword { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the requested new password.
    /// </summary>
    public string NewPassword { get; set; } = string.Empty;
}
