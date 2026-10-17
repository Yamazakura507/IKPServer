using IKP.Application.Auth.Administration.Interfaces;
using IKP.Application.Auth.Administration.Models;
using IKP.Application.Auth.Exceptions;
using IKP.Application.Auth.Models;
using IKP.Domain.Common.Auth;
using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Auth.Credentials;
using IKP.Domain.Entities.Auth.Permissions;
using IKP.Domain.Entities.Auth.Roles;
using IKP.Domain.Entities.Auth.Sessions;
using IKP.Domain.Entities.Auth.Users;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.Auth.Administration;

/// <summary>
/// Implements administrator-only user and RBAC management.
/// </summary>
public sealed class UserAdministrationService : IUserAdministrationService
{
    private readonly AppDbContext db;
    private readonly IPasswordHasher<AuthUser> passwordHasher;
    private readonly AuthOptions options;

    /// <summary>
    /// Initializes the service.
    /// </summary>
    public UserAdministrationService(AppDbContext db, IPasswordHasher<AuthUser> passwordHasher, Microsoft.Extensions.Options.IOptions<AuthOptions> options)
    {
        this.db = db;
        this.passwordHasher = passwordHasher;
        this.options = options.Value;
    }

    /// <inheritdoc />
    public async ValueTask<IReadOnlyList<AuthUserAdministrationResponse>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        List<AuthUser> users = await db.AuthUsers.IgnoreQueryFilters().AsNoTracking()
            .Include(x => x.UserRoles).ThenInclude(x => x.Role)
            .OrderBy(x => x.UserName)
            .ToListAsync(cancellationToken);

