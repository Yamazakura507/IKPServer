namespace IKP.Application.Auth.Administration.Models;

/// <summary>Represents an administrator-facing authorization role.</summary>
public sealed class AuthRoleAdministrationResponse
{
    /// <summary>Gets or sets the role identifier.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the stable role code.</summary>
    public string Code { get; set; } = null!;

    /// <summary>Gets or sets the localization key for the role name.</summary>
    public string NameKey { get; set; } = null!;

    /// <summary>Gets or sets the localization key for the role description.</summary>
    public string? DescriptionKey { get; set; }

    /// <summary>Gets or sets whether the role is active.</summary>
    public bool IsActive { get; set; }

    /// <summary>Gets or sets whether the role is system-defined.</summary>
    public bool IsSystem { get; set; }

    /// <summary>Gets or sets assigned permissions.</summary>
    public IReadOnlyCollection<AuthPermissionAdministrationResponse> Permissions { get; set; } = [];
}
