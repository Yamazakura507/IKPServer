namespace IKP.Application.Auth.Administration.Models;

/// <summary>Represents an administrator-facing authentication user.</summary>
public sealed class AuthUserAdministrationResponse
{
    /// <summary>Gets or sets the user identifier.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the username.</summary>
    public string UserName { get; set; } = null!;

    /// <summary>Gets or sets the email.</summary>
    public string? Email { get; set; }

    /// <summary>Gets or sets the display name.</summary>
    public string DisplayName { get; set; } = null!;

    /// <summary>Gets or sets whether the account is active.</summary>
    public bool IsActive { get; set; }

    /// <summary>Gets or sets whether the account is the bootstrap account.</summary>
    public bool IsBootstrapAccount { get; set; }

    /// <summary>Gets or sets the last successful login timestamp.</summary>
    public DateTime? LastLoginAt { get; set; }

    /// <summary>Gets or sets assigned roles.</summary>
    public IReadOnlyCollection<AuthRoleAdministrationResponse> Roles { get; set; } = [];
}
