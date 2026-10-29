using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HrFoundation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Files_FileId1",
                schema: "localization_lookup",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Files_FileId1",
                schema: "auth_lookup",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Platforms_Files_FileId1",
                schema: "updates_lookup",
                table: "Platforms");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Files_FileId1",
                schema: "updates_lookup",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseNoteAttachments_Files_FileId",
                schema: "updates",
                table: "ReleaseNoteAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportCategories_Files_FileId1",
                schema: "reports_lookup",
                table: "ReportCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportDataTypies_Files_FileId1",
                schema: "reports_lookup",
                table: "ReportDataTypies");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportExecutionStatuses_Files_FileId1",
                schema: "reports_lookup",
                table: "ReportExecutionStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportTemplateVersions_Files_FileId",
                schema: "reports",
                table: "ReportTemplateVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportTemplateVersions_Files_FileId1",
                schema: "reports",
                table: "ReportTemplateVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Files_FileId1",
                schema: "auth_lookup",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketCategories_Files_FileId1",
                schema: "support_lookup",
                table: "TicketCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketPriorities_Files_FileId1",
                schema: "support_lookup",
                table: "TicketPriorities");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketStatuses_Files_FileId1",
                schema: "support_lookup",
                table: "TicketStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_UpdateCategories_Files_FileId1",
                schema: "updates_lookup",
                table: "UpdateCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Versions_Files_FileId1",
                schema: "updates",
                table: "Versions");

            migrationBuilder.DropIndex(
                name: "IX_Versions_FileId1",
                schema: "updates",
                table: "Versions");

            migrationBuilder.DropIndex(
                name: "IX_UpdateCategories_FileId1",
                schema: "updates_lookup",
                table: "UpdateCategories");

            migrationBuilder.DropIndex(
                name: "IX_TicketStatuses_FileId1",
                schema: "support_lookup",
                table: "TicketStatuses");

            migrationBuilder.DropIndex(
                name: "IX_TicketPriorities_FileId1",
                schema: "support_lookup",
                table: "TicketPriorities");

            migrationBuilder.DropIndex(
                name: "IX_TicketCategories_FileId1",
                schema: "support_lookup",
                table: "TicketCategories");

            migrationBuilder.DropIndex(
                name: "IX_Roles_FileId1",
                schema: "auth_lookup",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_ReportTemplateVersions_FileId1",
                schema: "reports",
                table: "ReportTemplateVersions");

            migrationBuilder.DropIndex(
                name: "IX_ReportExecutionStatuses_FileId1",
                schema: "reports_lookup",
                table: "ReportExecutionStatuses");

            migrationBuilder.DropIndex(
                name: "IX_ReportDataTypies_FileId1",
                schema: "reports_lookup",
                table: "ReportDataTypies");

            migrationBuilder.DropIndex(
                name: "IX_ReportCategories_FileId1",
                schema: "reports_lookup",
                table: "ReportCategories");

            migrationBuilder.DropIndex(
                name: "IX_Products_FileId1",
                schema: "updates_lookup",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Platforms_FileId1",
                schema: "updates_lookup",
                table: "Platforms");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_FileId1",
                schema: "auth_lookup",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Languages_FileId1",
                schema: "localization_lookup",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticStatisticsWorkItemStatuses_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticDefinitions_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticDefinitionGroups_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticAreas_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticAreaResolutionSources_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionSources");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosticAreaResolutionRules_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules");

            migrationBuilder.DropIndex(
                name: "IX_AuthenticationMethods_FileId1",
                schema: "auth_lookup",
                table: "AuthenticationMethods");

            migrationBuilder.DropIndex(
                name: "IX_AuditActions_FileId1",
                schema: "app_logs_lookup",
                table: "AuditActions");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "updates",
                table: "Versions");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "updates_lookup",
                table: "UpdateCategories");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "support_lookup",
                table: "TicketStatuses");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "support_lookup",
                table: "TicketPriorities");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "support_lookup",
                table: "TicketCategories");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "auth_lookup",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "reports",
                table: "ReportTemplateVersions");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "reports_lookup",
                table: "ReportExecutionStatuses");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "reports_lookup",
                table: "ReportDataTypies");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "reports_lookup",
                table: "ReportCategories");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "updates_lookup",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "updates_lookup",
                table: "Platforms");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "auth_lookup",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "localization_lookup",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionSources");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "auth_lookup",
                table: "AuthenticationMethods");

            migrationBuilder.DropColumn(
                name: "FileId1",
                schema: "app_logs_lookup",
                table: "AuditActions");

            migrationBuilder.EnsureSchema(
                name: "app_lookup");

            migrationBuilder.AlterColumn<string>(
                name: "FamilyName",
                schema: "app",
                table: "Employees",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                schema: "app",
                table: "Employees",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                schema: "app",
                table: "Employees",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "HireDate",
                schema: "app",
                table: "Employees",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                schema: "app",
                table: "Employees",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                schema: "app",
                table: "Employees",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "app",
                table: "Employees",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AttestationQuestionType",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_AttestationQuestionType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttestationQuestionType_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AttestationSectionType",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    ForPromotion = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_AttestationSectionType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttestationSectionType_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyAssessmentItemValues",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false),
                    Comment = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
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
                    table.PrimaryKey("PK_CompetencyAssessmentItemValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyAssessmentType",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_CompetencyAssessmentType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencyAssessmentType_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyMatrices",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_CompetencyMatrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetencySectionType",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencySectionType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencySectionType_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentDepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    HeadEmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Departments_ParentDepartmentId",
                        column: x => x.ParentDepartmentId,
                        principalSchema: "app",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Departments_Employees_HeadEmployeeId",
                        column: x => x.HeadEmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Departments_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DevelopmentControlMethods",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
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
                    table.PrimaryKey("PK_DevelopmentControlMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DevelopmentItemType",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_DevelopmentItemType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevelopmentItemType_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DevelopmentMethods",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    IsControl = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_DevelopmentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DevelopmentPlanType",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_DevelopmentPlanType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevelopmentPlanType_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "EducationType",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_EducationType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationType_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContactType",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContactType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeContactType_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeQualifications",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Issuer = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    IssuedAt = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpiresAt = table.Column<DateOnly>(type: "date", nullable: true),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_EmployeeQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeQualifications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeQualifications_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentRecordType",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentRecordType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentRecordType_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProbationPlans",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManagerEmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    FinalCompletionPercent = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
                    FinalComment = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_ProbationPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProbationPlans_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProbationPlans_Employees_ManagerEmployeeId",
                        column: x => x.ManagerEmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyMatrixVersions",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MatrixId = table.Column<Guid>(type: "uuid", nullable: false),
                    VersionNumber = table.Column<int>(type: "integer", nullable: false),
                    ChangeNotes = table.Column<string>(type: "text", nullable: true),
                    EffectiveFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_CompetencyMatrixVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencyMatrixVersions_CompetencyMatrices_MatrixId",
                        column: x => x.MatrixId,
                        principalSchema: "app",
                        principalTable: "CompetencyMatrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevelopmentNeeds",
                schema: "app_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_DevelopmentNeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevelopmentNeeds_DevelopmentItemType_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "app_lookup",
                        principalTable: "DevelopmentItemType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DevelopmentPlans",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManagerEmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanningDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodStart = table.Column<DateOnly>(type: "date", nullable: true),
                    PeriodEnd = table.Column<DateOnly>(type: "date", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    ManagerEffectiveness = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
                    EmployeeEffectiveness = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
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
                    table.PrimaryKey("PK_DevelopmentPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevelopmentPlans_DevelopmentPlanType_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "app_lookup",
                        principalTable: "DevelopmentPlanType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DevelopmentPlans_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DevelopmentPlans_Employees_ManagerEmployeeId",
                        column: x => x.ManagerEmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEducation",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EducationTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Organization = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Faculty = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Speciality = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Qualification = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    GraduationYear = table.Column<int>(type: "integer", nullable: true),
                    BeginOfDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndOfDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProofOfDocumentNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_EmployeeEducation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeEducation_EducationType_EducationTypeId",
                        column: x => x.EducationTypeId,
                        principalSchema: "app_lookup",
                        principalTable: "EducationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeEducation_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeEducation_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContacts",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeContactTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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
                    table.PrimaryKey("PK_EmployeeContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeContacts_EmployeeContactType_EmployeeContactTypeId",
                        column: x => x.EmployeeContactTypeId,
                        principalSchema: "app_lookup",
                        principalTable: "EmployeeContactType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeContacts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentHistory",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    GradeId = table.Column<Guid>(type: "uuid", nullable: true),
                    ManagerEmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    RecordTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValidFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    ValidTo = table.Column<DateOnly>(type: "date", nullable: true),
                    Reason = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Notes = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentHistory_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "app",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmploymentHistory_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmploymentHistory_Employees_ManagerEmployeeId",
                        column: x => x.ManagerEmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EmploymentHistory_EmploymentRecordType_RecordTypeId",
                        column: x => x.RecordTypeId,
                        principalSchema: "app_lookup",
                        principalTable: "EmploymentRecordType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmploymentHistory_Grades_GradeId",
                        column: x => x.GradeId,
                        principalSchema: "app_lookup",
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EmploymentHistory_Positions_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "app_lookup",
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProbationTasks",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    Deadline = table.Column<DateOnly>(type: "date", nullable: true),
                    CompletionPercent = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false),
                    WeeklyReport = table.Column<string>(type: "text", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_ProbationTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProbationTasks_ProbationPlans_PlanId",
                        column: x => x.PlanId,
                        principalSchema: "app",
                        principalTable: "ProbationPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyAssessments",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    MatrixVersionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssessmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AssessorEmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssessmentTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_CompetencyAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencyAssessments_CompetencyAssessmentType_AssessmentTy~",
                        column: x => x.AssessmentTypeId,
                        principalSchema: "app_lookup",
                        principalTable: "CompetencyAssessmentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetencyAssessments_CompetencyMatrixVersions_MatrixVersio~",
                        column: x => x.MatrixVersionId,
                        principalSchema: "app",
                        principalTable: "CompetencyMatrixVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetencyAssessments_Employees_AssessorEmployeeId",
                        column: x => x.AssessorEmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetencyAssessments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyMatrixSections",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MatrixVersionId = table.Column<Guid>(type: "uuid", nullable: false),
                    SectionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_CompetencyMatrixSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencyMatrixSections_CompetencyMatrixVersions_MatrixVer~",
                        column: x => x.MatrixVersionId,
                        principalSchema: "app",
                        principalTable: "CompetencyMatrixVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetencyMatrixSections_CompetencySectionType_SectionTypeId",
                        column: x => x.SectionTypeId,
                        principalSchema: "app_lookup",
                        principalTable: "CompetencySectionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DevelopmentPlanItems",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DevelopmentNeedId = table.Column<Guid>(type: "uuid", nullable: false),
                    ControlMethodId = table.Column<Guid>(type: "uuid", nullable: true),
                    DevelopmentMethodId = table.Column<Guid>(type: "uuid", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Goal = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Deadline = table.Column<DateOnly>(type: "date", nullable: true),
                    Actions = table.Column<string[]>(type: "text[]", nullable: true),
                    CompletionPercent = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
                    Comments = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_DevelopmentPlanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevelopmentPlanItems_DevelopmentControlMethods_ControlMetho~",
                        column: x => x.ControlMethodId,
                        principalSchema: "app_lookup",
                        principalTable: "DevelopmentControlMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DevelopmentPlanItems_DevelopmentItemType_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalSchema: "app_lookup",
                        principalTable: "DevelopmentItemType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DevelopmentPlanItems_DevelopmentMethods_DevelopmentMethodId",
                        column: x => x.DevelopmentMethodId,
                        principalSchema: "app_lookup",
                        principalTable: "DevelopmentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DevelopmentPlanItems_DevelopmentNeeds_DevelopmentNeedId",
                        column: x => x.DevelopmentNeedId,
                        principalSchema: "app_lookup",
                        principalTable: "DevelopmentNeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DevelopmentPlanItems_DevelopmentPlans_PlanId",
                        column: x => x.PlanId,
                        principalSchema: "app",
                        principalTable: "DevelopmentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attestations",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManagerEmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    MatrixAssessmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PlannedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CompletedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    FinalScore = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
                    FinalComment = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_Attestations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attestations_CompetencyAssessments_MatrixAssessmentId",
                        column: x => x.MatrixAssessmentId,
                        principalSchema: "app",
                        principalTable: "CompetencyAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attestations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attestations_Employees_ManagerEmployeeId",
                        column: x => x.ManagerEmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyMatrixItems",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_CompetencyMatrixItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencyMatrixItems_CompetencyMatrixItems_ParentItemId",
                        column: x => x.ParentItemId,
                        principalSchema: "app",
                        principalTable: "CompetencyMatrixItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetencyMatrixItems_CompetencyMatrixSections_SectionId",
                        column: x => x.SectionId,
                        principalSchema: "app",
                        principalTable: "CompetencyMatrixSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponsibleEmployeesOfPlanItem",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResponsibleEmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_ResponsibleEmployeesOfPlanItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResponsibleEmployeesOfPlanItem_DevelopmentPlanItems_PlanIte~",
                        column: x => x.PlanItemId,
                        principalSchema: "app",
                        principalTable: "DevelopmentPlanItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponsibleEmployeesOfPlanItem_Employees_ResponsibleEmploye~",
                        column: x => x.ResponsibleEmployeeId,
                        principalSchema: "app",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttestationSections",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttestationId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttestationSectionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_AttestationSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttestationSections_AttestationSectionType_AttestationSecti~",
                        column: x => x.AttestationSectionTypeId,
                        principalSchema: "app_lookup",
                        principalTable: "AttestationSectionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttestationSections_Attestations_AttestationId",
                        column: x => x.AttestationId,
                        principalSchema: "app",
                        principalTable: "Attestations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyAssessmentItems",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssessmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    MatrixItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReferenceAssessmentValueId = table.Column<Guid>(type: "uuid", nullable: true),
                    ManagerAssessmentValueId = table.Column<Guid>(type: "uuid", nullable: true),
                    EmployeeAssessmentValueId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_CompetencyAssessmentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencyAssessmentItems_CompetencyAssessmentItemValues_Em~",
                        column: x => x.EmployeeAssessmentValueId,
                        principalSchema: "app",
                        principalTable: "CompetencyAssessmentItemValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CompetencyAssessmentItems_CompetencyAssessmentItemValues_Ma~",
                        column: x => x.ManagerAssessmentValueId,
                        principalSchema: "app",
                        principalTable: "CompetencyAssessmentItemValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CompetencyAssessmentItems_CompetencyAssessmentItemValues_Re~",
                        column: x => x.ReferenceAssessmentValueId,
                        principalSchema: "app",
                        principalTable: "CompetencyAssessmentItemValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CompetencyAssessmentItems_CompetencyAssessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalSchema: "app",
                        principalTable: "CompetencyAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetencyAssessmentItems_CompetencyMatrixItems_MatrixItemId",
                        column: x => x.MatrixItemId,
                        principalSchema: "app",
                        principalTable: "CompetencyMatrixItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyMatrixTargets",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MatrixItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    GradeId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReferenceValue = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
                    ReferenceText = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
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
                    table.PrimaryKey("PK_CompetencyMatrixTargets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencyMatrixTargets_CompetencyMatrixItems_MatrixItemId",
                        column: x => x.MatrixItemId,
                        principalSchema: "app",
                        principalTable: "CompetencyMatrixItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetencyMatrixTargets_Grades_GradeId",
                        column: x => x.GradeId,
                        principalSchema: "app_lookup",
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CompetencyMatrixTargets_Positions_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "app_lookup",
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttestationQuestions",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    IsAiDraft = table.Column<bool>(type: "boolean", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsOralAnswer = table.Column<bool>(type: "boolean", nullable: false),
                    IsSelfWriteAnswer = table.Column<bool>(type: "boolean", nullable: false),
                    AiPromptContext = table.Column<string>(type: "text", nullable: true),
                    MaxScore = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true),
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
                    table.PrimaryKey("PK_AttestationQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttestationQuestions_AttestationQuestionType_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalSchema: "app_lookup",
                        principalTable: "AttestationQuestionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttestationQuestions_AttestationSections_SectionId",
                        column: x => x.SectionId,
                        principalSchema: "app",
                        principalTable: "AttestationSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttestationAnswerOptions",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_AttestationAnswerOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttestationAnswerOptions_AttestationQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "app",
                        principalTable: "AttestationQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "AttestationQuestionType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("400a6b76-3848-4ed8-9f87-66781eb5ad08"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Вопрос для ответа на который нужно выбрать только один ответ.", null, false, "Один ответ", null, null },
                    { new Guid("5e8ae364-20b4-4d87-a1dd-129933dc3f98"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Задание на практическое выполнение.", null, false, "Практика", null, null },
                    { new Guid("7d13cc7d-a26d-46f5-bc97-99cd1ef970a3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Вопрос для ответа на который нужно выбрать только один или несколько ответов.", null, false, "Множественный выбор", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "AttestationSectionType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "ForPromotion", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("135d60c7-8db3-44c5-ba38-40941c38e79f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, false, false, "Дополнительные вопросы по регламентам", null, null },
                    { new Guid("3107999a-8171-4603-9920-1049d28406d4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, false, false, "Практические вопросы", null, null },
                    { new Guid("48e20b26-4a1e-400c-bc4e-765ddfc67f5d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, false, false, "Тестовые вопросы", null, null },
                    { new Guid("664eecb2-ad07-4758-9374-840edead6e6c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Практическая часть или Проектная работа", null, null },
                    { new Guid("97694f85-3780-4fac-8f0f-f7d146482794"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Вопросы по развитию личностных навыков", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "CompetencyAssessmentType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "FileId", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("9f8e4471-55a6-4c35-9784-339775fb76ed"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Оценка руководителя", null, null },
                    { new Guid("ec327914-2553-417e-9293-7f1a257538e9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Оценка сотрудника", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "CompetencySectionType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsActive", "IsDeleted", "Name", "SortOrder", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("483a5019-76e0-444d-8b3d-07414af3aeab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Професиональные компетенции сотруника", null, true, false, "Профессиональные компетенции", 1, null, null },
                    { new Guid("b294a9f3-5ade-4065-a651-7204b9c1d09d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Личностные компетенции сотруника", null, true, false, "Личностные компетенции", 2, null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "DevelopmentControlMethods",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("9a4a148b-020d-46ae-ab9a-ae25c0f74556"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Беседа, выполнение действий на машине", null, null },
                    { new Guid("a8006951-8d75-4510-bacf-967d48779aa3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Беседа, выполнение действий на оборудовании", null, null },
                    { new Guid("d315ce3c-2cd7-44d7-8bbe-7290307b85d2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Беседа", null, null },
                    { new Guid("daa84e6a-5c60-4d07-b92f-2dc7db57ac22"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Проведение ТО-2 ТПА", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "DevelopmentItemType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "FileId", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Профессиональные компетенции", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Личностные компетенции", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Проект", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsControl", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("016e6c89-ad0e-4969-a004-38713a329c61"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, "Выполнение ремонта на машине совместно с сотрудниками СС", null, null },
                    { new Guid("033d6925-6d4e-41d3-bdca-92d8c96b952f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Лекции от технологов", null, null },
                    { new Guid("08434097-599e-4676-8b88-5d083e79f5b1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Самостоятельное изучение", null, null },
                    { new Guid("3a9f9698-88b6-4834-a055-ec3935f607fe"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Практика", null, null },
                    { new Guid("65400c7b-662d-46de-bd2f-e0f035f50887"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Списание инструмента с карточки сотрудника", null, null },
                    { new Guid("81e0c037-fe5b-4b05-aaf6-f05ef72769af"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, "Лекции от сотрудников СС", null, null },
                    { new Guid("947f3616-84c8-4e9e-acb7-bcaae61bf220"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Заполнени отчета после испытаний совместно с технологами", null, null },
                    { new Guid("b1200c92-895a-4fb3-aa6f-4e2b9047d17b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Подбор комплектующих для оборудования", null, null },
                    { new Guid("b9ff6c64-6c01-4f25-beba-806d8c1e252d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Самостоятельное изучение материала, лекции от Менеджера по СМК", null, null },
                    { new Guid("fecafe0d-cc2d-400d-a388-c9dadcd21809"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, "Навешивание ПФ совместно с сотрудниками СС", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "DevelopmentPlanType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "FileId", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("e18bc645-2acc-4d07-87ec-159529ded17e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Повышение должности", null, null },
                    { new Guid("f43f5cdf-836b-467e-891b-48112a10f029"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Подтверждение должности", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "EducationType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("12e1cff1-6139-4631-af8e-702604e65666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "1–4 классы школы (базовые навыки чтения, письма, счета)", null, false, "Начальное общее", null, null },
                    { new Guid("23024474-050b-4782-91e8-2140db526a7d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Повышение квалификации и профессиональная переподготовка", null, false, "Дополнительное профессиональное образование (ДПО)", null, null },
                    { new Guid("3b207b2c-4b3a-4e09-b57a-0fc2f8c6c702"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка кадров высшей квалификации", null, false, "Ординатура", null, null },
                    { new Guid("5c34ff2a-2f44-4b01-9579-c190402e2111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка высококвалифицированных кадров", null, false, "Магистратура", null, null },
                    { new Guid("6c333bc7-6980-40ca-85e7-87472cebac1e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "10–11 классы школы (завершение общего образования)", null, false, "Среднее общее образование", null, null },
                    { new Guid("7a230f27-fe49-4e77-bcfc-58f9325f0ae9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка кадров высшей квалификации", null, false, "Аспирантура", null, null },
                    { new Guid("7d604675-7b70-4546-93b4-7a9d6357c2ef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Воспитание и обучение детей в детских садах", null, false, "Дошкольное", null, null },
                    { new Guid("87139e74-2b35-407d-ab66-39c6f1d32567"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка кадров высшей квалификации", null, false, "Ассистентура-стажировка", null, null },
                    { new Guid("9dd93eb6-22d3-4177-bf39-98e74d9520b7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Обучение в колледжах или техникумах после 9 или 11 класса", null, false, "Среднее профессиональное образование (СПО)", null, null },
                    { new Guid("d75f86f8-f249-4943-afc2-9e4a71b3ef71"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Кружки, секции, творческие студии", null, false, "Дополнительное образование детей и взрослых", null, null },
                    { new Guid("df002f57-595f-4ea0-93eb-ba4a6ce4b1f9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка высококвалифицированных кадров", null, false, "Специалитет", null, null },
                    { new Guid("f3756199-c246-48b5-a784-ff9459739034"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "5–9 классы школы (обязательный уровень)", null, false, "Основное общее", null, null },
                    { new Guid("f98a9344-4194-4c4c-ad5b-3a162aa0bd79"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка высококвалифицированных кадров", null, false, "Бакалавриат", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "EmployeeContactType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsActive", "IsDeleted", "Name", "SortOrder", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("3f934caa-efa2-4e6c-b42e-e9ac8f6efa0a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Внутрений рабочимй номер телефона сотрудника", null, true, false, "Внутрений телефон", 3, null, null },
                    { new Guid("706969aa-486e-4ee8-ac18-d05f34c70a4b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Ссылка на контакт в мессенджере WhatsApp", null, true, false, "WhatsApp", 4, null, null },
                    { new Guid("a416a68d-c269-406e-bd44-7a73775ef195"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Адресс электронной почты", null, true, false, "Email", 1, null, null },
                    { new Guid("cd1483a3-4554-4108-b9bb-0e7e65819309"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Личный или рабочимй номер телефон сотрудника", null, true, false, "Телефон", 2, null, null },
                    { new Guid("cda2e2e3-944d-4817-aea8-373547b7cb62"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Ссылка на контакт в мессенджере Telegram", null, true, false, "Telegram", 5, null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsActive", "IsDeleted", "Name", "SortOrder", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("28709016-2293-478c-8287-0f06aef33f84"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Востановлен с повышением", 10, null, null },
                    { new Guid("5c3c4c10-ad63-486e-85e7-23da76960ede"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Повышен", 4, null, null },
                    { new Guid("79778ad8-be41-44ec-bcf5-25a9b42a8303"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Уволен", 7, null, null },
                    { new Guid("7c4232bd-5233-48b3-863a-b8c079035fd5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Нанят", 2, null, null },
                    { new Guid("8d44deac-24e5-4ff8-b0bd-117b3bbef882"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Обучение", 5, null, null },
                    { new Guid("9b576a00-6a70-4f0a-b996-817f016ce388"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Востановлен с понижением", 9, null, null },
                    { new Guid("c2a4f4fd-4328-4c2f-81b0-a2b92a7ce067"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Востановлен на прежнюю должность", 8, null, null },
                    { new Guid("db1f0ed6-9d84-4536-ba46-1db321f0fd93"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Стажировка", 1, null, null },
                    { new Guid("f63bbd0b-081f-4a98-ab82-b888f09fbb4b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Переведен", 3, null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "Name", "TypeId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("03ea6d8c-fb9c-45dc-b589-126e076ab4e8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Презентация проекта", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("16f677ab-0314-40d1-ab6f-90099483d770"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Работа с ручным и электро инструментом", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("1f07ecab-585f-4f33-9b0d-c6560e16a058"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Организация и планирование", new Guid("10000000-0000-0000-0000-000000000002"), null, null },
                    { new Guid("262c7d6b-05cf-4c7f-a264-18fbf57d7fb6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Пищевая безопасность", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("278c967d-3d23-4c09-8377-6c10d34a7215"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Технология производства", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("2efde89f-66fb-4e8d-beb2-944a1a542fa4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Технологические регламенты", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("30568ab9-31ec-414a-8e30-980d764fd988"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Предоставление данных сотрудникам", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("4ad17a34-26dd-4216-8b3c-c2a01f1c5ae5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Документы СМК", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("591d1482-7c34-4377-9572-e4773ba70425"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Презентация выполненного проекта", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("5e1593bf-e290-49c6-91c3-21216ca154b2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Работа с технической документацией", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("72111a66-6892-43a1-9273-c2bd394d747a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Актуализация", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("730629af-4007-4423-b604-a1f048806896"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Локально нормативные акты", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("7e4a82da-13ae-4a12-93a6-de7f39588286"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Обучение и мотивация на развитие", new Guid("10000000-0000-0000-0000-000000000002"), null, null },
                    { new Guid("8f85a850-09ab-49e0-aa75-0cd060004e8f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Создание таблицы", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("8fc2deb1-3258-402f-a744-645a34fa67b0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Управленческие навыки", new Guid("10000000-0000-0000-0000-000000000002"), null, null },
                    { new Guid("9b5a97f6-0e59-4ecb-9545-043d382c0fb8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Общие-технические знания", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("a53f0d6c-221e-4618-beab-fe17d1e0809c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Охрана труда", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("a6afe381-7e69-40d2-afb3-783eef10cbfc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Наладка и обслуживание пресс-форм", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("ae8188b3-5c67-4d7b-9854-fbde5a6aa84d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Отчетность", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("bc103f5f-edfb-4c3e-a826-7069733e1dac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Ремонтные работы при экстплаутаци ТПА", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("dc0df98f-c941-4587-9626-1f228720fca9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Поиск источников", new Guid("10000000-0000-0000-0000-000000000003"), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FileId",
                schema: "app",
                table: "Employees",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LastName_FirstName",
                schema: "app",
                table: "Employees",
                columns: new[] { "LastName", "FirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                schema: "app",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AttestationAnswerOptions_QuestionId_SortOrder",
                schema: "app",
                table: "AttestationAnswerOptions",
                columns: new[] { "QuestionId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_AttestationQuestions_QuestionTypeId",
                schema: "app",
                table: "AttestationQuestions",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttestationQuestions_SectionId_SortOrder",
                schema: "app",
                table: "AttestationQuestions",
                columns: new[] { "SectionId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_AttestationQuestionType_FileId",
                schema: "app_lookup",
                table: "AttestationQuestionType",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_AttestationQuestionType_IsDeleted",
                schema: "app_lookup",
                table: "AttestationQuestionType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Attestations_EmployeeId",
                schema: "app",
                table: "Attestations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attestations_ManagerEmployeeId",
                schema: "app",
                table: "Attestations",
                column: "ManagerEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attestations_MatrixAssessmentId",
                schema: "app",
                table: "Attestations",
                column: "MatrixAssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AttestationSections_AttestationId_SortOrder",
                schema: "app",
                table: "AttestationSections",
                columns: new[] { "AttestationId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_AttestationSections_AttestationSectionTypeId",
                schema: "app",
                table: "AttestationSections",
                column: "AttestationSectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttestationSectionType_FileId",
                schema: "app_lookup",
                table: "AttestationSectionType",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_AttestationSectionType_IsDeleted",
                schema: "app_lookup",
                table: "AttestationSectionType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyAssessmentItems_AssessmentId_MatrixItemId",
                schema: "app",
                table: "CompetencyAssessmentItems",
                columns: new[] { "AssessmentId", "MatrixItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyAssessmentItems_EmployeeAssessmentValueId",
                schema: "app",
                table: "CompetencyAssessmentItems",
                column: "EmployeeAssessmentValueId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyAssessmentItems_ManagerAssessmentValueId",
                schema: "app",
                table: "CompetencyAssessmentItems",
                column: "ManagerAssessmentValueId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyAssessmentItems_MatrixItemId",
                schema: "app",
                table: "CompetencyAssessmentItems",
                column: "MatrixItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyAssessmentItems_ReferenceAssessmentValueId",
                schema: "app",
                table: "CompetencyAssessmentItems",
                column: "ReferenceAssessmentValueId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyAssessmentItemValues_IsDeleted",
                schema: "app",
                table: "CompetencyAssessmentItemValues",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyAssessments_AssessmentTypeId",
                schema: "app",
                table: "CompetencyAssessments",
                column: "AssessmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyAssessments_AssessorEmployeeId",
                schema: "app",
                table: "CompetencyAssessments",
                column: "AssessorEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyAssessments_EmployeeId_AssessmentDate",
                schema: "app",
                table: "CompetencyAssessments",
                columns: new[] { "EmployeeId", "AssessmentDate" });

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyAssessments_MatrixVersionId",
                schema: "app",
                table: "CompetencyAssessments",
                column: "MatrixVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyAssessmentType_FileId",
                schema: "app_lookup",
                table: "CompetencyAssessmentType",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyAssessmentType_IsDeleted",
                schema: "app_lookup",
                table: "CompetencyAssessmentType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyMatrices_IsActive_IsDeleted",
                schema: "app",
                table: "CompetencyMatrices",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyMatrixItems_IsDeleted_SectionId",
                schema: "app",
                table: "CompetencyMatrixItems",
                columns: new[] { "IsDeleted", "SectionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyMatrixItems_ParentItemId",
                schema: "app",
                table: "CompetencyMatrixItems",
                column: "ParentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyMatrixItems_SectionId",
                schema: "app",
                table: "CompetencyMatrixItems",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyMatrixSections_MatrixVersionId_SortOrder",
                schema: "app",
                table: "CompetencyMatrixSections",
                columns: new[] { "MatrixVersionId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyMatrixSections_SectionTypeId",
                schema: "app",
                table: "CompetencyMatrixSections",
                column: "SectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyMatrixTargets_GradeId",
                schema: "app",
                table: "CompetencyMatrixTargets",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyMatrixTargets_MatrixItemId_PositionId_GradeId",
                schema: "app",
                table: "CompetencyMatrixTargets",
                columns: new[] { "MatrixItemId", "PositionId", "GradeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyMatrixTargets_PositionId",
                schema: "app",
                table: "CompetencyMatrixTargets",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyMatrixVersions_MatrixId_VersionNumber",
                schema: "app",
                table: "CompetencyMatrixVersions",
                columns: new[] { "MatrixId", "VersionNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetencySectionType_FileId",
                schema: "app_lookup",
                table: "CompetencySectionType",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencySectionType_IsActive_IsDeleted_SortOrder",
                schema: "app_lookup",
                table: "CompetencySectionType",
                columns: new[] { "IsActive", "IsDeleted", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FileId",
                schema: "app",
                table: "Departments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HeadEmployeeId",
                schema: "app",
                table: "Departments",
                column: "HeadEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentDepartmentId_IsActive_IsDeleted",
                schema: "app",
                table: "Departments",
                columns: new[] { "ParentDepartmentId", "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentControlMethods_IsDeleted",
                schema: "app_lookup",
                table: "DevelopmentControlMethods",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentItemType_FileId",
                schema: "app_lookup",
                table: "DevelopmentItemType",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentItemType_IsDeleted",
                schema: "app_lookup",
                table: "DevelopmentItemType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentMethods_IsDeleted",
                schema: "app_lookup",
                table: "DevelopmentMethods",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentNeeds_IsDeleted",
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentNeeds_TypeId",
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPlanItems_ControlMethodId",
                schema: "app",
                table: "DevelopmentPlanItems",
                column: "ControlMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPlanItems_DevelopmentMethodId",
                schema: "app",
                table: "DevelopmentPlanItems",
                column: "DevelopmentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPlanItems_DevelopmentNeedId",
                schema: "app",
                table: "DevelopmentPlanItems",
                column: "DevelopmentNeedId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPlanItems_ItemTypeId",
                schema: "app",
                table: "DevelopmentPlanItems",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPlanItems_PlanId_SortOrder",
                schema: "app",
                table: "DevelopmentPlanItems",
                columns: new[] { "PlanId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPlans_EmployeeId",
                schema: "app",
                table: "DevelopmentPlans",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPlans_ManagerEmployeeId",
                schema: "app",
                table: "DevelopmentPlans",
                column: "ManagerEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPlans_TypeId",
                schema: "app",
                table: "DevelopmentPlans",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPlanType_FileId",
                schema: "app_lookup",
                table: "DevelopmentPlanType",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPlanType_IsDeleted",
                schema: "app_lookup",
                table: "DevelopmentPlanType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EducationType_FileId",
                schema: "app_lookup",
                table: "EducationType",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationType_IsDeleted",
                schema: "app_lookup",
                table: "EducationType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContacts_EmployeeContactTypeId",
                schema: "app",
                table: "EmployeeContacts",
                column: "EmployeeContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContacts_EmployeeId_EmployeeContactTypeId_IsPrimary",
                schema: "app",
                table: "EmployeeContacts",
                columns: new[] { "EmployeeId", "EmployeeContactTypeId", "IsPrimary" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContactType_FileId",
                schema: "app_lookup",
                table: "EmployeeContactType",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContactType_IsActive_IsDeleted_SortOrder",
                schema: "app_lookup",
                table: "EmployeeContactType",
                columns: new[] { "IsActive", "IsDeleted", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducation_EducationTypeId",
                schema: "app",
                table: "EmployeeEducation",
                column: "EducationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducation_EmployeeId_EducationTypeId",
                schema: "app",
                table: "EmployeeEducation",
                columns: new[] { "EmployeeId", "EducationTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducation_FileId",
                schema: "app",
                table: "EmployeeEducation",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducation_IsDeleted",
                schema: "app",
                table: "EmployeeEducation",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeQualifications_EmployeeId",
                schema: "app",
                table: "EmployeeQualifications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeQualifications_FileId",
                schema: "app",
                table: "EmployeeQualifications",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeQualifications_IsDeleted",
                schema: "app",
                table: "EmployeeQualifications",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentHistory_DepartmentId",
                schema: "app",
                table: "EmploymentHistory",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentHistory_EmployeeId_ValidFrom",
                schema: "app",
                table: "EmploymentHistory",
                columns: new[] { "EmployeeId", "ValidFrom" });

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentHistory_EmployeeId_ValidTo",
                schema: "app",
                table: "EmploymentHistory",
                columns: new[] { "EmployeeId", "ValidTo" });

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentHistory_GradeId",
                schema: "app",
                table: "EmploymentHistory",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentHistory_ManagerEmployeeId",
                schema: "app",
                table: "EmploymentHistory",
                column: "ManagerEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentHistory_PositionId",
                schema: "app",
                table: "EmploymentHistory",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentHistory_RecordTypeId",
                schema: "app",
                table: "EmploymentHistory",
                column: "RecordTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentRecordType_FileId",
                schema: "app_lookup",
                table: "EmploymentRecordType",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentRecordType_IsActive_IsDeleted_SortOrder",
                schema: "app_lookup",
                table: "EmploymentRecordType",
                columns: new[] { "IsActive", "IsDeleted", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_IsActive_IsDeleted_SortOrder",
                schema: "app_lookup",
                table: "Grades",
                columns: new[] { "IsActive", "IsDeleted", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Positions_IsActive_IsDeleted_Name",
                schema: "app_lookup",
                table: "Positions",
                columns: new[] { "IsActive", "IsDeleted", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Positions_IsActive_IsDeleted_SortOrder",
                schema: "app_lookup",
                table: "Positions",
                columns: new[] { "IsActive", "IsDeleted", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_ProbationPlans_EmployeeId",
                schema: "app",
                table: "ProbationPlans",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProbationPlans_ManagerEmployeeId",
                schema: "app",
                table: "ProbationPlans",
                column: "ManagerEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProbationTasks_PlanId_SortOrder",
                schema: "app",
                table: "ProbationTasks",
                columns: new[] { "PlanId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_ResponsibleEmployeesOfPlanItem_PlanItemId",
                schema: "app",
                table: "ResponsibleEmployeesOfPlanItem",
                column: "PlanItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsibleEmployeesOfPlanItem_ResponsibleEmployeeId",
                schema: "app",
                table: "ResponsibleEmployeesOfPlanItem",
                column: "ResponsibleEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Files_FileId",
                schema: "app",
                table: "Employees",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_UserId",
                schema: "app",
                table: "Employees",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseNoteAttachments_Files_FileId",
                schema: "updates",
                table: "ReleaseNoteAttachments",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportTemplateVersions_Files_FileId",
                schema: "reports",
                table: "ReportTemplateVersions",
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
                name: "FK_Employees_Files_FileId",
                schema: "app",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_UserId",
                schema: "app",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseNoteAttachments_Files_FileId",
                schema: "updates",
                table: "ReleaseNoteAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportTemplateVersions_Files_FileId",
                schema: "reports",
                table: "ReportTemplateVersions");

            migrationBuilder.DropTable(
                name: "AttestationAnswerOptions",
                schema: "app");

            migrationBuilder.DropTable(
                name: "CompetencyAssessmentItems",
                schema: "app");

            migrationBuilder.DropTable(
                name: "CompetencyMatrixTargets",
                schema: "app");

            migrationBuilder.DropTable(
                name: "EmployeeContacts",
                schema: "app");

            migrationBuilder.DropTable(
                name: "EmployeeEducation",
                schema: "app");

            migrationBuilder.DropTable(
                name: "EmployeeQualifications",
                schema: "app");

            migrationBuilder.DropTable(
                name: "EmploymentHistory",
                schema: "app");

            migrationBuilder.DropTable(
                name: "ProbationTasks",
                schema: "app");

            migrationBuilder.DropTable(
                name: "ResponsibleEmployeesOfPlanItem",
                schema: "app");

            migrationBuilder.DropTable(
                name: "AttestationQuestions",
                schema: "app");

            migrationBuilder.DropTable(
                name: "CompetencyAssessmentItemValues",
                schema: "app");

            migrationBuilder.DropTable(
                name: "CompetencyMatrixItems",
                schema: "app");

            migrationBuilder.DropTable(
                name: "EmployeeContactType",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "EducationType",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "app");

            migrationBuilder.DropTable(
                name: "EmploymentRecordType",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "Grades",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "Positions",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "ProbationPlans",
                schema: "app");

            migrationBuilder.DropTable(
                name: "DevelopmentPlanItems",
                schema: "app");

            migrationBuilder.DropTable(
                name: "AttestationQuestionType",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "AttestationSections",
                schema: "app");

            migrationBuilder.DropTable(
                name: "CompetencyMatrixSections",
                schema: "app");

            migrationBuilder.DropTable(
                name: "DevelopmentControlMethods",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "DevelopmentMethods",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "DevelopmentNeeds",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "DevelopmentPlans",
                schema: "app");

            migrationBuilder.DropTable(
                name: "AttestationSectionType",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "Attestations",
                schema: "app");

            migrationBuilder.DropTable(
                name: "CompetencySectionType",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "DevelopmentItemType",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "DevelopmentPlanType",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "CompetencyAssessments",
                schema: "app");

            migrationBuilder.DropTable(
                name: "CompetencyAssessmentType",
                schema: "app_lookup");

            migrationBuilder.DropTable(
                name: "CompetencyMatrixVersions",
                schema: "app");

            migrationBuilder.DropTable(
                name: "CompetencyMatrices",
                schema: "app");

            migrationBuilder.DropIndex(
                name: "IX_Employees_FileId",
                schema: "app",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_LastName_FirstName",
                schema: "app",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                schema: "app",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                schema: "app",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FileId",
                schema: "app",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HireDate",
                schema: "app",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NickName",
                schema: "app",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ShortName",
                schema: "app",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "app",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "updates",
                table: "Versions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "updates_lookup",
                table: "UpdateCategories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "support_lookup",
                table: "TicketStatuses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "support_lookup",
                table: "TicketPriorities",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "support_lookup",
                table: "TicketCategories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "auth_lookup",
                table: "Roles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "reports",
                table: "ReportTemplateVersions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "reports_lookup",
                table: "ReportExecutionStatuses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "reports_lookup",
                table: "ReportDataTypies",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "reports_lookup",
                table: "ReportCategories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "updates_lookup",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "updates_lookup",
                table: "Platforms",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "auth_lookup",
                table: "Permissions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "localization_lookup",
                table: "Languages",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FamilyName",
                schema: "app",
                table: "Employees",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionSources",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "auth_lookup",
                table: "AuthenticationMethods",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileId1",
                schema: "app_logs_lookup",
                table: "AuditActions",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "app_logs_lookup",
                table: "AuditActions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "app_logs_lookup",
                table: "AuditActions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "app_logs_lookup",
                table: "AuditActions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "app_logs_lookup",
                table: "AuditActions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "app_logs_lookup",
                table: "AuditActions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "AuthenticationMethods",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionSources",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionSources",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000007"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000005"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000006"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000007"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000008"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000009"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000005"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000006"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000007"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000008"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000009"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000010"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "localization_lookup",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "localization_lookup",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000007"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000008"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000009"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000010"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000011"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000012"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000013"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000014"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000015"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000016"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000017"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000018"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000019"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000020"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000021"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000022"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000023"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000024"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000025"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000026"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000027"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000028"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000029"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000030"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000031"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000032"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000033"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000034"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000035"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000036"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000037"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000038"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000039"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000040"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000005"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("13000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportCategories",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportCategories",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportCategories",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportCategories",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportDataTypies",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportDataTypies",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportDataTypies",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportDataTypies",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportDataTypies",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportDataTypies",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportDataTypies",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportExecutionStatuses",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportExecutionStatuses",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportExecutionStatuses",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportExecutionStatuses",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "reports_lookup",
                table: "ReportExecutionStatuses",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "auth_lookup",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketCategories",
                keyColumn: "Id",
                keyValue: new Guid("92000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketCategories",
                keyColumn: "Id",
                keyValue: new Guid("92000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketCategories",
                keyColumn: "Id",
                keyValue: new Guid("92000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketCategories",
                keyColumn: "Id",
                keyValue: new Guid("92000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketCategories",
                keyColumn: "Id",
                keyValue: new Guid("92000000-0000-0000-0000-000000000005"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketPriorities",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketPriorities",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketPriorities",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketPriorities",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000004"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000005"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "support_lookup",
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000006"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "UpdateCategories",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000001"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "UpdateCategories",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000002"),
                column: "FileId1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "updates_lookup",
                table: "UpdateCategories",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000003"),
                column: "FileId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Versions_FileId1",
                schema: "updates",
                table: "Versions",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_UpdateCategories_FileId1",
                schema: "updates_lookup",
                table: "UpdateCategories",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStatuses_FileId1",
                schema: "support_lookup",
                table: "TicketStatuses",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPriorities_FileId1",
                schema: "support_lookup",
                table: "TicketPriorities",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_TicketCategories_FileId1",
                schema: "support_lookup",
                table: "TicketCategories",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_FileId1",
                schema: "auth_lookup",
                table: "Roles",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateVersions_FileId1",
                schema: "reports",
                table: "ReportTemplateVersions",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_ReportExecutionStatuses_FileId1",
                schema: "reports_lookup",
                table: "ReportExecutionStatuses",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDataTypies_FileId1",
                schema: "reports_lookup",
                table: "ReportDataTypies",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCategories_FileId1",
                schema: "reports_lookup",
                table: "ReportCategories",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FileId1",
                schema: "updates_lookup",
                table: "Products",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_FileId1",
                schema: "updates_lookup",
                table: "Platforms",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_FileId1",
                schema: "auth_lookup",
                table: "Permissions",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_FileId1",
                schema: "localization_lookup",
                table: "Languages",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticStatisticsWorkItemStatuses_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticDefinitions_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticDefinitionGroups_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreas_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionSources_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionSources",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionRules_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticationMethods_FileId1",
                schema: "auth_lookup",
                table: "AuthenticationMethods",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_AuditActions_FileId1",
                schema: "app_logs_lookup",
                table: "AuditActions",
                column: "FileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticDefinitionGroups_Files_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticDefinitions_Files_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosticStatisticsWorkItemStatuses_Files_FileId1",
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Files_FileId1",
                schema: "localization_lookup",
                table: "Languages",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Files_FileId1",
                schema: "auth_lookup",
                table: "Permissions",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Platforms_Files_FileId1",
                schema: "updates_lookup",
                table: "Platforms",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Files_FileId1",
                schema: "updates_lookup",
                table: "Products",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseNoteAttachments_Files_FileId",
                schema: "updates",
                table: "ReleaseNoteAttachments",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportCategories_Files_FileId1",
                schema: "reports_lookup",
                table: "ReportCategories",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportDataTypies_Files_FileId1",
                schema: "reports_lookup",
                table: "ReportDataTypies",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportExecutionStatuses_Files_FileId1",
                schema: "reports_lookup",
                table: "ReportExecutionStatuses",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportTemplateVersions_Files_FileId",
                schema: "reports",
                table: "ReportTemplateVersions",
                column: "FileId",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportTemplateVersions_Files_FileId1",
                schema: "reports",
                table: "ReportTemplateVersions",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Files_FileId1",
                schema: "auth_lookup",
                table: "Roles",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketCategories_Files_FileId1",
                schema: "support_lookup",
                table: "TicketCategories",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPriorities_Files_FileId1",
                schema: "support_lookup",
                table: "TicketPriorities",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketStatuses_Files_FileId1",
                schema: "support_lookup",
                table: "TicketStatuses",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UpdateCategories_Files_FileId1",
                schema: "updates_lookup",
                table: "UpdateCategories",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Versions_Files_FileId1",
                schema: "updates",
                table: "Versions",
                column: "FileId1",
                principalSchema: "files",
                principalTable: "Files",
                principalColumn: "Id");
        }
    }
}
