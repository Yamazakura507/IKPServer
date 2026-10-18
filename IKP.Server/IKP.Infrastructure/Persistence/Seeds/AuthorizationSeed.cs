using IKP.Domain.Common.Auth;
using IKP.Domain.Entities.Auth.Permissions;
using IKP.Domain.Entities.Auth.Roles;
using IKP.Infrastructure.Persistence.Seeds.Records;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds;

/// <summary>Seeds system authorization roles and permissions.</summary>
public static class AuthorizationSeed
{
    private const string PathBaseKey = "Auth.Authorization";
    private const string PathRoleKey = $"{PathBaseKey}.Role.Permission";
    internal const string PathPermissionKey = $"{PathBaseKey}";
    internal const uint BaseIndex = 8;

    internal static readonly Guid SystemAdminRoleId = BaseIndex.GetGuidForLevel(0, 1);
    private static readonly Guid SystemAdminPermissionId = BaseIndex.GetGuidForLevel(1, 1);
    private static readonly Guid UsersReadPermissionId = BaseIndex.GetGuidForLevel(1, 2);
    private static readonly Guid UsersCreatePermissionId = BaseIndex.GetGuidForLevel(1, 3);
    private static readonly Guid UsersUpdatePermissionId = BaseIndex.GetGuidForLevel(1, 4);
    private static readonly Guid UsersManageStatusPermissionId = BaseIndex.GetGuidForLevel(1, 5);
    private static readonly Guid UsersResetPasswordPermissionId = BaseIndex.GetGuidForLevel(1, 6);
    private static readonly Guid RolesReadPermissionId = BaseIndex.GetGuidForLevel(1, 7);
    private static readonly Guid RolesManagePermissionId = BaseIndex.GetGuidForLevel(1, 8);
    private static readonly Guid UserRolesManagePermissionId = BaseIndex.GetGuidForLevel(1, 9);
    private static readonly Guid PermissionsReadPermissionId = BaseIndex.GetGuidForLevel(1, 10);
    private static readonly Guid RolePermissionsManagePermissionId = BaseIndex.GetGuidForLevel(1, 11);
    private static readonly Guid AuditReadPermissionId = BaseIndex.GetGuidForLevel(1, 12);
    private static readonly Guid AuditRollbackPermissionId = BaseIndex.GetGuidForLevel(1, 13);
    private static readonly Guid DiagnosticsReadPermissionId = BaseIndex.GetGuidForLevel(1, 14);
    private static readonly Guid DiagnosticsManagePermissionId = BaseIndex.GetGuidForLevel(1, 15);
    private static readonly Guid EmployeesReadPermissionId = BaseIndex.GetGuidForLevel(1, 16);
    private static readonly Guid EmployeesManagePermissionId = BaseIndex.GetGuidForLevel(1, 17);
    private static readonly Guid FilesReadPermissionId = BaseIndex.GetGuidForLevel(1, 18);
    private static readonly Guid FilesCreatePermissionId = BaseIndex.GetGuidForLevel(1, 19);
    private static readonly Guid FilesDeletePermissionId = BaseIndex.GetGuidForLevel(1, 20);

    /// <summary>
    /// Adds the built-in authorization definitions to the EF model.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    public static void Seed(ModelBuilder modelBuilder)
    {
        SeedRecord<AuthRole> role = new(SystemAdminRoleId, AuthorizationRoleCodes.SystemAdministrator, Configure: entity => entity.CreatedAt = DateTime.UnixEpoch);
        SeedRecord<AuthPermission>[] permissions =
            {
                new SeedRecord<AuthPermission>(SystemAdminPermissionId, AuthorizationPermissionCodes.SystemAdmin),
                new SeedRecord<AuthPermission>(UsersReadPermissionId, AuthorizationPermissionCodes.UsersRead),
                new SeedRecord<AuthPermission>(UsersCreatePermissionId, AuthorizationPermissionCodes.UsersCreate),
                new SeedRecord<AuthPermission>(UsersUpdatePermissionId, AuthorizationPermissionCodes.UsersUpdate),
                new SeedRecord<AuthPermission>(UsersManageStatusPermissionId, AuthorizationPermissionCodes.UsersManageStatus),
                new SeedRecord<AuthPermission>(UsersResetPasswordPermissionId, AuthorizationPermissionCodes.UsersResetPassword),
                new SeedRecord<AuthPermission>(RolesReadPermissionId, AuthorizationPermissionCodes.RolesRead),
                new SeedRecord<AuthPermission>(RolesManagePermissionId, AuthorizationPermissionCodes.RolesManage),
                new SeedRecord<AuthPermission>(UserRolesManagePermissionId, AuthorizationPermissionCodes.UserRolesManage),
                new SeedRecord<AuthPermission>(PermissionsReadPermissionId, AuthorizationPermissionCodes.PermissionsRead),
                new SeedRecord<AuthPermission>(RolePermissionsManagePermissionId, AuthorizationPermissionCodes.RolePermissionsManage),
                new SeedRecord<AuthPermission>(AuditReadPermissionId, AuthorizationPermissionCodes.AuditRead),
                new SeedRecord<AuthPermission>(AuditRollbackPermissionId, AuthorizationPermissionCodes.AuditRollback),
                new SeedRecord<AuthPermission>(DiagnosticsReadPermissionId, AuthorizationPermissionCodes.DiagnosticsRead),
                new SeedRecord<AuthPermission>(DiagnosticsManagePermissionId, AuthorizationPermissionCodes.DiagnosticsManage),
                new SeedRecord<AuthPermission>(EmployeesReadPermissionId, AuthorizationPermissionCodes.EmployeesRead),
                new SeedRecord<AuthPermission>(EmployeesManagePermissionId, AuthorizationPermissionCodes.EmployeesManage),
                new SeedRecord<AuthPermission>(FilesReadPermissionId, AuthorizationPermissionCodes.FilesRead),
                new SeedRecord<AuthPermission>(FilesCreatePermissionId, AuthorizationPermissionCodes.FilesCreate),
                new SeedRecord<AuthPermission>(FilesDeletePermissionId, AuthorizationPermissionCodes.FilesDelete)
            };

        modelBuilder.Entity<AuthRole>().HasData(role.CreateDefinition(PathRoleKey));
        modelBuilder.Entity<AuthPermission>().HasData(permissions.CreateListDefinition(PathPermissionKey));
        modelBuilder.Entity<AuthRolePermission>().HasData(permissions.Select(permission => new AuthRolePermission
        {
            RoleId = role.Id,
            PermissionId = permission.Id,
            CreatedAt = DateTime.UnixEpoch
        }));
    }
}
