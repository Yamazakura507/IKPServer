using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DiagnosticDefinitionReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BucketTypeId",
                schema: "diagnostics",
                table: "ErrorMetricBuckets",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SeverityId",
                schema: "diagnostics",
                table: "ErrorGroups",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                schema: "diagnostics",
                table: "ErrorGroups",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DefinitionId",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000001"),
                column: "DescriptionKey",
                value: "Diagnostic.Severity.Information.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000002"),
                column: "DescriptionKey",
                value: "Diagnostic.Severity.Warning.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000003"),
                column: "DescriptionKey",
                value: "Diagnostic.Severity.Error.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000004"),
                column: "DescriptionKey",
                value: "Diagnostic.Severity.Critical.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000001"),
                column: "DescriptionKey",
                value: "Diagnostic.ErrorGroupStatus.New.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000002"),
                column: "DescriptionKey",
                value: "Diagnostic.ErrorGroupStatus.Investigating.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000003"),
                column: "DescriptionKey",
                value: "Diagnostic.ErrorGroupStatus.Known.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000004"),
                column: "DescriptionKey",
                value: "Diagnostic.ErrorGroupStatus.FixPlanned.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000005"),
                column: "DescriptionKey",
                value: "Diagnostic.ErrorGroupStatus.Fixed.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000006"),
                column: "DescriptionKey",
                value: "Diagnostic.ErrorGroupStatus.Monitoring.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000007"),
                column: "DescriptionKey",
                value: "Diagnostic.ErrorGroupStatus.Resolved.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000008"),
                column: "DescriptionKey",
                value: "Diagnostic.ErrorGroupStatus.Ignored.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000009"),
                column: "DescriptionKey",
                value: "Diagnostic.ErrorGroupStatus.WontFix.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000001"),
                column: "DescriptionKey",
                value: "Diagnostic.CriticalityFactor.OccurrenceRate.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000002"),
                column: "DescriptionKey",
                value: "Diagnostic.CriticalityFactor.AffectedUsers.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000003"),
                column: "DescriptionKey",
                value: "Diagnostic.CriticalityFactor.AffectedUserRatio.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000004"),
                column: "DescriptionKey",
                value: "Diagnostic.CriticalityFactor.FeatureImportance.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000005"),
                column: "DescriptionKey",
                value: "Diagnostic.CriticalityFactor.Trend.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000006"),
                column: "DescriptionKey",
                value: "Diagnostic.CriticalityFactor.FailureRate.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000007"),
                column: "DescriptionKey",
                value: "Diagnostic.CriticalityFactor.Regression.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000008"),
                column: "DescriptionKey",
                value: "Diagnostic.CriticalityFactor.BusinessImpact.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000009"),
                column: "DescriptionKey",
                value: "Diagnostic.CriticalityFactor.SupportImpact.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000010"),
                column: "DescriptionKey",
                value: "Diagnostic.CriticalityFactor.DataLossRisk.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000001"),
                column: "DescriptionKey",
                value: "Diagnostic.MetricBucketType.Minute.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000002"),
                column: "DescriptionKey",
                value: "Diagnostic.MetricBucketType.Hour.Description");

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000003"),
                column: "DescriptionKey",
                value: "Diagnostic.MetricBucketType.Day.Description");

            migrationBuilder.Sql(
                """
                UPDATE diagnostics."ErrorGroups" AS eg
                SET "SeverityId" = dd."Id"
                FROM diagnostics."DiagnosticDefinitions" AS dd
                INNER JOIN diagnostics."DiagnosticDefinitionGroups" AS dg
                    ON dg."Id" = dd."GroupId"
                WHERE dg."Code" = 'DIAGNOSTIC_SEVERITY'
                  AND dd."Code" = eg."Severity";
                """);

            migrationBuilder.Sql(
                """
                UPDATE diagnostics."ErrorGroups" AS eg
                SET "StatusId" = dd."Id"
                FROM diagnostics."DiagnosticDefinitions" AS dd
                INNER JOIN diagnostics."DiagnosticDefinitionGroups" AS dg
                    ON dg."Id" = dd."GroupId"
                WHERE dg."Code" = 'ERROR_GROUP_STATUS'
                  AND dd."Code" = eg."Status";
                """);

            migrationBuilder.Sql(
                """
                UPDATE diagnostics."ErrorCriticalityFactors" AS ecf
                SET "DefinitionId" = dd."Id"
                FROM diagnostics."DiagnosticDefinitions" AS dd
                INNER JOIN diagnostics."DiagnosticDefinitionGroups" AS dg
                    ON dg."Id" = dd."GroupId"
                WHERE dg."Code" = 'CRITICALITY_FACTOR'
                  AND dd."Code" = ecf."Code";
                """);

            migrationBuilder.Sql(
                """
                UPDATE diagnostics."ErrorMetricBuckets" AS emb
                SET "BucketTypeId" = dd."Id"
                FROM diagnostics."DiagnosticDefinitions" AS dd
                INNER JOIN diagnostics."DiagnosticDefinitionGroups" AS dg
                    ON dg."Id" = dd."GroupId"
                WHERE dg."Code" = 'METRIC_BUCKET_TYPE'
                  AND dd."Code" = emb."BucketType";
                """);

            migrationBuilder.Sql(
                """
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1
                        FROM diagnostics."ErrorGroups"
                        WHERE "SeverityId" IS NULL
                    ) THEN
                        RAISE EXCEPTION 'Unable to migrate ErrorGroups.Severity to DiagnosticDefinitions.';
                    END IF;

                    IF EXISTS (
                        SELECT 1
                        FROM diagnostics."ErrorGroups"
                        WHERE "StatusId" IS NULL
                    ) THEN
                        RAISE EXCEPTION 'Unable to migrate ErrorGroups.Status to DiagnosticDefinitions.';
                    END IF;

                    IF EXISTS (
                        SELECT 1
                        FROM diagnostics."ErrorCriticalityFactors"
                        WHERE "DefinitionId" IS NULL
                    ) THEN
                        RAISE EXCEPTION 'Unable to migrate ErrorCriticalityFactors.Code to DiagnosticDefinitions.';
                    END IF;

                    IF EXISTS (
                        SELECT 1
                        FROM diagnostics."ErrorMetricBuckets"
                        WHERE "BucketTypeId" IS NULL
                    ) THEN
                        RAISE EXCEPTION 'Unable to migrate ErrorMetricBuckets.BucketType to DiagnosticDefinitions.';
                    END IF;
                END
                $$;
                """);

            migrationBuilder.AlterColumn<Guid>(
                name: "SeverityId",
                schema: "diagnostics",
                table: "ErrorGroups",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StatusId",
                schema: "diagnostics",
                table: "ErrorGroups",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DefinitionId",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BucketTypeId",
                schema: "diagnostics",
                table: "ErrorMetricBuckets",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.DropIndex(
                name: "IX_ErrorMetricBuckets_BucketType_BucketStart",
                schema: "diagnostics",
                table: "ErrorMetricBuckets");

            migrationBuilder.DropIndex(
                name: "IX_ErrorMetricBuckets_ErrorGroupId_BucketType_BucketStart_Buck~",
                schema: "diagnostics",
                table: "ErrorMetricBuckets");

            migrationBuilder.DropIndex(
                name: "IX_ErrorGroups_Status_Severity",
                schema: "diagnostics",
                table: "ErrorGroups");

            migrationBuilder.DropIndex(
                name: "IX_ErrorCriticalityFactors_Code",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors");

            migrationBuilder.DropIndex(
                name: "IX_ErrorCriticalityFactors_ErrorCriticalityId_Code",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors");

            migrationBuilder.DropColumn(
                name: "BucketType",
                schema: "diagnostics",
                table: "ErrorMetricBuckets");

            migrationBuilder.DropColumn(
                name: "Severity",
                schema: "diagnostics",
                table: "ErrorGroups");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "diagnostics",
                table: "ErrorGroups");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMetricBuckets_BucketTypeId_BucketStart",
                schema: "diagnostics",
                table: "ErrorMetricBuckets",
                columns: new[] { "BucketTypeId", "BucketStart" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMetricBuckets_ErrorGroupId_BucketTypeId_BucketStart_BucketDurationMinutes",
                schema: "diagnostics",
                table: "ErrorMetricBuckets",
                columns: new[] { "ErrorGroupId", "BucketTypeId", "BucketStart", "BucketDurationMinutes" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_SeverityId",
                schema: "diagnostics",
                table: "ErrorGroups",
                column: "SeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_StatusId",
                schema: "diagnostics",
                table: "ErrorGroups",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCriticalityFactors_DefinitionId",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors",
                column: "DefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCriticalityFactors_ErrorCriticalityId_DefinitionId",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors",
                columns: new[] { "ErrorCriticalityId", "DefinitionId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorCriticalityFactors_DiagnosticDefinitions_DefinitionId",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors",
                column: "DefinitionId",
                principalSchema: "diagnostics",
                principalTable: "DiagnosticDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorGroups_DiagnosticDefinitions_SeverityId",
                schema: "diagnostics",
                table: "ErrorGroups",
                column: "SeverityId",
                principalSchema: "diagnostics",
                principalTable: "DiagnosticDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorGroups_DiagnosticDefinitions_StatusId",
                schema: "diagnostics",
                table: "ErrorGroups",
                column: "StatusId",
                principalSchema: "diagnostics",
                principalTable: "DiagnosticDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorMetricBuckets_DiagnosticDefinitions_BucketTypeId",
                schema: "diagnostics",
                table: "ErrorMetricBuckets",
                column: "BucketTypeId",
                principalSchema: "diagnostics",
                principalTable: "DiagnosticDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorCriticalityFactors_DiagnosticDefinitions_DefinitionId",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorGroups_DiagnosticDefinitions_SeverityId",
                schema: "diagnostics",
                table: "ErrorGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorGroups_DiagnosticDefinitions_StatusId",
                schema: "diagnostics",
                table: "ErrorGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorMetricBuckets_DiagnosticDefinitions_BucketTypeId",
                schema: "diagnostics",
                table: "ErrorMetricBuckets");

            migrationBuilder.DropIndex(
                name: "IX_ErrorMetricBuckets_BucketTypeId_BucketStart",
                schema: "diagnostics",
                table: "ErrorMetricBuckets");

            migrationBuilder.DropIndex(
                name: "IX_ErrorMetricBuckets_ErrorGroupId_BucketTypeId_BucketStart_BucketDurationMinutes",
                schema: "diagnostics",
                table: "ErrorMetricBuckets");

            migrationBuilder.DropIndex(
                name: "IX_ErrorGroups_SeverityId",
                schema: "diagnostics",
                table: "ErrorGroups");

            migrationBuilder.DropIndex(
                name: "IX_ErrorGroups_StatusId",
                schema: "diagnostics",
                table: "ErrorGroups");

            migrationBuilder.DropIndex(
                name: "IX_ErrorCriticalityFactors_DefinitionId",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors");

            migrationBuilder.DropIndex(
                name: "IX_ErrorCriticalityFactors_ErrorCriticalityId_DefinitionId",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors");

            migrationBuilder.AddColumn<string>(
                name: "BucketType",
                schema: "diagnostics",
                table: "ErrorMetricBuckets",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Severity",
                schema: "diagnostics",
                table: "ErrorGroups",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "diagnostics",
                table: "ErrorGroups",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.Sql(
                """
                UPDATE diagnostics."ErrorGroups" AS eg
                SET "Severity" = dd."Code"
                FROM diagnostics."DiagnosticDefinitions" AS dd
                INNER JOIN diagnostics."DiagnosticDefinitionGroups" AS dg
                    ON dg."Id" = dd."GroupId"
                WHERE dg."Code" = 'DIAGNOSTIC_SEVERITY'
                  AND dd."Id" = eg."SeverityId";
                """);

            migrationBuilder.Sql(
                """
                UPDATE diagnostics."ErrorGroups" AS eg
                SET "Status" = dd."Code"
                FROM diagnostics."DiagnosticDefinitions" AS dd
                INNER JOIN diagnostics."DiagnosticDefinitionGroups" AS dg
                    ON dg."Id" = dd."GroupId"
                WHERE dg."Code" = 'ERROR_GROUP_STATUS'
                  AND dd."Id" = eg."StatusId";
                """);

            migrationBuilder.Sql(
                """
                UPDATE diagnostics."ErrorCriticalityFactors" AS ecf
                SET "Code" = dd."Code"
                FROM diagnostics."DiagnosticDefinitions" AS dd
                INNER JOIN diagnostics."DiagnosticDefinitionGroups" AS dg
                    ON dg."Id" = dd."GroupId"
                WHERE dg."Code" = 'CRITICALITY_FACTOR'
                  AND dd."Id" = ecf."DefinitionId";
                """);

            migrationBuilder.Sql(
                """
                UPDATE diagnostics."ErrorMetricBuckets" AS emb
                SET "BucketType" = dd."Code"
                FROM diagnostics."DiagnosticDefinitions" AS dd
                INNER JOIN diagnostics."DiagnosticDefinitionGroups" AS dg
                    ON dg."Id" = dd."GroupId"
                WHERE dg."Code" = 'METRIC_BUCKET_TYPE'
                  AND dd."Id" = emb."BucketTypeId";
                """);

            migrationBuilder.AlterColumn<string>(
                name: "BucketType",
                schema: "diagnostics",
                table: "ErrorMetricBuckets",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Severity",
                schema: "diagnostics",
                table: "ErrorGroups",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "diagnostics",
                table: "ErrorGroups",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "BucketTypeId",
                schema: "diagnostics",
                table: "ErrorMetricBuckets");

            migrationBuilder.DropColumn(
                name: "SeverityId",
                schema: "diagnostics",
                table: "ErrorGroups");

            migrationBuilder.DropColumn(
                name: "StatusId",
                schema: "diagnostics",
                table: "ErrorGroups");

            migrationBuilder.DropColumn(
                name: "DefinitionId",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMetricBuckets_BucketType_BucketStart",
                schema: "diagnostics",
                table: "ErrorMetricBuckets",
                columns: new[] { "BucketType", "BucketStart" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMetricBuckets_ErrorGroupId_BucketType_BucketStart_BucketDurationMinutes",
                schema: "diagnostics",
                table: "ErrorMetricBuckets",
                columns: new[] { "ErrorGroupId", "BucketType", "BucketStart", "BucketDurationMinutes" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_Status_Severity",
                schema: "diagnostics",
                table: "ErrorGroups",
                columns: new[] { "Status", "Severity" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCriticalityFactors_Code",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCriticalityFactors_ErrorCriticalityId_Code",
                schema: "diagnostics",
                table: "ErrorCriticalityFactors",
                columns: new[] { "ErrorCriticalityId", "Code" },
                unique: true);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000001"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000002"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000003"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000004"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000001"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000002"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000003"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000004"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000005"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000006"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000007"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000008"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000009"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000001"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000002"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000003"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000004"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000005"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000006"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000007"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000008"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000009"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000010"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000001"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000002"),
                column: "DescriptionKey",
                value: null);

            migrationBuilder.UpdateData(
                schema: "diagnostics",
                table: "DiagnosticDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000003"),
                column: "DescriptionKey",
                value: null);
        }
    }
}
