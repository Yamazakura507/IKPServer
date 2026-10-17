namespace IKP.Application.Auth.Administration.Models;

/// <summary>Requests replacement of a role's permission assignments.</summary>
public sealed class SetRolePermissionsRequest
{
    /// <summary>Gets or sets the complete permission identifier set.</summary>
    public IReadOnlyCollection<Guid> PermissionIds { get; set; } = [];
}
