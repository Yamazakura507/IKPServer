using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKP.Infrastructure.Migrations;

public partial class AddAuthenticationFoundation : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(name: "auth");
        migrationBuilder.EnsureSchema(name: "auth_lookup");

        migrationBuilder.CreateTable(
            name: "AuthenticationMethods",
            schema: "auth_lookup",
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
            constraints: table => table.PrimaryKey("PK_AuthenticationMethods", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Users",
            schema: "auth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                NormalizedUserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: true),
                NormalizedEmail = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: true),
                DisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                IsBootstrapAccount = table.Column<bool>(type: "boolean", nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                FailedLoginCount = table.Column<int>(type: "integer", nullable: false),
                LockoutEndAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Users", x => x.Id));

        migrationBuilder.CreateTable(
            name: "UserCredentials",
            schema: "auth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                AuthenticationMethodId = table.Column<Guid>(type: "uuid", nullable: false),
                SecretHash = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                MustChangePassword = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                LastUsedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserCredentials", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserCredentials_AuthenticationMethods_AuthenticationMethodId",
                    column: x => x.AuthenticationMethodId,
                    principalSchema: "auth_lookup",
                    principalTable: "AuthenticationMethods",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_UserCredentials_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "auth",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserSessions",
            schema: "auth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                RefreshTokenHash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                LastUsedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                RevokedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                ReplacedBySessionId = table.Column<Guid>(type: "uuid", nullable: true),
                IpAddress = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                UserAgent = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserSessions", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserSessions_UserSessions_ReplacedBySessionId",
                    column: x => x.ReplacedBySessionId,
                    principalSchema: "auth",
                    principalTable: "UserSessions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_UserSessions_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "auth",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex("IX_AuthenticationMethods_Code", "AuthenticationMethods", "Code", schema: "auth_lookup", unique: true);
        migrationBuilder.CreateIndex("IX_AuthenticationMethods_IsActive_SortOrder", "AuthenticationMethods", new[] { "IsActive", "SortOrder" }, schema: "auth_lookup");
        migrationBuilder.CreateIndex("IX_Users_NormalizedUserName", "Users", "NormalizedUserName", schema: "auth", unique: true);
        migrationBuilder.CreateIndex("IX_Users_NormalizedEmail", "Users", "NormalizedEmail", schema: "auth", unique: true, filter: "\"NormalizedEmail\" IS NOT NULL");
        migrationBuilder.CreateIndex("IX_Users_IsActive_IsDeleted", "Users", new[] { "IsActive", "IsDeleted" }, schema: "auth");
        migrationBuilder.CreateIndex("IX_Users_IsBootstrapAccount", "Users", "IsBootstrapAccount", schema: "auth", unique: true, filter: "\"IsBootstrapAccount\" = TRUE");
        migrationBuilder.CreateIndex("IX_UserCredentials_UserId_AuthenticationMethodId", "UserCredentials", new[] { "UserId", "AuthenticationMethodId" }, schema: "auth", unique: true);
        migrationBuilder.CreateIndex("IX_UserCredentials_AuthenticationMethodId_IsActive", "UserCredentials", new[] { "AuthenticationMethodId", "IsActive" }, schema: "auth");
        migrationBuilder.CreateIndex("IX_UserSessions_RefreshTokenHash", "UserSessions", "RefreshTokenHash", schema: "auth", unique: true);
        migrationBuilder.CreateIndex("IX_UserSessions_UserId_RevokedAt_ExpiresAt", "UserSessions", new[] { "UserId", "RevokedAt", "ExpiresAt" }, schema: "auth");
        migrationBuilder.CreateIndex("IX_UserSessions_ReplacedBySessionId", "UserSessions", "ReplacedBySessionId", schema: "auth");

        migrationBuilder.InsertData(
            schema: "auth_lookup",
            table: "AuthenticationMethods",
            columns: new[] { "Id", "Code", "NameKey", "DescriptionKey", "FileId", "SortOrder", "IsActive", "IsSystem" },
            values: new object[] { new Guid("70000000-0000-0000-0000-000000000001"), "PASSWORD", "Auth.AuthenticationMethod.Password", "Auth.AuthenticationMethod.Password.Description", null, 10, true, true });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(schema: "auth_lookup", table: "AuthenticationMethods", keyColumn: "Id", keyValue: new Guid("70000000-0000-0000-0000-000000000001"));
        migrationBuilder.DropTable(name: "UserCredentials", schema: "auth");
        migrationBuilder.DropTable(name: "UserSessions", schema: "auth");
        migrationBuilder.DropTable(name: "Users", schema: "auth");
        migrationBuilder.DropTable(name: "AuthenticationMethods", schema: "auth_lookup");
    }
}
