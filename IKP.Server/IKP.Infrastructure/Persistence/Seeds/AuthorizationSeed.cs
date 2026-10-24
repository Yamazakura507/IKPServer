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
    private static readonly Guid LocalizationReadPermissionId = BaseIndex.GetGuidForLevel(1, 30);
    private static readonly Guid LocalizationManagePermissionId = BaseIndex.GetGuidForLevel(1, 31);
    private static readonly Guid UpdatesReadPermissionId = BaseIndex.GetGuidForLevel(1, 32);
    private static readonly Guid UpdatesManagePermissionId = BaseIndex.GetGuidForLevel(1, 33);
    private static readonly Guid ReportsReadPermissionId = BaseIndex.GetGuidForLevel(1, 34);
    private static readonly Guid ReportsManagePermissionId = BaseIndex.GetGuidForLevel(1, 35);
    private static readonly Guid ReportsGeneratePermissionId = BaseIndex.GetGuidForLevel(1, 36);
    private static readonly Guid SystemReadPermissionId = BaseIndex.GetGuidForLevel(1, 37);
    private static readonly Guid SystemManagePermissionId = BaseIndex.GetGuidForLevel(1, 38);

    /// <summary>
    /// Adds the built-in authorization definitions to the EF model.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    public static void Seed(ModelBuilder modelBuilder)
    {
        SeedRecord<AuthRole> role = new(SystemAdminRoleId, AuthorizationRoleCodes.SystemAdministrator, Configure: entity => entity.CreatedAt = DateTime.UnixEpoch);
        SeedRecord<AuthPermission>[] permissions =
            {
                new (SystemAdminPermissionId, AuthorizationPermissionCodes.SystemAdmin),
                new (UsersReadPermissionId, AuthorizationPermissionCodes.UsersRead),
                new (UsersCreatePermissionId, AuthorizationPermissionCodes.UsersCreate),
                new (UsersUpdatePermissionId, AuthorizationPermissionCodes.UsersUpdate),
                new (UsersManageStatusPermissionId, AuthorizationPermissionCodes.UsersManageStatus),
                new (UsersResetPasswordPermissionId, AuthorizationPermissionCodes.UsersResetPassword),
                new (RolesReadPermissionId, AuthorizationPermissionCodes.RolesRead),
                new (RolesManagePermissionId, AuthorizationPermissionCodes.RolesManage),
                new (UserRolesManagePermissionId, AuthorizationPermissionCodes.UserRolesManage),
                new (PermissionsReadPermissionId, AuthorizationPermissionCodes.PermissionsRead),
                new (RolePermissionsManagePermissionId, AuthorizationPermissionCodes.RolePermissionsManage),
                new (AuditReadPermissionId, AuthorizationPermissionCodes.AuditRead),
                new (AuditRollbackPermissionId, AuthorizationPermissionCodes.AuditRollback),
                new (DiagnosticsReadPermissionId, AuthorizationPermissionCodes.DiagnosticsRead),
                new (DiagnosticsManagePermissionId, AuthorizationPermissionCodes.DiagnosticsManage),
                new (EmployeesReadPermissionId, AuthorizationPermissionCodes.EmployeesRead),
                new (EmployeesManagePermissionId, AuthorizationPermissionCodes.EmployeesManage),
                new (FilesReadPermissionId, AuthorizationPermissionCodes.FilesRead),
                new (FilesCreatePermissionId, AuthorizationPermissionCodes.FilesCreate),
                new (FilesDeletePermissionId, AuthorizationPermissionCodes.FilesDelete),
                new (LocalizationReadPermissionId, AuthorizationPermissionCodes.LocalizationRead),
                new (LocalizationManagePermissionId, AuthorizationPermissionCodes.LocalizationManage),
                new (UpdatesReadPermissionId, AuthorizationPermissionCodes.UpdatesRead),
                new (UpdatesManagePermissionId, AuthorizationPermissionCodes.UpdatesManage),
                new (ReportsReadPermissionId, AuthorizationPermissionCodes.ReportsRead),
                new (ReportsManagePermissionId, AuthorizationPermissionCodes.ReportsManage),
                new (ReportsGeneratePermissionId, AuthorizationPermissionCodes.ReportsGenerate),
                new (SystemReadPermissionId, AuthorizationPermissionCodes.SystemRead),
                new (SystemManagePermissionId, AuthorizationPermissionCodes.SystemManage)
            };

        modelBuilder.Entity<AuthRole>().HasData(role.CreateDefinitionLocalized(PathRoleKey));
        modelBuilder.Entity<AuthPermission>().HasData(permissions.CreateListDefinitionLocalized(PathPermissionKey));
        modelBuilder.Entity<AuthRolePermission>().HasData(permissions.Select(permission => new AuthRolePermission
        {
            RoleId = role.Id,
            PermissionId = permission.Id,
            CreatedAt = DateTime.UnixEpoch
        }));
    }
}
