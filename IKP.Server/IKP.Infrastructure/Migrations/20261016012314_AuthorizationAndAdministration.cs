using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AuthorizationAndAdministration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "auth_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    NameKey = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DescriptionKey = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "auth_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameKey = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DescriptionKey = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "auth",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "auth_lookup",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth_lookup",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "auth",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth_lookup",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "auth_lookup",
                table: "Permissions",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("81000000-0000-0000-0000-000000000001"), "system.admin", "Auth.Authorization.SystemAdmin.Description", null, true, true, "Auth.Authorization.SystemAdmin", 10 },
                    { new Guid("81000000-0000-0000-0000-000000000002"), "users.read", "Auth.Authorization.UsersRead.Description", null, true, true, "Auth.Authorization.UsersRead", 20 },
                    { new Guid("81000000-0000-0000-0000-000000000003"), "users.create", "Auth.Authorization.UsersCreate.Description", null, true, true, "Auth.Authorization.UsersCreate", 30 },
                    { new Guid("81000000-0000-0000-0000-000000000004"), "users.update", "Auth.Authorization.UsersUpdate.Description", null, true, true, "Auth.Authorization.UsersUpdate", 40 },
                    { new Guid("81000000-0000-0000-0000-000000000005"), "users.manage_status", "Auth.Authorization.UsersManageStatus.Description", null, true, true, "Auth.Authorization.UsersManageStatus", 50 },
                    { new Guid("81000000-0000-0000-0000-000000000006"), "users.reset_password", "Auth.Authorization.UsersResetPassword.Description", null, true, true, "Auth.Authorization.UsersResetPassword", 60 },
                    { new Guid("81000000-0000-0000-0000-000000000007"), "roles.read", "Auth.Authorization.RolesRead.Description", null, true, true, "Auth.Authorization.RolesRead", 70 },
                    { new Guid("81000000-0000-0000-0000-000000000008"), "roles.manage", "Auth.Authorization.RolesManage.Description", null, true, true, "Auth.Authorization.RolesManage", 80 },
                    { new Guid("81000000-0000-0000-0000-000000000009"), "user_roles.manage", "Auth.Authorization.UserRolesManage.Description", null, true, true, "Auth.Authorization.UserRolesManage", 90 },
                    { new Guid("81000000-0000-0000-0000-000000000010"), "permissions.read", "Auth.Authorization.PermissionsRead.Description", null, true, true, "Auth.Authorization.PermissionsRead", 100 },
                    { new Guid("81000000-0000-0000-0000-000000000011"), "role_permissions.manage", "Auth.Authorization.RolePermissionsManage.Description", null, true, true, "Auth.Authorization.RolePermissionsManage", 110 },
                    { new Guid("81000000-0000-0000-0000-000000000012"), "audit.read", "Auth.Authorization.AuditRead.Description", null, true, true, "Auth.Authorization.AuditRead", 120 },
                    { new Guid("81000000-0000-0000-0000-000000000013"), "audit.rollback", "Auth.Authorization.AuditRollback.Description", null, true, true, "Auth.Authorization.AuditRollback", 130 },
                    { new Guid("81000000-0000-0000-0000-000000000014"), "diagnostics.read", "Auth.Authorization.DiagnosticsRead.Description", null, true, true, "Auth.Authorization.DiagnosticsRead", 140 },
                    { new Guid("81000000-0000-0000-0000-000000000015"), "diagnostics.manage", "Auth.Authorization.DiagnosticsManage.Description", null, true, true, "Auth.Authorization.DiagnosticsManage", 150 },
                    { new Guid("81000000-0000-0000-0000-000000000016"), "employees.read", "Auth.Authorization.EmployeesRead.Description", null, true, true, "Auth.Authorization.EmployeesRead", 160 },
                    { new Guid("81000000-0000-0000-0000-000000000017"), "employees.manage", "Auth.Authorization.EmployeesManage.Description", null, true, true, "Auth.Authorization.EmployeesManage", 170 }
                });

            migrationBuilder.InsertData(
                schema: "auth_lookup",
                table: "Roles",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("80000000-0000-0000-0000-000000000001"), "SYSTEM_ADMINISTRATOR", new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Auth.Authorization.Role.Permission.SystemAdministrator.Description", null, true, true, "Auth.Authorization.Role.Permission.SystemAdministrator", 10, null, null });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("81000000-0000-0000-0000-000000000001"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000002"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000003"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000004"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000005"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000006"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000007"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000008"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000009"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000010"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000011"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000012"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000013"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000014"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000015"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000016"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000017"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Code",
                schema: "auth_lookup",
                table: "Permissions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_IsActive_SortOrder",
                schema: "auth_lookup",
                table: "Permissions",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "auth",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Code",
                schema: "auth_lookup",
                table: "Roles",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IsActive_SortOrder",
                schema: "auth_lookup",
                table: "Roles",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "auth",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "auth_lookup");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "auth_lookup");
        }
    }
}
