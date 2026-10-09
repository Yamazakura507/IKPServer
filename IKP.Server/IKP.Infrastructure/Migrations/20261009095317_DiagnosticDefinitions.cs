using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DiagnosticDefinitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiagnosticDefinitionGroups",
                schema: "diagnostics",
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
                    table.PrimaryKey("PK_DiagnosticDefinitionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticDefinitions",
                schema: "diagnostics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_DiagnosticDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiagnosticDefinitions_DiagnosticDefinitionGroups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "diagnostics",
                        principalTable: "DiagnosticDefinitionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "diagnostics",
                table: "DiagnosticDefinitionGroups",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), "DIAGNOSTIC_SEVERITY", "Diagnostic.DefinitionGroup.Severity.Description", null, true, true, "Diagnostic.DefinitionGroup.Severity", 10 },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "ERROR_GROUP_STATUS", "Diagnostic.DefinitionGroup.ErrorGroupStatus.Description", null, true, true, "Diagnostic.DefinitionGroup.ErrorGroupStatus", 20 },
                    { new Guid("20000000-0000-0000-0000-000000000003"), "CRITICALITY_FACTOR", "Diagnostic.DefinitionGroup.CriticalityFactor.Description", null, true, true, "Diagnostic.DefinitionGroup.CriticalityFactor", 30 },
                    { new Guid("20000000-0000-0000-0000-000000000004"), "METRIC_BUCKET_TYPE", "Diagnostic.DefinitionGroup.MetricBucketType.Description", null, true, true, "Diagnostic.DefinitionGroup.MetricBucketType", 40 }
                });

            migrationBuilder.InsertData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "GroupId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("21000000-0000-0000-0000-000000000001"), "INFORMATION", null, null, new Guid("20000000-0000-0000-0000-000000000001"), true, true, "Diagnostic.Severity.Information", 10 },
                    { new Guid("21000000-0000-0000-0000-000000000002"), "WARNING", null, null, new Guid("20000000-0000-0000-0000-000000000001"), true, true, "Diagnostic.Severity.Warning", 20 },
                    { new Guid("21000000-0000-0000-0000-000000000003"), "ERROR", null, null, new Guid("20000000-0000-0000-0000-000000000001"), true, true, "Diagnostic.Severity.Error", 30 },
                    { new Guid("21000000-0000-0000-0000-000000000004"), "CRITICAL", null, null, new Guid("20000000-0000-0000-0000-000000000001"), true, true, "Diagnostic.Severity.Critical", 40 },
                    { new Guid("22000000-0000-0000-0000-000000000001"), "NEW", null, null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.New", 10 },
                    { new Guid("22000000-0000-0000-0000-000000000002"), "INVESTIGATING", null, null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.Investigating", 20 },
                    { new Guid("22000000-0000-0000-0000-000000000003"), "KNOWN", null, null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.Known", 30 },
                    { new Guid("22000000-0000-0000-0000-000000000004"), "FIX_PLANNED", null, null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.FixPlanned", 40 },
                    { new Guid("22000000-0000-0000-0000-000000000005"), "FIXED", null, null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.Fixed", 50 },
                    { new Guid("22000000-0000-0000-0000-000000000006"), "MONITORING", null, null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.Monitoring", 60 },
                    { new Guid("22000000-0000-0000-0000-000000000007"), "RESOLVED", null, null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.Resolved", 70 },
                    { new Guid("22000000-0000-0000-0000-000000000008"), "IGNORED", null, null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.Ignored", 80 },
                    { new Guid("22000000-0000-0000-0000-000000000009"), "WONT_FIX", null, null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.WontFix", 90 },
                    { new Guid("23000000-0000-0000-0000-000000000001"), "OCCURRENCE_RATE", null, null, new Guid("20000000-0000-0000-0000-000000000003"), true, true, "Diagnostic.CriticalityFactor.OccurrenceRate", 10 },
                    { new Guid("23000000-0000-0000-0000-000000000002"), "AFFECTED_USERS", null, null, new Guid("20000000-0000-0000-0000-000000000003"), true, true, "Diagnostic.CriticalityFactor.AffectedUsers", 20 },
                    { new Guid("23000000-0000-0000-0000-000000000003"), "AFFECTED_USER_RATIO", null, null, new Guid("20000000-0000-0000-0000-000000000003"), true, true, "Diagnostic.CriticalityFactor.AffectedUserRatio", 30 },
                    { new Guid("23000000-0000-0000-0000-000000000004"), "FEATURE_IMPORTANCE", null, null, new Guid("20000000-0000-0000-0000-000000000003"), true, true, "Diagnostic.CriticalityFactor.FeatureImportance", 40 },
                    { new Guid("23000000-0000-0000-0000-000000000005"), "TREND", null, null, new Guid("20000000-0000-0000-0000-000000000003"), true, true, "Diagnostic.CriticalityFactor.Trend", 50 },
                    { new Guid("23000000-0000-0000-0000-000000000006"), "FAILURE_RATE", null, null, new Guid("20000000-0000-0000-0000-000000000003"), true, true, "Diagnostic.CriticalityFactor.FailureRate", 60 },
                    { new Guid("23000000-0000-0000-0000-000000000007"), "REGRESSION", null, null, new Guid("20000000-0000-0000-0000-000000000003"), true, true, "Diagnostic.CriticalityFactor.Regression", 70 },
                    { new Guid("23000000-0000-0000-0000-000000000008"), "BUSINESS_IMPACT", null, null, new Guid("20000000-0000-0000-0000-000000000003"), true, true, "Diagnostic.CriticalityFactor.BusinessImpact", 80 },
                    { new Guid("23000000-0000-0000-0000-000000000009"), "SUPPORT_IMPACT", null, null, new Guid("20000000-0000-0000-0000-000000000003"), true, true, "Diagnostic.CriticalityFactor.SupportImpact", 90 },
                    { new Guid("23000000-0000-0000-0000-000000000010"), "DATA_LOSS_RISK", null, null, new Guid("20000000-0000-0000-0000-000000000003"), true, true, "Diagnostic.CriticalityFactor.DataLossRisk", 100 },
                    { new Guid("24000000-0000-0000-0000-000000000001"), "MINUTE", null, null, new Guid("20000000-0000-0000-0000-000000000004"), true, true, "Diagnostic.MetricBucketType.Minute", 10 },
                    { new Guid("24000000-0000-0000-0000-000000000002"), "HOUR", null, null, new Guid("20000000-0000-0000-0000-000000000004"), true, true, "Diagnostic.MetricBucketType.Hour", 20 },
                    { new Guid("24000000-0000-0000-0000-000000000003"), "DAY", null, null, new Guid("20000000-0000-0000-0000-000000000004"), true, true, "Diagnostic.MetricBucketType.Day", 30 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticDefinitionGroups_Code",
                schema: "diagnostics",
                table: "DiagnosticDefinitionGroups",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticDefinitionGroups_IsActive_SortOrder",
                schema: "diagnostics",
                table: "DiagnosticDefinitionGroups",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticDefinitions_GroupId_Code",
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                columns: new[] { "GroupId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticDefinitions_GroupId_IsActive_SortOrder",
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                columns: new[] { "GroupId", "IsActive", "SortOrder" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagnosticDefinitions",
                schema: "diagnostics");

            migrationBuilder.DropTable(
                name: "DiagnosticDefinitionGroups",
                schema: "diagnostics");
        }
    }
}
