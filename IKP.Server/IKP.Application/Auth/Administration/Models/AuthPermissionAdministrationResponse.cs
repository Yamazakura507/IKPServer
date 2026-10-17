namespace IKP.Application.Auth.Administration.Models;

/// <summary>Represents an authorization permission.</summary>
public sealed class AuthPermissionAdministrationResponse
{
    /// <summary>Gets or sets the permission identifier.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the stable permission code.</summary>
    public string Code { get; set; } = null!;

    /// <summary>Gets or sets the localization key for the permission name.</summary>
    public string NameKey { get; set; } = null!;

    /// <summary>Gets or sets the localization key for the permission description.</summary>
    public string? DescriptionKey { get; set; }

    /// <summary>Gets or sets whether the permission is active.</summary>
    public bool IsActive { get; set; }

    /// <summary>Gets or sets whether the permission is system-defined.</summary>
    public bool IsSystem { get; set; }
}
