namespace IKP.Application.Auth.Administration.Models;

/// <summary>Requests changes to a custom authorization role.</summary>
public sealed class UpdateRoleRequest
{
    /// <summary>Gets or sets the localization key for the role name.</summary>
    public string NameKey { get; set; } = null!;

    /// <summary>Gets or sets the optional localization key for the role description.</summary>
    public string? DescriptionKey { get; set; }

    /// <summary>Gets or sets whether the role is active.</summary>
    public bool IsActive { get; set; }
}
