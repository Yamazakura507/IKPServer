using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLookupFileRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AuthenticationMethods_FileId",
                schema: "auth_lookup",
                table: "AuthenticationMethods",
                column: "FileId");
            migrationBuilder.CreateIndex(
                name: "IX_Permissions_FileId",
                schema: "auth_lookup",
                table: "Permissions",
                column: "FileId");
            migrationBuilder.CreateIndex(
                name: "IX_Roles_FileId",
                schema: "auth_lookup",
                table: "Roles",
                column: "FileId");
            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreas_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas",
                column: "FileId");
            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionRules_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules",
                column: "FileId");
            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionSources_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionSources",
                column: "FileId");
            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticDefinitions_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                column: "FileId");
            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticDefinitionGroups_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups",
                column: "FileId");
            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticStatisticsWorkItemStatuses_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses",
                column: "FileId");
            migrationBuilder.CreateIndex(
                name: "IX_AuditActions_FileId",
                schema: "app_logs_lookup",
                table: "AuditActions",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthenticationMethods_Files_FileId",
                schema: "auth_lookup",
                table: "AuthenticationMethods",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Files_FileId",
                schema: "auth_lookup",
                table: "Permissions",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Files_FileId",
                schema: "auth_lookup",
                table: "Roles",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticAreas_Files_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticAreaResolutionRules_Files_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticAreaResolutionSources_Files_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionSources",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticDefinitions_Files_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticDefinitionGroups_Files_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticStatisticsWorkItemStatuses_Files_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditActions_Files_FileId",
                schema: "app_logs_lookup",
                table: "AuditActions",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditActions_Files_FileId",
                schema: "app_logs_lookup",
                table: "AuditActions");
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticStatisticsWorkItemStatuses_Files_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses");
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticDefinitionGroups_Files_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups");
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticDefinitions_Files_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions");
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticAreaResolutionSources_Files_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionSources");
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticAreaResolutionRules_Files_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules");
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosticAreas_Files_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas");
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Files_FileId",
                schema: "auth_lookup",
                table: "Roles");
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Files_FileId",
                schema: "auth_lookup",
                table: "Permissions");
            migrationBuilder.DropForeignKey(
                name: "FK_AuthenticationMethods_Files_FileId",
                schema: "auth_lookup",
                table: "AuthenticationMethods");

            migrationBuilder.DropIndex(
                name: "IX_AuditActions_FileId",
                schema: "app_logs_lookup",
                table: "AuditActions");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticStatisticsWorkItemStatuses_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticDefinitionGroups_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticDefinitions_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticAreaResolutionSources_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionSources");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticAreaResolutionRules_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticAreas_FileId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas");

            migrationBuilder.DropIndex(
                name: "IX_Roles_FileId",
                schema: "auth_lookup",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_FileId",
                schema: "auth_lookup",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_AuthenticationMethods_FileId",
                schema: "auth_lookup",
                table: "AuthenticationMethods");
        }
    }
}
