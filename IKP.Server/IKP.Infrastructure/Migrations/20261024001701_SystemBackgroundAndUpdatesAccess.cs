using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SystemBackgroundAndUpdatesAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "system");

            migrationBuilder.CreateTable(
                name: "BackgroundJobs",
                schema: "system",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Payload = table.Column<string>(type: "character varying(20000)", maxLength: 20000, nullable: true),
                    StatusCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ScheduledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NextAttemptAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ErrorMessage = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackgroundJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UpdateProductPermissions",
                schema: "updates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateProductPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpdateProductPermissions_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "updates_lookup",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpdateProductPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpdateProductRelations",
                schema: "updates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    RelatedProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateProductRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpdateProductRelations_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "updates_lookup",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpdateProductRelations_Products_RelatedProductId",
                        column: x => x.RelatedProductId,
                        principalSchema: "updates_lookup",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

           

            migrationBuilder.InsertData(
                schema: "auth_lookup",
                table: "Permissions",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("81000000-0000-0000-0000-000000000037"), "system.read", "Auth.Authorization.SystemRead.Description", null, true, true, "Auth.Authorization.SystemRead", 280 },
                    { new Guid("81000000-0000-0000-0000-000000000038"), "system.manage", "Auth.Authorization.SystemManage.Description", null, true, true, "Auth.Authorization.SystemManage", 290 }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("81000000-0000-0000-0000-000000000037"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, false, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000038"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, false, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BackgroundJobs_JobType_CreatedAt",
                schema: "system",
                table: "BackgroundJobs",
                columns: new[] { "JobType", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_BackgroundJobs_StatusCode_NextAttemptAt",
                schema: "system",
                table: "BackgroundJobs",
                columns: new[] { "StatusCode", "NextAttemptAt" });

            migrationBuilder.CreateIndex(
                name: "IX_UpdateProductPermissions_ProductId",
                schema: "updates",
                table: "UpdateProductPermissions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UpdateProductPermissions_UserId_ProductId",
                schema: "updates",
                table: "UpdateProductPermissions",
                columns: new[] { "UserId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UpdateProductRelations_ProductId_RelatedProductId",
                schema: "updates",
                table: "UpdateProductRelations",
                columns: new[] { "ProductId", "RelatedProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UpdateProductRelations_RelatedProductId",
                schema: "updates",
                table: "UpdateProductRelations",
                column: "RelatedProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportDefinitions_Files_FileId",
                schema: "reports",
                table: "ReportDefinitions",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportParameters_Files_FileId",
                schema: "reports",
                table: "ReportParameters",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackgroundJobs",
                schema: "system");

            migrationBuilder.DropTable(
                name: "UpdateProductPermissions",
                schema: "updates");

            migrationBuilder.DropTable(
                name: "UpdateProductRelations",
                schema: "updates");

            migrationBuilder.DropIndex(
                name: "IX_ReportParameters_FileId",
                schema: "reports",
                table: "ReportParameters");

            migrationBuilder.DropIndex(
                name: "IX_ReportDefinitions_FileId",
                schema: "reports",
                table: "ReportDefinitions");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("81000000-0000-0000-0000-000000000037"), new Guid("80000000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("81000000-0000-0000-0000-000000000038"), new Guid("80000000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000038"));
        }
    }
}
