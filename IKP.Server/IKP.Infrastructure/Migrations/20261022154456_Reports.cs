using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Reports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "reports_lookup");

            migrationBuilder.EnsureSchema(
                name: "reports");

            migrationBuilder.CreateTable(
                name: "ReportCategories",
                schema: "reports_lookup",
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
                    table.PrimaryKey("PK_ReportCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportCategories_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ReportDataTypies",
                schema: "reports_lookup",
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
                    table.PrimaryKey("PK_ReportDataTypies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportDataTypies_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ReportExecutionStatuses",
                schema: "reports_lookup",
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
                    table.PrimaryKey("PK_ReportExecutionStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportExecutionStatuses_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ReportDefinitions",
                schema: "reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsHtmlDescription = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_ReportDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportDefinitions_ReportCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "reports_lookup",
                        principalTable: "ReportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportParameters",
                schema: "reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataTypeCodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    DefaultValue = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
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
                    table.PrimaryKey("PK_ReportParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportParameters_ReportDataTypies_DataTypeCodeId",
                        column: x => x.DataTypeCodeId,
                        principalSchema: "reports_lookup",
                        principalTable: "ReportDataTypies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportParameters_ReportDefinitions_ReportDefinitionId",
                        column: x => x.ReportDefinitionId,
                        principalSchema: "reports",
                        principalTable: "ReportDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportTemplateVersions",
                schema: "reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    ContentHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    FileId1 = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTemplateVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportTemplateVersions_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportTemplateVersions_Files_FileId1",
                        column: x => x.FileId1,
                        principalSchema: "files",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportTemplateVersions_ReportDefinitions_ReportDefinitionId",
                        column: x => x.ReportDefinitionId,
                        principalSchema: "reports",
                        principalTable: "ReportDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportExecutions",
                schema: "reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateVersionId = table.Column<Guid>(type: "uuid", nullable: true),
                    RequestedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    StatusCodeId = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    OutputFileId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OutputContentType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ErrorMessage = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: true),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportExecutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportExecutions_ReportDefinitions_ReportDefinitionId",
                        column: x => x.ReportDefinitionId,
                        principalSchema: "reports",
                        principalTable: "ReportDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportExecutions_ReportExecutionStatuses_StatusCodeId",
                        column: x => x.StatusCodeId,
                        principalSchema: "reports_lookup",
                        principalTable: "ReportExecutionStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportExecutions_ReportTemplateVersions_TemplateVersionId",
                        column: x => x.TemplateVersionId,
                        principalSchema: "reports",
                        principalTable: "ReportTemplateVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                schema: "auth_lookup",
                table: "Permissions",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("81000000-0000-0000-0000-000000000034"), "reports.read", "Auth.Authorization.ReportsRead.Description", null, true, true, "Auth.Authorization.ReportsRead", 250 },
                    { new Guid("81000000-0000-0000-0000-000000000035"), "reports.manage", "Auth.Authorization.ReportsManage.Description", null, true, true, "Auth.Authorization.ReportsManage", 260 },
                    { new Guid("81000000-0000-0000-0000-000000000036"), "reports.generate", "Auth.Authorization.ReportsGenerate.Description", null, true, true, "Auth.Authorization.ReportsGenerate", 270 }
                });

            migrationBuilder.InsertData(
                schema: "reports_lookup",
                table: "ReportCategories",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("91000000-0000-0000-0000-000000000001"), "OPERATIONAL", "Reports.Categories.Operational.Description", null, true, true, "Reports.Categories.Operational", 10 },
                    { new Guid("91000000-0000-0000-0000-000000000002"), "HR", "Reports.Categories.Hr.Description", null, true, true, "Reports.Categories.Hr", 20 },
                    { new Guid("91000000-0000-0000-0000-000000000003"), "DIAGNOSTICS", "Reports.Categories.Diagnostics.Description", null, true, true, "Reports.Categories.Diagnostics", 30 },
                    { new Guid("91000000-0000-0000-0000-000000000004"), "ADMINISTRATION", "Reports.Categories.Administration.Description", null, true, true, "Reports.Categories.Administration", 40 }
                });

            migrationBuilder.InsertData(
                schema: "reports_lookup",
                table: "ReportDataTypies",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("91000000-0000-0000-0000-000000000001"), "STRING", "Reports.DataType.String.Description", null, true, true, "Reports.DataType.String", 10 },
                    { new Guid("91000000-0000-0000-0000-000000000002"), "INTEGER", "Reports.DataType.Integer.Description", null, true, true, "Reports.DataType.Integer", 20 },
                    { new Guid("91000000-0000-0000-0000-000000000003"), "DECIMAL", "Reports.DataType.Decimal.Description", null, true, true, "Reports.DataType.Decimal", 30 },
                    { new Guid("91000000-0000-0000-0000-000000000004"), "BOOLEAN", "Reports.DataType.Boolean.Description", null, true, true, "Reports.DataType.Boolean", 40 },
                    { new Guid("91000000-0000-0000-0000-000000000005"), "DATE", "Reports.DataType.Date.Description", null, true, true, "Reports.DataType.Date", 50 },
                    { new Guid("91000000-0000-0000-0000-000000000006"), "DATETIME", "Reports.DataType.Datetime.Description", null, true, true, "Reports.DataType.Datetime", 60 },
                    { new Guid("91000000-0000-0000-0000-000000000007"), "GUID", "Reports.DataType.Guid.Description", null, true, true, "Reports.DataType.Guid", 70 }
                });

            migrationBuilder.InsertData(
                schema: "reports_lookup",
                table: "ReportExecutionStatuses",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("91000000-0000-0000-0000-000000000001"), "QUEUED", "Reports.ExecutionStatus.Queued.Description", null, true, true, "Reports.ExecutionStatus.Queued", 10 },
                    { new Guid("91000000-0000-0000-0000-000000000002"), "RUNNING", "Reports.ExecutionStatus.Running.Description", null, true, true, "Reports.ExecutionStatus.Running", 20 },
                    { new Guid("91000000-0000-0000-0000-000000000003"), "COMPLETED", "Reports.ExecutionStatus.Completed.Description", null, true, true, "Reports.ExecutionStatus.Completed", 30 },
                    { new Guid("91000000-0000-0000-0000-000000000004"), "FAILED", "Reports.ExecutionStatus.Failed.Description", null, true, true, "Reports.ExecutionStatus.Failed", 40 },
                    { new Guid("91000000-0000-0000-0000-000000000005"), "CANCELLED", "Reports.ExecutionStatus.Cancelled.Description", null, true, true, "Reports.ExecutionStatus.Cancelled", 50 }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("81000000-0000-0000-0000-000000000034"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, false, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000035"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, false, null, null },
                    { new Guid("81000000-0000-0000-0000-000000000036"), new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, false, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportCategories_Code",
                schema: "reports_lookup",
                table: "ReportCategories",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportCategories_FileId",
                schema: "reports_lookup",
                table: "ReportCategories",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCategories_IsActive_SortOrder",
                schema: "reports_lookup",
                table: "ReportCategories",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_ReportDataTypies_Code",
                schema: "reports_lookup",
                table: "ReportDataTypies",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportDataTypies_FileId",
                schema: "reports_lookup",
                table: "ReportDataTypies",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDataTypies_IsActive_SortOrder",
                schema: "reports_lookup",
                table: "ReportDataTypies",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_ReportDefinitions_CategoryId_IsActive",
                schema: "reports",
                table: "ReportDefinitions",
                columns: new[] { "CategoryId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_ReportDefinitions_Code",
                schema: "reports",
                table: "ReportDefinitions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportExecutions_ReportDefinitionId_StartedAt",
                schema: "reports",
                table: "ReportExecutions",
                columns: new[] { "ReportDefinitionId", "StartedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ReportExecutions_StatusCodeId_StartedAt",
                schema: "reports",
                table: "ReportExecutions",
                columns: new[] { "StatusCodeId", "StartedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ReportExecutions_TemplateVersionId",
                schema: "reports",
                table: "ReportExecutions",
                column: "TemplateVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportExecutionStatuses_Code",
                schema: "reports_lookup",
                table: "ReportExecutionStatuses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportExecutionStatuses_FileId",
                schema: "reports_lookup",
                table: "ReportExecutionStatuses",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportExecutionStatuses_IsActive_SortOrder",
                schema: "reports_lookup",
                table: "ReportExecutionStatuses",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_ReportParameters_DataTypeCodeId",
                schema: "reports",
                table: "ReportParameters",
                column: "DataTypeCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportParameters_ReportDefinitionId_Code",
                schema: "reports",
                table: "ReportParameters",
                columns: new[] { "ReportDefinitionId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportParameters_ReportDefinitionId_IsActive_SortOrder",
                schema: "reports",
                table: "ReportParameters",
                columns: new[] { "ReportDefinitionId", "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateVersions_FileId",
                schema: "reports",
                table: "ReportTemplateVersions",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateVersions_FileId1",
                schema: "reports",
                table: "ReportTemplateVersions",
                column: "FileId1");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateVersions_ReportDefinitionId_IsPublished_Publi~",
                schema: "reports",
                table: "ReportTemplateVersions",
                columns: new[] { "ReportDefinitionId", "IsPublished", "PublishedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateVersions_ReportDefinitionId_Version",
                schema: "reports",
                table: "ReportTemplateVersions",
                columns: new[] { "ReportDefinitionId", "Version" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportExecutions",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "ReportParameters",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "ReportExecutionStatuses",
                schema: "reports_lookup");

            migrationBuilder.DropTable(
                name: "ReportTemplateVersions",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "ReportDataTypies",
                schema: "reports_lookup");

            migrationBuilder.DropTable(
                name: "ReportDefinitions",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "ReportCategories",
                schema: "reports_lookup");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("81000000-0000-0000-0000-000000000034"), new Guid("80000000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("81000000-0000-0000-0000-000000000035"), new Guid("80000000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("81000000-0000-0000-0000-000000000036"), new Guid("80000000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                schema: "auth_lookup",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000036"));
        }
    }
}
