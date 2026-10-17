namespace IKP.Application.Auth.Administration.Models;

/// <summary>Requests an administrator password reset.</summary>
public sealed class ResetUserPasswordRequest
{
    /// <summary>Gets or sets the new password.</summary>
    public string Password { get; set; } = null!;
}
