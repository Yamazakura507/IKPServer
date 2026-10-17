namespace IKP.Application.Auth.Administration.Models;

/// <summary>Requests replacement of a user's role assignments.</summary>
public sealed class SetUserRolesRequest
{
    /// <summary>Gets or sets the complete role identifier set.</summary>
    public IReadOnlyCollection<Guid> RoleIds { get; set; } = [];
}
