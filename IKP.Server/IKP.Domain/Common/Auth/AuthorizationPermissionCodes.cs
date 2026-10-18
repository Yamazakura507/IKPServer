namespace IKP.Domain.Common.Auth;

/// <summary>
/// Contains stable authorization permission codes used by the server.
/// </summary>
public static class AuthorizationPermissionCodes
{
    /// <summary>
    /// Grants unrestricted server administration.
    /// </summary>
    public const string SystemAdmin = "system.admin";

    /// <summary>
    /// Allows viewing users.
    /// </summary>
    public const string UsersRead = "users.read";

    /// <summary>
    /// Allows creating users.
    /// </summary>
    public const string UsersCreate = "users.create";

    /// <summary>
    /// Allows updating users.
    /// </summary>
    public const string UsersUpdate = "users.update";

    /// <summary>
    /// Allows deactivating and reactivating users.
    /// </summary>
    public const string UsersManageStatus = "users.manage_status";

    /// <summary
    /// >Allows resetting a user's password.
    /// </summary>
    public const string UsersResetPassword = "users.reset_password";

    /// <summary>
    /// Allows viewing roles.
    /// </summary>
    public const string RolesRead = "roles.read";

    /// <summary>
    /// Allows creating and updating roles.
    /// </summary>
    public const string RolesManage = "roles.manage";

    /// <summary>
    /// Allows assigning roles to users.
    /// </summary>
    public const string UserRolesManage = "user_roles.manage";

    /// <summary>
    /// Allows viewing permissions.
    /// </summary>
    public const string PermissionsRead = "permissions.read";

    /// <summary>
    /// Allows managing role permissions.
    /// </summary>
    public const string RolePermissionsManage = "role_permissions.manage";

    /// <summary>
    /// Allows reading audit records.
    /// </summary>
    public const string AuditRead = "audit.read";

    /// <summary>
    /// Allows rolling back audit changes.
    /// </summary>
    public const string AuditRollback = "audit.rollback";

    /// <summary>
    /// Allows reading diagnostics.
    /// </summary>
    public const string DiagnosticsRead = "diagnostics.read";

    /// <summary>
    /// Allows managing diagnostic configuration.
    /// </summary>
    public const string DiagnosticsManage = "diagnostics.manage";

    /// <summary>
    /// Allows viewing employees.
    /// </summary>
    public const string EmployeesRead = "employees.read";

    /// <summary>
    /// Allows creating, updating, deleting and restoring employees.
    /// </summary>
    public const string EmployeesManage = "employees.manage";

    /// <summary>
    /// Allows viewing file metadata and downloading file content.
    /// </summary>
    public const string FilesRead = "files.read";

    /// <summary>
    /// Allows creating and uploading files.
    /// </summary>
    public const string FilesCreate = "files.create";

    /// <summary>
    /// Allows deleting stored file records.
    /// </summary>
    public const string FilesDelete = "files.delete";
}