        return users.Select(ToUserResponse).ToArray();
    }

    /// <inheritdoc />
    public async ValueTask<AuthUserAdministrationResponse> CreateUserAsync(CreateUserRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidateUser(request.UserName, request.DisplayName, request.Email);
        ValidatePassword(request.Password);

        string normalizedUserName = Normalize(request.UserName);
        string? normalizedEmail = NormalizeNullable(request.Email);

        if (await db.AuthUsers.IgnoreQueryFilters().AnyAsync(x => x.NormalizedUserName == normalizedUserName, cancellationToken))
            throw new AuthOperationException(409, "USERNAME_ALREADY_EXISTS", "The username is already in use.");
        if (normalizedEmail is not null && await db.AuthUsers.IgnoreQueryFilters().AnyAsync(x => x.NormalizedEmail == normalizedEmail, cancellationToken))
            throw new AuthOperationException(409, "EMAIL_ALREADY_EXISTS", "The email address is already in use.");

        List<AuthRole> roles = await GetRolesByIdsAsync(request.RoleIds, cancellationToken);

        await ValidateAssignableRolesAsync(roles, actorUserId, cancellationToken);

        AuthUser user = new()
        {
            UserName = request.UserName.Trim(),
            NormalizedUserName = normalizedUserName,
            Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim(),
            NormalizedEmail = normalizedEmail,
            DisplayName = request.DisplayName.Trim(),
            IsBootstrapAccount = false,
            IsActive = true
        };

        user.Credentials.Add(new IKP.Domain.Entities.Auth.Credentials.UserCredential
        {
            AuthenticationMethodId = await GetPasswordMethodIdAsync(cancellationToken),
            SecretHash = passwordHasher.HashPassword(user, request.Password),
            IsActive = true,
            MustChangePassword = true,
            CreatedAt = DateTime.UtcNow
        });

        db.AuthUsers.Add(user);
        AddUserRoles(user, roles, actorUserId);
        await db.SaveChangesAsync(cancellationToken);

        return ToUserResponse(await GetUserResponseAsync(user.Id, cancellationToken) ?? throw new InvalidOperationException("The newly created user could not be loaded."));
    }

    /// <inheritdoc />
    public async ValueTask<AuthUserAdministrationResponse> UpdateUserAsync(Guid userId, UpdateUserRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidateUserProfile(request.DisplayName, request.Email);

        AuthUser user = await GetUserForUpdateAsync(userId, cancellationToken);
        string? normalizedEmail = NormalizeNullable(request.Email);

        if (normalizedEmail is not null && await db.AuthUsers.IgnoreQueryFilters().AnyAsync(x => x.Id != userId && x.NormalizedEmail == normalizedEmail, cancellationToken))
            throw new AuthOperationException(409, "EMAIL_ALREADY_EXISTS", "The email address is already in use.");

        user.DisplayName = request.DisplayName.Trim();
        user.Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim();
        user.NormalizedEmail = normalizedEmail;
        user.UpdatedBy = actorUserId;

        await db.SaveChangesAsync(cancellationToken);

        return ToUserResponse(await GetUserResponseAsync(userId, cancellationToken) ?? throw new InvalidOperationException("The user could not be loaded."));
    }

    /// <inheritdoc />
    public async ValueTask SetUserStatusAsync(Guid userId, SetUserStatusRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        AuthUser user = await GetUserForUpdateAsync(userId, cancellationToken);

        if (user.IsBootstrapAccount && !request.IsActive)
            throw new AuthOperationException(400, "BOOTSTRAP_ACCOUNT_PROTECTED", "The bootstrap account cannot be deactivated.");
        if (user.Id == actorUserId && !request.IsActive)
            throw new AuthOperationException(400, "SELF_DEACTIVATION_FORBIDDEN", "The current administrator cannot deactivate its own account.");

        user.IsActive = request.IsActive;
        user.UpdatedBy = actorUserId;

        if (!request.IsActive)
        {
            foreach (UserSession session in await db.UserSessions.Where(x => x.UserId == userId && x.RevokedAt == null).ToListAsync(cancellationToken))
            {
                session.RevokedAt = DateTime.UtcNow;
            }
        }

        await db.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask ResetUserPasswordAsync(Guid userId, ResetUserPasswordRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidatePassword(request.Password);

        AuthUser user = await GetUserForUpdateAsync(userId, cancellationToken);
        UserCredential? credential = await db.UserCredentials
            .Include(x => x.AuthenticationMethod)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.AuthenticationMethod.Code == AuthenticationMethodCodes.Password, cancellationToken);

        if (credential is null) throw new AuthOperationException(409, "PASSWORD_CREDENTIAL_MISSING", "The user does not have a password credential.");

        credential.SecretHash = passwordHasher.HashPassword(user, request.Password);
        credential.UpdatedAt = DateTime.UtcNow;
        credential.MustChangePassword = true;
        user.UpdatedBy = actorUserId;

        foreach (UserSession session in await db.UserSessions.Where(x => x.UserId == userId && x.RevokedAt == null).ToListAsync(cancellationToken))
        {
            session.RevokedAt = DateTime.UtcNow;
        }

        await db.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask SetUserRolesAsync(Guid userId, SetUserRolesRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        AuthUser user = await GetUserForUpdateAsync(userId, cancellationToken);
        List<AuthRole> roles = await GetRolesByIdsAsync(request.RoleIds, cancellationToken);

        await ValidateAssignableRolesAsync(roles, actorUserId, cancellationToken);

        if (user.IsBootstrapAccount && !roles.Any(x => x.Code == AuthorizationRoleCodes.SystemAdministrator))
            throw new AuthOperationException(400, "BOOTSTRAP_ROLE_PROTECTED", "The bootstrap account must retain the system administrator role.");

        List<AuthUserRole> current = await db.Set<AuthUserRole>().Where(x => x.UserId == userId).ToListAsync(cancellationToken);

        db.RemoveRange(current);
        AddUserRoles(user, roles, actorUserId);

        foreach (UserSession session in await db.UserSessions.Where(x => x.UserId == userId && x.RevokedAt == null).ToListAsync(cancellationToken))
        {
            session.RevokedAt = DateTime.UtcNow;
        }

        await db.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<IReadOnlyList<AuthRoleAdministrationResponse>> GetRolesAsync(CancellationToken cancellationToken = default)
    {
        List<AuthRole> roles = await db.Set<AuthRole>().AsNoTracking()
            .Include(x => x.RolePermissions).ThenInclude(x => x.Permission)
            .OrderBy(x => x.SortOrder).ThenBy(x => x.Code)
            .ToListAsync(cancellationToken);

        return roles.Select(ToRoleResponse).ToArray();
    }

    /// <inheritdoc />
    public async ValueTask<AuthRoleAdministrationResponse> CreateRoleAsync(CreateRoleRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidateRole(request.Code, request.NameKey);

        string code = request.Code.Trim().ToUpperInvariant();

        if (await db.Set<AuthRole>().AnyAsync(x => x.Code == code, cancellationToken)) throw new AuthOperationException(409, "ROLE_ALREADY_EXISTS", "The role code is already in use.");

        List<AuthPermission> permissions = await GetPermissionsByIdsAsync(request.PermissionIds, cancellationToken);

        await ValidateAssignablePermissionsAsync(permissions, actorUserId, cancellationToken);

        AuthRole role = new()
        {
            Code = code,
            NameKey = request.NameKey.Trim(),
            DescriptionKey = string.IsNullOrWhiteSpace(request.DescriptionKey) ? null : request.DescriptionKey.Trim(),
            SortOrder = 1000,
            IsActive = true,
            IsSystem = false,
            CreatedBy = actorUserId
        };

        db.Set<AuthRole>().Add(role);
        AddRolePermissions(role, permissions);
        await db.SaveChangesAsync(cancellationToken);

        return ToRoleResponse(await db.Set<AuthRole>().Include(x => x.RolePermissions).ThenInclude(x => x.Permission).FirstAsync(x => x.Id == role.Id, cancellationToken));
    }

    /// <inheritdoc />
    public async ValueTask<AuthRoleAdministrationResponse> UpdateRoleAsync(Guid roleId, UpdateRoleRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        AuthRole role = await db.Set<AuthRole>().FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken) ?? throw new AuthOperationException(404, "ROLE_NOT_FOUND", "The role was not found.");
        
        if (role.IsSystem && (!string.Equals(role.NameKey, request.NameKey.Trim(), StringComparison.Ordinal) || role.IsActive != request.IsActive))
            throw new AuthOperationException(400, "SYSTEM_ROLE_PROTECTED", "System roles cannot be renamed or deactivated.");

        ValidateRoleName(request.NameKey);

        role.NameKey = request.NameKey.Trim();
        role.DescriptionKey = string.IsNullOrWhiteSpace(request.DescriptionKey) ? null : request.DescriptionKey.Trim();
        role.IsActive = request.IsActive;
        role.UpdatedBy = actorUserId;

        List<Guid> affectedUsers = await db.Set<AuthUserRole>().Where(x => x.RoleId == roleId).Select(x => x.UserId).ToListAsync(cancellationToken);

        foreach (UserSession session in await db.UserSessions.Where(x => affectedUsers.Contains(x.UserId) && x.RevokedAt == null).ToListAsync(cancellationToken))
        {
            session.RevokedAt = DateTime.UtcNow;
        }

        await db.SaveChangesAsync(cancellationToken);

        return ToRoleResponse(await db.Set<AuthRole>().Include(x => x.RolePermissions).ThenInclude(x => x.Permission).FirstAsync(x => x.Id == roleId, cancellationToken));
    }

    /// <inheritdoc />
    public async ValueTask SetRolePermissionsAsync(Guid roleId, SetRolePermissionsRequest request, Guid actorUserId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        AuthRole role = await db.Set<AuthRole>().FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken) ?? throw new AuthOperationException(404, "ROLE_NOT_FOUND", "The role was not found.");
        
        if (role.IsSystem) throw new AuthOperationException(400, "SYSTEM_ROLE_PROTECTED", "System roles cannot have their permissions changed.");

        List<AuthPermission> permissions = await GetPermissionsByIdsAsync(request.PermissionIds, cancellationToken);

        await ValidateAssignablePermissionsAsync(permissions, actorUserId, cancellationToken);

        List<AuthRolePermission> current = await db.Set<AuthRolePermission>().Where(x => x.RoleId == roleId).ToListAsync(cancellationToken);

        db.RemoveRange(current);
        AddRolePermissions(role, permissions);

        List<Guid> affectedUsers = await db.Set<AuthUserRole>().Where(x => x.RoleId == roleId).Select(x => x.UserId).ToListAsync(cancellationToken);

        foreach (UserSession session in await db.UserSessions.Where(x => affectedUsers.Contains(x.UserId) && x.RevokedAt == null).ToListAsync(cancellationToken))
        {
            session.RevokedAt = DateTime.UtcNow;
        }

        role.UpdatedBy = actorUserId;

        await db.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<IReadOnlyList<AuthPermissionAdministrationResponse>> GetPermissionsAsync(CancellationToken cancellationToken = default)
    {
        List<AuthPermission> permissions = await db.Set<AuthPermission>().AsNoTracking().OrderBy(x => x.SortOrder).ThenBy(x => x.Code).ToListAsync(cancellationToken);
        
        return permissions.Select(ToPermissionResponse).ToArray();
    }

    private async ValueTask<AuthUser?> GetUserResponseAsync(Guid userId, CancellationToken cancellationToken) => 
        await db.AuthUsers.IgnoreQueryFilters().AsNoTracking().Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

    private async ValueTask<AuthUser> GetUserForUpdateAsync(Guid userId, CancellationToken cancellationToken) => 
        await db.AuthUsers.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == userId, cancellationToken) ?? throw new AuthOperationException(404, "USER_NOT_FOUND", "The user was not found.");

    private ValueTask<List<AuthRole>> GetRolesByIdsAsync(IReadOnlyCollection<Guid> roleIds, CancellationToken cancellationToken)
        => GetAuthLookupByIdsAsync<AuthRole>(roleIds, "role", cancellationToken);

    private ValueTask<List<AuthPermission>> GetPermissionsByIdsAsync(IReadOnlyCollection<Guid> permissionIds, CancellationToken cancellationToken) 
        => GetAuthLookupByIdsAsync<AuthPermission>(permissionIds, "permission", cancellationToken);

    private async ValueTask<List<T>> GetAuthLookupByIdsAsync<T>(IReadOnlyCollection<Guid> lookupIds, string exeptionTitle, CancellationToken cancellationToken) where T : FullLookupLocalized
    {
        Guid[] ids = lookupIds.Distinct().ToArray();

        if (ids.Length == 0) return [];

        List<T> lookups = await db.Set<T>().Where(x => ids.Contains(x.Id) && x.IsActive).ToListAsync(cancellationToken);

        if (lookups.Count != ids.Length) throw new AuthOperationException(400, $"{exeptionTitle.ToUpper()}_ASSIGNMENT_INVALID", $"One or more requested {exeptionTitle.ToLower()}s are missing or inactive.");

        return lookups;
    }


    private async ValueTask<Guid> GetPasswordMethodIdAsync(CancellationToken cancellationToken)
    {
        Guid? id = await db.AuthenticationMethods.Where(x => x.Code == AuthenticationMethodCodes.Password && x.IsActive).Select(x => (Guid?)x.Id).FirstOrDefaultAsync(cancellationToken);
        
        return id ?? throw new InvalidOperationException("The PASSWORD authentication method has not been seeded.");
    }

    private static void AddUserRoles(AuthUser user, IEnumerable<AuthRole> roles, Guid actorUserId)
    {
        foreach (AuthRole role in roles)
        {
            user.UserRoles.Add(new AuthUserRole { UserId = user.Id, RoleId = role.Id, CreatedAt = DateTime.UtcNow, CreatedBy = actorUserId });
        }
    }

    private static void AddRolePermissions(AuthRole role, IEnumerable<AuthPermission> permissions)
    {
        foreach (AuthPermission permission in permissions)
        {
            role.RolePermissions.Add(new AuthRolePermission { RoleId = role.Id, PermissionId = permission.Id, CreatedAt = DateTime.UtcNow });
        }
    }

    private async ValueTask ValidateAssignableRolesAsync(IEnumerable<AuthRole> roles, Guid actorUserId, CancellationToken cancellationToken)
    {
        if (!roles.Any(x => x.IsSystem)) return;
        if (!await IsSystemAdministratorAsync(actorUserId, cancellationToken))
            throw new AuthOperationException(403, "SYSTEM_ROLE_ASSIGNMENT_FORBIDDEN", "System roles can only be assigned by a system administrator.");
        if (roles.Any(x => x.Code != AuthorizationRoleCodes.SystemAdministrator))
            throw new AuthOperationException(400, "SYSTEM_ROLE_ASSIGNMENT_INVALID", "The requested system role is not assignable through user management.");
    }

    private async ValueTask ValidateAssignablePermissionsAsync(IEnumerable<AuthPermission> permissions, Guid actorUserId, CancellationToken cancellationToken)
    {
        if (!permissions.Any(x => x.Code == AuthorizationPermissionCodes.SystemAdmin)) return;
        if (!await IsSystemAdministratorAsync(actorUserId, cancellationToken))
            throw new AuthOperationException(403, "SYSTEM_PERMISSION_ASSIGNMENT_FORBIDDEN", "The system administrator permission can only be assigned by a system administrator.");
    }

    private async ValueTask<bool> IsSystemAdministratorAsync(Guid userId, CancellationToken cancellationToken) 
        => await db.AuthUserRoles.AnyAsync(x => x.UserId == userId && x.Role.IsActive && x.Role.Code == AuthorizationRoleCodes.SystemAdministrator, cancellationToken);

    private static void ValidateUser(string userName, string displayName, string? email)
    {
        if (string.IsNullOrWhiteSpace(userName) || userName.Trim().Length > 100)
            throw new AuthOperationException(400, "USERNAME_INVALID", "The username is required and must not exceed 100 characters.");

        ValidateUserProfile(displayName, email);
    }

    private static void ValidateUserProfile(string displayName, string? email)
    {
        if (string.IsNullOrWhiteSpace(displayName) || displayName.Trim().Length > 200)
            throw new AuthOperationException(400, "DISPLAY_NAME_INVALID", "The display name is required and must not exceed 200 characters.");
        if (!string.IsNullOrWhiteSpace(email) && (email.Trim().Length > 320 || !email.Contains('@'))) 
            throw new AuthOperationException(400, "EMAIL_INVALID", "The email address is invalid.");
    }

    private void ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < options.MinimumPasswordLength || password.Length > 1024)
            throw new AuthOperationException(400, "PASSWORD_POLICY_FAILED", $"The password must contain between {options.MinimumPasswordLength} and 1024 characters.");
    }

    private static void ValidateRole(string code, string nameKey)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Trim().Length > 100 || code.Any(char.IsWhiteSpace))
            throw new AuthOperationException(400, "ROLE_CODE_INVALID", "The role code is required, must not exceed 100 characters, and must not contain whitespace.");
        
        ValidateRoleName(nameKey);
    }

    private static void ValidateRoleName(string nameKey)
    {
        if (string.IsNullOrWhiteSpace(nameKey) || nameKey.Trim().Length > 200) 
            throw new AuthOperationException(400, "ROLE_NAME_INVALID", "The role name localization key is required and must not exceed 200 characters.");
    }

    private static string Normalize(string value) => value.Trim().ToUpperInvariant();

    private static string? NormalizeNullable(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim().ToUpperInvariant();

    private static AuthUserAdministrationResponse ToUserResponse(AuthUser user) => new()
    {
        Id = user.Id,
        UserName = user.UserName,
        Email = user.Email,
        DisplayName = user.DisplayName,
        IsActive = user.IsActive,
        IsBootstrapAccount = user.IsBootstrapAccount,
        LastLoginAt = user.LastLoginAt,
        Roles = user.UserRoles.Select(x => ToRoleResponse(x.Role)).ToArray()
    };

    private static AuthRoleAdministrationResponse ToRoleResponse(AuthRole role) => new()
    {
        Id = role.Id,
        Code = role.Code,
        NameKey = role.NameKey,
        DescriptionKey = role.DescriptionKey,
        IsActive = role.IsActive,
        IsSystem = role.IsSystem,
        Permissions = role.RolePermissions.Select(x => ToPermissionResponse(x.Permission)).ToArray()
    };

    private static AuthPermissionAdministrationResponse ToPermissionResponse(AuthPermission permission) => new()
    {
        Id = permission.Id,
        Code = permission.Code,
        NameKey = permission.NameKey,
        DescriptionKey = permission.DescriptionKey,
        IsActive = permission.IsActive,
        IsSystem = permission.IsSystem
    };
}
