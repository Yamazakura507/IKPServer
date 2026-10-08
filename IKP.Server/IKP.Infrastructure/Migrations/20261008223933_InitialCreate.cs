using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app_logs");

            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.CreateTable(
                name: "AuditActions",
                schema: "app_logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameKey = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DescriptionKey = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FamilyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "app_logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntitySchema = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntityType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_AuditActions_ActionId",
                        column: x => x.ActionId,
                        principalSchema: "app_logs",
                        principalTable: "AuditActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogChanges",
                schema: "app_logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PropertyType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OldValue = table.Column<string>(type: "jsonb", nullable: true),
                    NewValue = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogChanges_AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalSchema: "app_logs",
                        principalTable: "AuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "app_logs",
                table: "AuditActions",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "CREATE", "Audit.Action.Create.Description", null, true, true, "Audit.Action.Create", 10 },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "UPDATE", "Audit.Action.Update.Description", null, true, true, "Audit.Action.Update", 20 },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "DELETE", "Audit.Action.Delete.Description", null, true, true, "Audit.Action.Delete", 30 },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "RESTORE", "Audit.Action.Restore.Description", null, true, true, "Audit.Action.Restore", 40 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditActions_Code",
                schema: "app_logs",
                table: "AuditActions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditActions_IsActive_SortOrder",
                schema: "app_logs",
                table: "AuditActions",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogChanges_AuditLogId",
                schema: "app_logs",
                table: "AuditLogChanges",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ActionId",
                schema: "app_logs",
                table: "AuditLogs",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_CreatedAt",
                schema: "app_logs",
                table: "AuditLogs",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntitySchema_EntityType_EntityId",
                schema: "app_logs",
                table: "AuditLogs",
                columns: new[] { "EntitySchema", "EntityType", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IsDeleted",
                schema: "app",
                table: "Employees",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogChanges",
                schema: "app_logs");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "app");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "app_logs");

            migrationBuilder.DropTable(
                name: "AuditActions",
                schema: "app_logs");
        }
    }
}
