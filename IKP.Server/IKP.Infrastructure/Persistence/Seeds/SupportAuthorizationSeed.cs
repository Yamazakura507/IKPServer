using IKP.Domain.Common.Auth;
using IKP.Domain.Entities.Auth.Permissions;
using IKP.Infrastructure.Persistence.Seeds.Records;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds;

/// <summary>
/// Seeds support authorization permissions.
/// </summary>
public static class SupportAuthorizationSeed
{
    private static readonly Guid SupportReadPermissionId = AuthorizationSeed.BaseIndex.GetGuidForLevel(1, 21);
    private static readonly Guid SupportCreatePermissionId = AuthorizationSeed.BaseIndex.GetGuidForLevel(1, 22);
    private static readonly Guid SupportUpdatePermissionId = AuthorizationSeed.BaseIndex.GetGuidForLevel(1, 23);
    private static readonly Guid SupportAssignPermissionId = AuthorizationSeed.BaseIndex.GetGuidForLevel(1, 24);
    private static readonly Guid SupportStatusManagePermissionId = AuthorizationSeed.BaseIndex.GetGuidForLevel(1, 25);
    private static readonly Guid SupportDeletePermissionId = AuthorizationSeed.BaseIndex.GetGuidForLevel(1, 26);
    private static readonly Guid SupportMessagesCreatePermissionId = AuthorizationSeed.BaseIndex.GetGuidForLevel(1, 27);
    private static readonly Guid SupportInternalNotesReadPermissionId = AuthorizationSeed.BaseIndex.GetGuidForLevel(1, 28);
    private static readonly Guid SupportInternalNotesCreatePermissionId = AuthorizationSeed.BaseIndex.GetGuidForLevel(1, 29);

    /// <summary>
    /// Adds support permissions and grants them to the system administrator role.
    /// </summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        SeedRecord<AuthPermission>[] permissions =
        [
            new(SupportReadPermissionId, AuthorizationPermissionCodes.SupportRead),
            new(SupportCreatePermissionId, AuthorizationPermissionCodes.SupportCreate),
            new(SupportUpdatePermissionId, AuthorizationPermissionCodes.SupportUpdate),
            new(SupportAssignPermissionId, AuthorizationPermissionCodes.SupportAssign),
            new(SupportStatusManagePermissionId, AuthorizationPermissionCodes.SupportStatusManage),
            new(SupportDeletePermissionId, AuthorizationPermissionCodes.SupportDelete),
            new(SupportMessagesCreatePermissionId, AuthorizationPermissionCodes.SupportMessagesCreate),
            new(SupportInternalNotesReadPermissionId, AuthorizationPermissionCodes.SupportInternalNotesRead),
            new(SupportInternalNotesCreatePermissionId, AuthorizationPermissionCodes.SupportInternalNotesCreate)
        ];

        modelBuilder.Entity<AuthPermission>().HasData(permissions.CreateListDefinition(AuthorizationSeed.PathPermissionKey));
        modelBuilder.Entity<AuthRolePermission>().HasData(permissions.Select(permission => new AuthRolePermission
        {
            RoleId = AuthorizationSeed.SystemAdminRoleId,
            PermissionId = permission.Id,
            CreatedAt = DateTime.UnixEpoch
        }));
    }
}
