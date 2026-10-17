using IKP.Application.Auth.Administration.Models;

namespace IKP.Application.Auth.Administration.Interfaces;

/// <summary>Provides administrator-only user and RBAC management operations.</summary>
public interface IUserAdministrationService
{
    /// <summary>Gets users with their assigned roles.</summary>
    ValueTask<IReadOnlyList<AuthUserAdministrationResponse>> GetUsersAsync(CancellationToken cancellationToken = default);

    /// <summary>Creates a user and optionally assigns roles.</summary>
    ValueTask<AuthUserAdministrationResponse> CreateUserAsync(CreateUserRequest request, Guid actorUserId, CancellationToken cancellationToken = default);

    /// <summary>Updates a user's profile.</summary>
    ValueTask<AuthUserAdministrationResponse> UpdateUserAsync(Guid userId, UpdateUserRequest request, Guid actorUserId, CancellationToken cancellationToken = default);

    /// <summary>Changes a user's active state.</summary>
    ValueTask SetUserStatusAsync(Guid userId, SetUserStatusRequest request, Guid actorUserId, CancellationToken cancellationToken = default);

    /// <summary>Resets a user's password and revokes refresh sessions.</summary>
    ValueTask ResetUserPasswordAsync(Guid userId, ResetUserPasswordRequest request, Guid actorUserId, CancellationToken cancellationToken = default);

    /// <summary>Replaces a user's assigned roles.</summary>
    ValueTask SetUserRolesAsync(Guid userId, SetUserRolesRequest request, Guid actorUserId, CancellationToken cancellationToken = default);

    /// <summary>Gets all authorization roles.</summary>
    ValueTask<IReadOnlyList<AuthRoleAdministrationResponse>> GetRolesAsync(CancellationToken cancellationToken = default);

    /// <summary>Creates a custom authorization role.</summary>
    ValueTask<AuthRoleAdministrationResponse> CreateRoleAsync(CreateRoleRequest request, Guid actorUserId, CancellationToken cancellationToken = default);

    /// <summary>Updates a custom authorization role.</summary>
    ValueTask<AuthRoleAdministrationResponse> UpdateRoleAsync(Guid roleId, UpdateRoleRequest request, Guid actorUserId, CancellationToken cancellationToken = default);

    /// <summary>Replaces the permissions assigned to a role.</summary>
    ValueTask SetRolePermissionsAsync(Guid roleId, SetRolePermissionsRequest request, Guid actorUserId, CancellationToken cancellationToken = default);

    /// <summary>Gets all authorization permissions.</summary>
    ValueTask<IReadOnlyList<AuthPermissionAdministrationResponse>> GetPermissionsAsync(CancellationToken cancellationToken = default);
}
