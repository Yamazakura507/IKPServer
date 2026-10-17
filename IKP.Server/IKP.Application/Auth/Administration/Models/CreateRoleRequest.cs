namespace IKP.Application.Auth.Administration.Models;

/// <summary>Requests creation of a custom authorization role.</summary>
public sealed class CreateRoleRequest
{
    /// <summary>Gets or sets the stable role code.</summary>
    public string Code { get; set; } = null!;

    /// <summary>Gets or sets the localization key for the role name.</summary>
    public string NameKey { get; set; } = null!;

    /// <summary>Gets or sets the optional localization key for the role description.</summary>
    public string? DescriptionKey { get; set; }

    /// <summary>Gets or sets the permission identifiers granted by the role.</summary>
    public IReadOnlyCollection<Guid> PermissionIds { get; set; } = [];
}
