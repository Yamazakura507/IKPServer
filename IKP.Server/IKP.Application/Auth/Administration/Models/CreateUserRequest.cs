namespace IKP.Application.Auth.Administration.Models;

/// <summary>Requests creation of an authentication user.</summary>
public sealed class CreateUserRequest
{
    /// <summary>Gets or sets the sign-in name.</summary>
    public string UserName { get; set; } = null!;

    /// <summary>Gets or sets the optional email address.</summary>
    public string? Email { get; set; }

    /// <summary>Gets or sets the display name.</summary>
    public string DisplayName { get; set; } = null!;

    /// <summary>Gets or sets the initial password.</summary>
    public string Password { get; set; } = null!;

    /// <summary>Gets or sets role identifiers assigned at creation.</summary>
    public IReadOnlyCollection<Guid> RoleIds { get; set; } = [];
}
