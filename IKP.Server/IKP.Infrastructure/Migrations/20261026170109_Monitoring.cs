using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Monitoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "monitoring");

            migrationBuilder.CreateTable(
                name: "AlertRules",
                schema: "monitoring",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameKey = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DescriptionKey = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Condition = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    SeverityCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BackupRecords",
                schema: "monitoring",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BackupType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StatusCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StorageKey = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: true),
                    ContentHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ErrorMessage = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackupRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthSnapshots",
                schema: "monitoring",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Component = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    StatusCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DurationMilliseconds = table.Column<double>(type: "double precision", nullable: true),
                    Details = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: true),
                    CheckedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthSnapshots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RetentionPolicys",
                schema: "monitoring",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AreaCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RetentionDays = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LastRunAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetentionPolicys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                schema: "monitoring",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AlertRuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    TriggeredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alerts_AlertRules_AlertRuleId",
                        column: x => x.AlertRuleId,
                        principalSchema: "monitoring",
                        principalTable: "AlertRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "auth_lookup",
                table: "Permissions",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("81000000-0000-0000-0000-000000000039"), "monitoring.read", "Auth.Authorization.MonitoringRead.Description", null, true, true, "Auth.Authorization.MonitoringRead", 300 },
                    { new Guid("81000000-0000-0000-0000-000000000040"), "monitoring.manage", "Auth.Authorization.MonitoringManage.Description", null, true, true, "Auth.Authorization.MonitoringManage", 310 }
                });

            migrationBuilder.InsertData(
                schema: "monitoring",
                table: "RetentionPolicys",
                columns: new[] { "Id", "AreaCode", "CreatedAt", "CreatedBy", "IsActive", "LastRunAt", "RetentionDays", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("91000000-0000-0000-0000-000000000001"), "MONITORING_HEALTH", new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, null, 30, null, null });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("81000000-0000-0000-0000-000000000039"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, false, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000040"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, false, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertRules_Code",
                schema: "monitoring",
                table: "AlertRules",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_AlertRuleId",
                schema: "monitoring",
                table: "Alerts",
                column: "AlertRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_StatusCode_TriggeredAt",
                schema: "monitoring",
                table: "Alerts",
                columns: new[] { "StatusCode", "TriggeredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_BackupRecords_StartedAt",
                schema: "monitoring",
                table: "BackupRecords",
                column: "StartedAt");

            migrationBuilder.CreateIndex(
                name: "IX_HealthSnapshots_Component_CheckedAt",
                schema: "monitoring",
                table: "HealthSnapshots",
                columns: new[] { "Component", "CheckedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_RetentionPolicys_AreaCode",
                schema: "monitoring",
                table: "RetentionPolicys",
                column: "AreaCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts",
                schema: "monitoring");

            migrationBuilder.DropTable(
                name: "BackupRecords",
                schema: "monitoring");

            migrationBuilder.DropTable(
                name: "HealthSnapshots",
                schema: "monitoring");

            migrationBuilder.DropTable(
                name: "RetentionPolicys",
                schema: "monitoring");

            migrationBuilder.DropTable(
                name: "AlertRules",
                schema: "monitoring");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("81000000-0000-0000-0000-000000000039"), new Guid("80000000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("81000000-0000-0000-0000-000000000040"), new Guid("80000000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000040"));
        }
    }
}
