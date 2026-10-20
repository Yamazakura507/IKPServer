using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Localization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "localization");

            migrationBuilder.EnsureSchema(
                name: "localization_lookup");

            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "localization_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CultureCode = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    NativeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsRightToLeft = table.Column<bool>(type: "boolean", nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NameKey = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DescriptionKey = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Languages_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LanguagePackages",
                schema: "localization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentHash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguagePackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguagePackages_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LanguagePackages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "localization_lookup",
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "localization_lookup",
                table: "Languages",
                columns: new[] { "Id", "Code", "CultureCode", "DescriptionKey", "FileId", "IsActive", "IsDefault", "IsRightToLeft", "IsSystem", "NameKey", "NativeName", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "en", "en-US", "Localization.Language.En.Description", null, true, true, false, true, "Localization.Language.En", "English", 10 },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "ru", "ru-RU", "Localization.Language.Ru.Description", null, true, false, false, true, "Localization.Language.Ru", "Русский", 20 }
                });

            migrationBuilder.InsertData(
                schema: "auth_lookup",
                table: "Permissions",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("81000000-0000-0000-0000-000000000030"), "localization.read", "Auth.Authorization.LocalizationRead.Description", null, true, true, "Auth.Authorization.LocalizationRead", 210 },
                    { new Guid("81000000-0000-0000-0000-000000000031"), "localization.manage", "Auth.Authorization.LocalizationManage.Description", null, true, true, "Auth.Authorization.LocalizationManage", 220 }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("81000000-0000-0000-0000-000000000030"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, false, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000031"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, false, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanguagePackages_FileId",
                schema: "localization",
                table: "LanguagePackages",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LanguagePackages_LanguageId_IsActive",
                schema: "localization",
                table: "LanguagePackages",
                columns: new[] { "LanguageId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_LanguagePackages_LanguageId_Version",
                schema: "localization",
                table: "LanguagePackages",
                columns: new[] { "LanguageId", "Version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Code",
                schema: "localization_lookup",
                table: "Languages",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_CultureCode",
                schema: "localization_lookup",
                table: "Languages",
                column: "CultureCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_FileId",
                schema: "localization_lookup",
                table: "Languages",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_IsActive_SortOrder",
                schema: "localization_lookup",
                table: "Languages",
                columns: new[] { "IsActive", "SortOrder" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanguagePackages",
                schema: "localization");

            migrationBuilder.DropTable(
                name: "Languages",
                schema: "localization_lookup");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("81000000-0000-0000-0000-000000000030"), new Guid("80000000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("81000000-0000-0000-0000-000000000031"), new Guid("80000000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000031"));
        }
    }
}
