using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FilesStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "files");

            migrationBuilder.CreateTable(
                name: "FileBlobs",
                schema: "files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentHash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    StorageProvider = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StorageKey = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileBlobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadSessions",
                schema: "files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StorageKey = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    OriginalFileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ExpectedSize = table.Column<long>(type: "bigint", nullable: true),
                    BytesReceived = table.Column<long>(type: "bigint", nullable: false),
                    LastActivityAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                schema: "files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BlobId = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalFileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Extension = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_FileBlobs_BlobId",
                        column: x => x.BlobId,
                        principalSchema: "files",
                        principalTable: "FileBlobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileBlobs_ContentHash_Size",
                schema: "files",
                table: "FileBlobs",
                columns: new[] { "ContentHash", "Size" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileBlobs_StorageKey",
                schema: "files",
                table: "FileBlobs",
                column: "StorageKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileBlobs_CreatedAt",
                schema: "files",
                table: "FileBlobs",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Files_BlobId",
                schema: "files",
                table: "Files",
                column: "BlobId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_IsDeleted_CreatedAt",
                schema: "files",
                table: "Files",
                columns: new[] { "IsDeleted", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_UploadSessions_CreatedBy",
                schema: "files",
                table: "UploadSessions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UploadSessions_ExpiresAt_IsCompleted",
                schema: "files",
                table: "UploadSessions",
                columns: new[] { "ExpiresAt", "IsCompleted" });

            migrationBuilder.InsertData(
                schema: "auth_lookup",
                table: "Permissions",
                columns: new[] { "Id", "Code", "NameKey", "DescriptionKey", "SortOrder", "IsActive", "IsSystem" },
                values: new object[,]
                {
                { new Guid("81000000-0000-0000-0000-000000000018"), "files.read", "Auth.Authorization.FilesRead", "Auth.Authorization.FilesRead.Description", 180, true, true },
                { new Guid("81000000-0000-0000-0000-000000000019"), "files.create", "Auth.Authorization.FilesCreate", "Auth.Authorization.FilesCreate.Description", 190, true, true },
                { new Guid("81000000-0000-0000-0000-000000000020"), "files.delete", "Auth.Authorization.FilesDelete", "Auth.Authorization.FilesDelete.Description", 200, true, true }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId", "CreatedAt", "IsDeleted" },
                values: new object[,]
                {
                { new Guid("80000000-0000-0000-0000-000000000001"), new Guid("81000000-0000-0000-0000-000000000018"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                { new Guid("80000000-0000-0000-0000-000000000001"), new Guid("81000000-0000-0000-0000-000000000019"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false },
                { new Guid("80000000-0000-0000-0000-000000000001"), new Guid("81000000-0000-0000-0000-000000000020"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "RoleId", "PermissionId" },
                keyValues: new object[] { new Guid("80000000-0000-0000-0000-000000000001"), new Guid("81000000-0000-0000-0000-000000000018") });
            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "RoleId", "PermissionId" },
                keyValues: new object[] { new Guid("80000000-0000-0000-0000-000000000001"), new Guid("81000000-0000-0000-0000-000000000019") });
            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "RoleId", "PermissionId" },
                keyValues: new object[] { new Guid("80000000-0000-0000-0000-000000000001"), new Guid("81000000-0000-0000-0000-000000000020") });

            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000018"));
            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000019"));
            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000020"));

            migrationBuilder.DropTable(
                name: "Files",
                schema: "files");
            migrationBuilder.DropTable(
                name: "FileBlobs",
                schema: "files");
            migrationBuilder.DropTable(
                name: "UploadSessions",
                schema: "files");
        }
    }
}
