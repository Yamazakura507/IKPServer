using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DiagnosticsInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "diagnostics");

            migrationBuilder.CreateTable(
                name: "DiagnosticAreas",
                schema: "diagnostics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameKey = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DescriptionKey = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ImportanceScore = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorGroups",
                schema: "diagnostics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Fingerprint = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    FingerprintVersion = table.Column<int>(type: "integer", nullable: false),
                    ApplicationId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DiagnosticAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExceptionType = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ErrorCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Summary = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    Severity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstOccurredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastOccurredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FirstSeenVersion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastSeenVersion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorGroups_DiagnosticAreas_DiagnosticAreaId",
                        column: x => x.DiagnosticAreaId,
                        principalSchema: "diagnostics",
                        principalTable: "DiagnosticAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ErrorCriticalities",
                schema: "diagnostics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ErrorGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    AutomaticScore = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    ManualScore = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    EffectiveScore = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    CalculationVersion = table.Column<int>(type: "integer", nullable: false),
                    CalculatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OverrideUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    OverrideAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OverrideReason = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorCriticalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorCriticalities_ErrorGroups_ErrorGroupId",
                        column: x => x.ErrorGroupId,
                        principalSchema: "diagnostics",
                        principalTable: "ErrorGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorMetricBuckets",
                schema: "diagnostics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ErrorGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    BucketStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BucketDurationMinutes = table.Column<int>(type: "integer", nullable: false),
                    BucketType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Occurrences = table.Column<long>(type: "bigint", nullable: false),
                    UniqueUsers = table.Column<long>(type: "bigint", nullable: false),
                    FirstOccurrenceAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastOccurrenceAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CalculatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorMetricBuckets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorMetricBuckets_ErrorGroups_ErrorGroupId",
                        column: x => x.ErrorGroupId,
                        principalSchema: "diagnostics",
                        principalTable: "ErrorGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorOccurrences",
                schema: "diagnostics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ErrorGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    SessionId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ApplicationId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ApplicationVersion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Platform = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MachineId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ClientInstanceId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    RequestId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CorrelationId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Endpoint = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    HttpMethod = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Route = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ExceptionType = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ExceptionMessage = table.Column<string>(type: "text", nullable: true),
                    StackTrace = table.Column<string>(type: "text", nullable: true),
                    SourceFile = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SourceMethod = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SourceLineNumber = table.Column<int>(type: "integer", nullable: true),
                    ContextSnapshot = table.Column<JsonDocument>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorOccurrences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorOccurrences_ErrorGroups_ErrorGroupId",
                        column: x => x.ErrorGroupId,
                        principalSchema: "diagnostics",
                        principalTable: "ErrorGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorStatistics",
                schema: "diagnostics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ErrorGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalOccurrences = table.Column<long>(type: "bigint", nullable: false),
                    UniqueUsers = table.Column<long>(type: "bigint", nullable: false),
                    AffectedUserRatio = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    OccurrencesLastHour = table.Column<long>(type: "bigint", nullable: false),
                    OccurrencesLastDay = table.Column<long>(type: "bigint", nullable: false),
                    OccurrencesLast7Days = table.Column<long>(type: "bigint", nullable: false),
                    UsersLastHour = table.Column<long>(type: "bigint", nullable: false),
                    UsersLastDay = table.Column<long>(type: "bigint", nullable: false),
                    UsersLast7Days = table.Column<long>(type: "bigint", nullable: false),
                    GrowthRate = table.Column<decimal>(type: "numeric(9,2)", precision: 9, scale: 2, nullable: false),
                    CalculatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorStatistics_ErrorGroups_ErrorGroupId",
                        column: x => x.ErrorGroupId,
                        principalSchema: "diagnostics",
                        principalTable: "ErrorGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorCriticalityFactors",
                schema: "diagnostics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ErrorCriticalityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Score = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    Weight = table.Column<decimal>(type: "numeric(5,4)", precision: 5, scale: 4, nullable: false),
                    Contribution = table.Column<decimal>(type: "numeric(7,4)", precision: 7, scale: 4, nullable: false),
                    Value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    CalculatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorCriticalityFactors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorCriticalityFactors_ErrorCriticalities_ErrorCriticality~",
                        column: x => x.ErrorCriticalityId,
                        principalSchema: "diagnostics",
                        principalTable: "ErrorCriticalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreas_Code",
                schema: "diagnostics",
                table: "DiagnosticAreas",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreas_IsActive_ImportanceScore",
                schema: "diagnostics",
                table: "DiagnosticAreas",
                columns: new[] { "IsActive", "ImportanceScore" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCriticalities_EffectiveScore",
                schema: "diagnostics",
                table: "ErrorCriticalities",
                column: "EffectiveScore");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCriticalities_EffectiveScore_CalculatedAt",
                schema: "diagnostics",
                table: "ErrorCriticalities",
                columns: new[] { "EffectiveScore", "CalculatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCriticalities_ErrorGroupId",
                schema: "diagnostics",
                table: "ErrorCriticalities",
                column: "ErrorGroupId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_ApplicationId",
                schema: "diagnostics",
                table: "ErrorGroups",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_DiagnosticAreaId",
                schema: "diagnostics",
                table: "ErrorGroups",
                column: "DiagnosticAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_Fingerprint_FingerprintVersion",
                schema: "diagnostics",
                table: "ErrorGroups",
                columns: new[] { "Fingerprint", "FingerprintVersion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_LastOccurredAt",
                schema: "diagnostics",
                table: "ErrorGroups",
                column: "LastOccurredAt");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_Status_Severity",
                schema: "diagnostics",
                table: "ErrorGroups",
                columns: new[] { "Status", "Severity" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMetricBuckets_BucketType_BucketStart",
                schema: "diagnostics",
                table: "ErrorMetricBuckets",
                columns: new[] { "BucketType", "BucketStart" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMetricBuckets_CalculatedAt",
                schema: "diagnostics",
                table: "ErrorMetricBuckets",
                column: "CalculatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMetricBuckets_ErrorGroupId_BucketStart",
                schema: "diagnostics",
                table: "ErrorMetricBuckets",
                columns: new[] { "ErrorGroupId", "BucketStart" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMetricBuckets_ErrorGroupId_BucketType_BucketStart_Buck~",
                schema: "diagnostics",
                table: "ErrorMetricBuckets",
                columns: new[] { "ErrorGroupId", "BucketType", "BucketStart", "BucketDurationMinutes" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_ApplicationVersion",
                schema: "diagnostics",
                table: "ErrorOccurrences",
                column: "ApplicationVersion");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_CorrelationId",
                schema: "diagnostics",
                table: "ErrorOccurrences",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_ErrorGroupId_OccurredAt",
                schema: "diagnostics",
                table: "ErrorOccurrences",
                columns: new[] { "ErrorGroupId", "OccurredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_OccurredAt",
                schema: "diagnostics",
                table: "ErrorOccurrences",
                column: "OccurredAt");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_Platform",
                schema: "diagnostics",
                table: "ErrorOccurrences",
                column: "Platform");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_RequestId",
                schema: "diagnostics",
                table: "ErrorOccurrences",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_UserId",
                schema: "diagnostics",
                table: "ErrorOccurrences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorStatistics_CalculatedAt",
                schema: "diagnostics",
                table: "ErrorStatistics",
                column: "CalculatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorStatistics_ErrorGroupId",
                schema: "diagnostics",
                table: "ErrorStatistics",
                column: "ErrorGroupId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorCriticalityFactors",
                schema: "diagnostics");

            migrationBuilder.DropTable(
                name: "ErrorMetricBuckets",
                schema: "diagnostics");

            migrationBuilder.DropTable(
                name: "ErrorOccurrences",
                schema: "diagnostics");

            migrationBuilder.DropTable(
                name: "ErrorStatistics",
                schema: "diagnostics");

            migrationBuilder.DropTable(
                name: "ErrorCriticalities",
                schema: "diagnostics");

            migrationBuilder.DropTable(
                name: "ErrorGroups",
                schema: "diagnostics");

            migrationBuilder.DropTable(
                name: "DiagnosticAreas",
                schema: "diagnostics");
        }
    }
}
