using IKP.Domain.Common.Auth;

namespace IKP.Application.Auth.Authorization;

/// <summary>
/// Contains authorization policy names exposed by the API.
/// </summary>
public static class AuthorizationPolicyNames
{
    /// <summary>
    /// Gets the policy that requires server administration permission.
    /// </summary>
    public const string SystemAdmin = AuthorizationPermissionCodes.SystemAdmin;

    /// <summary>
    /// Gets the policy that requires user read permission.
    /// </summary>
    public const string UsersRead = AuthorizationPermissionCodes.UsersRead;

    /// <summary>
    /// Gets the policy that requires user creation permission.
    /// </summary>
    public const string UsersCreate = AuthorizationPermissionCodes.UsersCreate;

    /// <summary>
    /// Gets the policy that requires user update permission.
    /// </summary>
    public const string UsersUpdate = AuthorizationPermissionCodes.UsersUpdate;

    /// <summary>
    /// Gets the policy that requires user status management permission.
    /// </summary>
    public const string UsersManageStatus = AuthorizationPermissionCodes.UsersManageStatus;

    /// <summary>
    /// Gets the policy that requires user password reset permission.
    /// </summary>
    public const string UsersResetPassword = AuthorizationPermissionCodes.UsersResetPassword;

    /// <summary>
    /// Gets the policy that requires role read permission.
    /// </summary>
    public const string RolesRead = AuthorizationPermissionCodes.RolesRead;

    /// <summary>
    /// Gets the policy that requires role management permission.
    /// </summary>
    public const string RolesManage = AuthorizationPermissionCodes.RolesManage;

    /// <summary>
    /// Gets the policy that requires user-role management permission.
    /// </summary>
    public const string UserRolesManage = AuthorizationPermissionCodes.UserRolesManage;

    /// <summary>
    /// Gets the policy that requires permission read access.
    /// </summary>
    public const string PermissionsRead = AuthorizationPermissionCodes.PermissionsRead;

    /// <summary>
    /// Gets the policy that requires role-permission management access.
    /// </summary>
    public const string RolePermissionsManage = AuthorizationPermissionCodes.RolePermissionsManage;

    /// <summary>
    /// Gets the policy that requires audit read access.
    /// </summary>
    public const string AuditRead = AuthorizationPermissionCodes.AuditRead;

    /// <summary>
    /// Gets the policy that requires audit rollback access.
    /// </summary>
    public const string AuditRollback = AuthorizationPermissionCodes.AuditRollback;

    /// <summary>
    /// Gets the policy that requires diagnostics read access.
    /// </summary>
    public const string DiagnosticsRead = AuthorizationPermissionCodes.DiagnosticsRead;

    /// <summary>
    /// Gets the policy that requires diagnostics management access.
    /// </summary>
    public const string DiagnosticsManage = AuthorizationPermissionCodes.DiagnosticsManage;

    /// <summary>
    /// Gets the policy that requires employee read access.
    /// </summary>
    public const string EmployeesRead = AuthorizationPermissionCodes.EmployeesRead;

    /// <summary>
    /// Gets the policy that requires employee management access.
    /// </summary>
    public const string EmployeesManage = AuthorizationPermissionCodes.EmployeesManage;

    /// <summary>
    /// Gets the policy that requires file read access.
    /// </summary>
    public const string FilesRead = AuthorizationPermissionCodes.FilesRead;

    /// <summary>
    /// Gets the policy that requires file creation access.
    /// </summary>
    public const string FilesCreate = AuthorizationPermissionCodes.FilesCreate;

    /// <summary>
    /// Gets the policy that requires file deletion access.
    /// </summary>
    public const string FilesDelete = AuthorizationPermissionCodes.FilesDelete;

    /// <summary>
    /// Gets the policy that allows reading localization resources.
    /// </summary>
    public const string LocalizationRead = AuthorizationPermissionCodes.LocalizationRead;

    /// <summary>
    /// Gets the policy that allows managing localization resources.
    /// </summary>
    public const string LocalizationManage = AuthorizationPermissionCodes.LocalizationManage;

    public const string SystemRead = AuthorizationPermissionCodes.SystemRead;
    public const string SystemManage = AuthorizationPermissionCodes.SystemManage;

    public const string ReportsRead = AuthorizationPermissionCodes.ReportsRead;
    public const string ReportsManage = AuthorizationPermissionCodes.ReportsManage;
    public const string ReportsGenerate = AuthorizationPermissionCodes.ReportsGenerate;

    public const string UpdatesRead = AuthorizationPermissionCodes.UpdatesRead;
    public const string UpdatesManage = AuthorizationPermissionCodes.UpdatesManage;

    /// <summary>
    /// Gets the policy that allows reading support tickets.
    /// </summary>
    public const string SupportRead = AuthorizationPermissionCodes.SupportRead;

    /// <summary>
    /// Gets the policy that allows creating support tickets.
    /// </summary>
    public const string SupportCreate = AuthorizationPermissionCodes.SupportCreate;

    /// <summary>
    /// Gets the policy that allows updating support tickets.
    /// </summary>
    public const string SupportUpdate = AuthorizationPermissionCodes.SupportUpdate;

    /// <summary>
    /// Gets the policy that allows assigning support tickets.
    /// </summary>
    public const string SupportAssign = AuthorizationPermissionCodes.SupportAssign;

    /// <summary>
    /// Gets the policy that allows changing support ticket status.
    /// </summary>
    public const string SupportStatusManage = AuthorizationPermissionCodes.SupportStatusManage;

    /// <summary>
    /// Gets the policy that allows deleting support tickets.
    /// </summary>
    public const string SupportDelete = AuthorizationPermissionCodes.SupportDelete;

    /// <summary>
    /// Gets the policy that allows adding public messages.
    /// </summary>
    public const string SupportMessagesCreate = AuthorizationPermissionCodes.SupportMessagesCreate;

    /// <summary>
    /// Gets the policy that allows reading internal notes.
    /// </summary>
    public const string SupportInternalNotesRead = AuthorizationPermissionCodes.SupportInternalNotesRead;

    /// <summary>
    /// Gets the policy that allows creating internal notes.
    /// </summary>
    public const string SupportInternalNotesCreate = AuthorizationPermissionCodes.SupportInternalNotesCreate;
}
