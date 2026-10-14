using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditRestoreSemanticsAndFuzzySearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RestorLogId",
                schema: "app_logs",
                table: "AuditLogs",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_RestorLogId",
                schema: "app_logs",
                table: "AuditLogs",
                column: "RestorLogId");

            migrationBuilder.UpdateData(
                schema: "app_logs_lookup",
                table: "AuditActions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                columns: new[] { "Code", "DescriptionKey", "NameKey" },
                values: new object[] { "RESTORE_DELETE", "Audit.Action.RestoreDelete.Description", "Audit.Action.RestoreDelete" });

            migrationBuilder.InsertData(
                schema: "app_logs_lookup",
                table: "AuditActions",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[]
                {
            new Guid("10000000-0000-0000-0000-000000000005"),
            "RESTORE_UPDATE",
            "Audit.Action.RestoreUpdate.Description",
            null,
            true,
            true,
            "Audit.Action.RestoreUpdate",
            50
                });

            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS fuzzystrmatch;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "app_logs_lookup",
                table: "AuditActions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.UpdateData(
                schema: "app_logs_lookup",
                table: "AuditActions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                columns: new[] { "Code", "DescriptionKey", "NameKey" },
                values: new object[] { "RESTORE", "Audit.Action.Restore.Description", "Audit.Action.Restore" });

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_RestorLogId",
                schema: "app_logs",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "RestorLogId",
                schema: "app_logs",
                table: "AuditLogs");
        }
    }
}
