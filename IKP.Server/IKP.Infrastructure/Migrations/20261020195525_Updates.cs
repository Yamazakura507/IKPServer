using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "updates");

            migrationBuilder.EnsureSchema(
                name: "updates_lookup");

            migrationBuilder.CreateTable(
                name: "Platforms",
                schema: "updates_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Platforms_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UpdateCategories",
                schema: "updates_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_UpdateCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpdateCategories_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "updates_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlatformId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Products_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalSchema: "updates_lookup",
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Products_UpdateCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "updates_lookup",
                        principalTable: "UpdateCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Versions",
                schema: "updates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ContentHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: true),
                    IsPrerelease = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    ReleaseAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Versions_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Versions_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "updates_lookup",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DownloadLinks",
                schema: "updates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VersionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DownloadLinks_Versions_VersionId",
                        column: x => x.VersionId,
                        principalSchema: "updates",
                        principalTable: "Versions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseNotes",
                schema: "updates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VersionId = table.Column<Guid>(type: "uuid", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "character varying(12000)", maxLength: 12000, nullable: true),
                    IsPlanned = table.Column<bool>(type: "boolean", nullable: false),
                    IsHtmlDescription = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseNotes_Versions_VersionId",
                        column: x => x.VersionId,
                        principalSchema: "updates",
                        principalTable: "Versions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseNoteAttachments",
                schema: "updates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReleaseNoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseNoteAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseNoteAttachments_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReleaseNoteAttachments_ReleaseNotes_ReleaseNoteId",
                        column: x => x.ReleaseNoteId,
                        principalSchema: "updates",
                        principalTable: "ReleaseNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "auth_lookup",
                table: "Permissions",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("81000000-0000-0000-0000-000000000032"), "updates.read", "Auth.Authorization.UpdatesRead.Description", null, true, true, "Auth.Authorization.UpdatesRead", 230 },
                    { new Guid("81000000-0000-0000-0000-000000000033"), "updates.manage", "Auth.Authorization.UpdatesManage.Description", null, true, true, "Auth.Authorization.UpdatesManage", 240 }
                });

            migrationBuilder.InsertData(
                schema: "updates_lookup",
                table: "Platforms",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("11000000-0000-0000-0000-000000000001"), "WINDOWS", "Updates.Platforms.Windows.Description", null, true, true, "Updates.Platforms.Windows", 10 },
                    { new Guid("11000000-0000-0000-0000-000000000002"), "ANDROID", "Updates.Platforms.Android.Description", null, true, true, "Updates.Platforms.Android", 20 }
                });

            migrationBuilder.InsertData(
                schema: "updates_lookup",
                table: "UpdateCategories",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("11000000-0000-0000-0000-000000000001"), "CLIENT", "Updates.Categories.Client.Description", null, true, true, "Updates.Categories.Client", 10 },
                    { new Guid("11000000-0000-0000-0000-000000000002"), "LIB", "Updates.Categories.Lib.Description", null, true, true, "Updates.Categories.Lib", 20 },
                    { new Guid("11000000-0000-0000-0000-000000000003"), "INSTALLER", "Updates.Categories.Installer.Description", null, true, true, "Updates.Categories.Installer", 30 }
                });

            migrationBuilder.InsertData(
                schema: "updates_lookup",
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "PlatformId", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("11000000-0000-0000-0000-000000000001"), new Guid("11000000-0000-0000-0000-000000000001"), "DEVELOPER", "Updates.Products.Client.Developer.Description", null, true, true, "Updates.Products.Client.Developer", new Guid("11000000-0000-0000-0000-000000000001"), 10 },
                    { new Guid("11000000-0000-0000-0000-000000000002"), new Guid("11000000-0000-0000-0000-000000000001"), "HR_BASE", "Updates.Products.Client.HrBase.Description", null, true, true, "Updates.Products.Client.HrBase", new Guid("11000000-0000-0000-0000-000000000001"), 10 },
                    { new Guid("11000000-0000-0000-0000-000000000003"), new Guid("11000000-0000-0000-0000-000000000001"), "HR_BOSS", "Updates.Products.Client.HrBoss.Description", null, true, true, "Updates.Products.Client.HrBoss", new Guid("11000000-0000-0000-0000-000000000001"), 10 },
                    { new Guid("12000000-0000-0000-0000-000000000001"), new Guid("11000000-0000-0000-0000-000000000002"), "THEME_MANAGER", "Updates.Products.Lib.ThemeManager.Description", null, true, true, "Updates.Products.Lib.ThemeManager", new Guid("11000000-0000-0000-0000-000000000001"), 10 },
                    { new Guid("12000000-0000-0000-0000-000000000002"), new Guid("11000000-0000-0000-0000-000000000002"), "FILE_MANAGER", "Updates.Products.Lib.FileManager.Description", null, true, true, "Updates.Products.Lib.FileManager", new Guid("11000000-0000-0000-0000-000000000001"), 10 },
                    { new Guid("12000000-0000-0000-0000-000000000003"), new Guid("11000000-0000-0000-0000-000000000002"), "REPORT_MANAGER", "Updates.Products.Lib.ReportManager.Description", null, true, true, "Updates.Products.Lib.ReportManager", new Guid("11000000-0000-0000-0000-000000000001"), 10 },
                    { new Guid("12000000-0000-0000-0000-000000000004"), new Guid("11000000-0000-0000-0000-000000000002"), "MESSAGE_MANAGER", "Updates.Products.Lib.MessageManager.Description", null, true, true, "Updates.Products.Lib.MessageManager", new Guid("11000000-0000-0000-0000-000000000001"), 10 },
                    { new Guid("12000000-0000-0000-0000-000000000005"), new Guid("11000000-0000-0000-0000-000000000002"), "NOTIECE_MANAGER", "Updates.Products.Lib.NotieceManager.Description", null, true, true, "Updates.Products.Lib.NotieceManager", new Guid("11000000-0000-0000-0000-000000000001"), 10 },
                    { new Guid("13000000-0000-0000-0000-000000000001"), new Guid("11000000-0000-0000-0000-000000000003"), "IKP_SOFT", "Updates.Products.Installer.IkpSoft.Description", null, true, true, "Updates.Products.Installer.IkpSoft", new Guid("11000000-0000-0000-0000-000000000001"), 10 }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("81000000-0000-0000-0000-000000000032"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, false, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000033"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, false, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DownloadLinks_VersionId_IsPrimary",
                schema: "updates",
                table: "DownloadLinks",
                columns: new[] { "VersionId", "IsPrimary" },
                unique: true,
                filter: "\"IsPrimary\" = true");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_Code",
                schema: "updates_lookup",
                table: "Platforms",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_FileId",
                schema: "updates_lookup",
                table: "Platforms",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_IsActive_SortOrder",
                schema: "updates_lookup",
                table: "Platforms",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId_Code",
                schema: "updates_lookup",
                table: "Products",
                columns: new[] { "CategoryId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                schema: "updates_lookup",
                table: "Products",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_FileId",
                schema: "updates_lookup",
                table: "Products",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IsActive_SortOrder",
                schema: "updates_lookup",
                table: "Products",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_PlatformId",
                schema: "updates_lookup",
                table: "Products",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseNoteAttachments_FileId",
                schema: "updates",
                table: "ReleaseNoteAttachments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseNoteAttachments_ReleaseNoteId_SortOrder",
                schema: "updates",
                table: "ReleaseNoteAttachments",
                columns: new[] { "ReleaseNoteId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseNotes_VersionId_SortOrder",
                schema: "updates",
                table: "ReleaseNotes",
                columns: new[] { "VersionId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_UpdateCategories_Code",
                schema: "updates_lookup",
                table: "UpdateCategories",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UpdateCategories_FileId",
                schema: "updates_lookup",
                table: "UpdateCategories",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_UpdateCategories_IsActive_SortOrder",
                schema: "updates_lookup",
                table: "UpdateCategories",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Versions_FileId",
                schema: "updates",
                table: "Versions",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_ProductId_IsPublished_ReleaseAt",
                schema: "updates",
                table: "Versions",
                columns: new[] { "ProductId", "IsPublished", "ReleaseAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Versions_ProductId_Version",
                schema: "updates",
                table: "Versions",
                columns: new[] { "ProductId", "Version" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DownloadLinks",
                schema: "updates");

            migrationBuilder.DropTable(
                name: "ReleaseNoteAttachments",
                schema: "updates");

            migrationBuilder.DropTable(
                name: "ReleaseNotes",
                schema: "updates");

            migrationBuilder.DropTable(
                name: "Versions",
                schema: "updates");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "updates_lookup");

            migrationBuilder.DropTable(
                name: "Platforms",
                schema: "updates_lookup");

            migrationBuilder.DropTable(
                name: "UpdateCategories",
                schema: "updates_lookup");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("81000000-0000-0000-0000-000000000032"), new Guid("80000000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("81000000-0000-0000-0000-000000000033"), new Guid("80000000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000033"));
        }
    }
}
