namespace IKP.Application.Auth.Administration.Models;

/// <summary>Requests changes to an authentication user's profile.</summary>
public sealed class UpdateUserRequest
{
    /// <summary>Gets or sets the display name.</summary>
    public string DisplayName { get; set; } = null!;

    /// <summary>Gets or sets the optional email address.</summary>
    public string? Email { get; set; }
}
