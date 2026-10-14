using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app_logs_lookup");

            migrationBuilder.EnsureSchema(
                name: "app_logs");

            migrationBuilder.EnsureSchema(
                name: "diagnostics_core");

            migrationBuilder.EnsureSchema(
                name: "diagnostics_lookup");

            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.EnsureSchema(
                name: "diagnostics_statistics");

            migrationBuilder.CreateTable(
                name: "AuditActions",
                schema: "app_logs_lookup",
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
                    table.PrimaryKey("PK_AuditActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticAreaResolutionRules",
                schema: "diagnostics_lookup",
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
                    table.PrimaryKey("PK_DiagnosticAreaResolutionRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticAreaResolutionSources",
                schema: "diagnostics_lookup",
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
                    table.PrimaryKey("PK_DiagnosticAreaResolutionSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticAreas",
                schema: "diagnostics_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImportanceScore = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    NameKey = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    DescriptionKey = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticAreas", x => x.Id);
                    table.CheckConstraint("CK_DiagnosticAreas_ImportanceScore", "\"ImportanceScore\" >= 0 AND \"ImportanceScore\" <= 100");
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticDefinitionGroups",
                schema: "diagnostics_lookup",
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
                name: "DiagnosticStatisticsWorkItemStatuses",
                schema: "diagnostics_lookup",
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
                    table.PrimaryKey("PK_DiagnosticStatisticsWorkItemStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FamilyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "app_logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntitySchema = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntityType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_AuditActions_ActionId",
                        column: x => x.ActionId,
                        principalSchema: "app_logs_lookup",
                        principalTable: "AuditActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticAreaResolutionMappings",
                schema: "diagnostics_lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiagnosticAreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    RuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Pattern = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    Confidence = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticAreaResolutionMappings", x => x.Id);
                    table.CheckConstraint("CK_DiagnosticAreaResolutionMappings_Confidence", "\"Confidence\" >= 0 AND \"Confidence\" <= 100");
                    table.CheckConstraint("CK_DiagnosticAreaResolutionMappings_Priority", "\"Priority\" >= 0 AND \"Priority\" <= 100");
                    table.ForeignKey(
                        name: "FK_DiagnosticAreaResolutionMappings_DiagnosticAreaResolutionRu~",
                        column: x => x.RuleId,
                        principalSchema: "diagnostics_lookup",
                        principalTable: "DiagnosticAreaResolutionRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiagnosticAreaResolutionMappings_DiagnosticAreas_Diagnostic~",
                        column: x => x.DiagnosticAreaId,
                        principalSchema: "diagnostics_lookup",
                        principalTable: "DiagnosticAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticAreaResolutions",
                schema: "diagnostics_core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiagnosticAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    SourceId = table.Column<Guid>(type: "uuid", nullable: true),
                    RuleId = table.Column<Guid>(type: "uuid", nullable: true),
                    Confidence = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticAreaResolutions", x => x.Id);
                    table.CheckConstraint("CK_DiagnosticAreaResolutions_Confidence", "\"Confidence\" >= 0 AND \"Confidence\" <= 100");
                    table.CheckConstraint("CK_DiagnosticAreaResolutions_Version", "\"Version\" > 0");
                    table.ForeignKey(
                        name: "FK_DiagnosticAreaResolutions_DiagnosticAreaResolutionRules_Rul~",
                        column: x => x.RuleId,
                        principalSchema: "diagnostics_lookup",
                        principalTable: "DiagnosticAreaResolutionRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiagnosticAreaResolutions_DiagnosticAreaResolutionSources_S~",
                        column: x => x.SourceId,
                        principalSchema: "diagnostics_lookup",
                        principalTable: "DiagnosticAreaResolutionSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiagnosticAreaResolutions_DiagnosticAreas_DiagnosticAreaId",
                        column: x => x.DiagnosticAreaId,
                        principalSchema: "diagnostics_lookup",
                        principalTable: "DiagnosticAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticDefinitions",
                schema: "diagnostics_lookup",
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
                        principalSchema: "diagnostics_lookup",
                        principalTable: "DiagnosticDefinitionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogChanges",
                schema: "app_logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PropertyType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OldValue = table.Column<string>(type: "jsonb", nullable: true),
                    NewValue = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogChanges_AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalSchema: "app_logs",
                        principalTable: "AuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticAreaResolutionEvidences",
                schema: "diagnostics_core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiagnosticAreaResolutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    MappingId = table.Column<Guid>(type: "uuid", nullable: true),
                    RuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Pattern = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    MatchedValue = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    Confidence = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    Contribution = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticAreaResolutionEvidences", x => x.Id);
                    table.CheckConstraint("CK_DiagnosticAreaResolutionEvidences_Confidence", "\"Confidence\" >= 0 AND \"Confidence\" <= 100");
                    table.CheckConstraint("CK_DiagnosticAreaResolutionEvidences_Contribution", "\"Contribution\" >= 0 AND \"Contribution\" <= 100");
                    table.CheckConstraint("CK_DiagnosticAreaResolutionEvidences_Priority", "\"Priority\" >= 0 AND \"Priority\" <= 100");
                    table.ForeignKey(
                        name: "FK_DiagnosticAreaResolutionEvidences_DiagnosticAreaResolutionM~",
                        column: x => x.MappingId,
                        principalSchema: "diagnostics_lookup",
                        principalTable: "DiagnosticAreaResolutionMappings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DiagnosticAreaResolutionEvidences_DiagnosticAreaResolutionR~",
                        column: x => x.RuleId,
                        principalSchema: "diagnostics_lookup",
                        principalTable: "DiagnosticAreaResolutionRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiagnosticAreaResolutionEvidences_DiagnosticAreaResolutions~",
                        column: x => x.DiagnosticAreaResolutionId,
                        principalSchema: "diagnostics_core",
                        principalTable: "DiagnosticAreaResolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorGroups",
                schema: "diagnostics_core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Fingerprint = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    FingerprintVersion = table.Column<int>(type: "integer", nullable: false),
                    ApplicationId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DiagnosticAreaResolutionId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExceptionType = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ErrorCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Summary = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    SeverityId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false),
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
                        name: "FK_ErrorGroups_DiagnosticAreaResolutions_DiagnosticAreaResolut~",
                        column: x => x.DiagnosticAreaResolutionId,
                        principalSchema: "diagnostics_core",
                        principalTable: "DiagnosticAreaResolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ErrorGroups_DiagnosticDefinitions_SeverityId",
                        column: x => x.SeverityId,
                        principalSchema: "diagnostics_lookup",
                        principalTable: "DiagnosticDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ErrorGroups_DiagnosticDefinitions_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "diagnostics_lookup",
                        principalTable: "DiagnosticDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticStatisticsWorkItems",
                schema: "diagnostics_core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ErrorGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AvailableAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastError = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticStatisticsWorkItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiagnosticStatisticsWorkItems_DiagnosticStatisticsWorkItemS~",
                        column: x => x.StatusId,
                        principalSchema: "diagnostics_lookup",
                        principalTable: "DiagnosticStatisticsWorkItemStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiagnosticStatisticsWorkItems_ErrorGroups_ErrorGroupId",
                        column: x => x.ErrorGroupId,
                        principalSchema: "diagnostics_core",
                        principalTable: "ErrorGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorCriticalities",
                schema: "diagnostics_core",
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
                        principalSchema: "diagnostics_core",
                        principalTable: "ErrorGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorMetricBuckets",
                schema: "diagnostics_statistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ErrorGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    BucketStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BucketDurationMinutes = table.Column<int>(type: "integer", nullable: false),
                    BucketTypeId = table.Column<Guid>(type: "uuid", nullable: false),
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
                        name: "FK_ErrorMetricBuckets_DiagnosticDefinitions_BucketTypeId",
                        column: x => x.BucketTypeId,
                        principalSchema: "diagnostics_lookup",
                        principalTable: "DiagnosticDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ErrorMetricBuckets_ErrorGroups_ErrorGroupId",
                        column: x => x.ErrorGroupId,
                        principalSchema: "diagnostics_core",
                        principalTable: "ErrorGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorOccurrences",
                schema: "diagnostics_core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ErrorGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiagnosticAreaResolutionId = table.Column<Guid>(type: "uuid", nullable: true),
                    OccurredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContextSnapshot = table.Column<JsonDocument>(type: "jsonb", nullable: true),
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
                    SourceLineNumber = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorOccurrences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorOccurrences_DiagnosticAreaResolutions_DiagnosticAreaRe~",
                        column: x => x.DiagnosticAreaResolutionId,
                        principalSchema: "diagnostics_core",
                        principalTable: "DiagnosticAreaResolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ErrorOccurrences_ErrorGroups_ErrorGroupId",
                        column: x => x.ErrorGroupId,
                        principalSchema: "diagnostics_core",
                        principalTable: "ErrorGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorStatistics",
                schema: "diagnostics_statistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ErrorGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalOccurrences = table.Column<long>(type: "bigint", nullable: false),
                    UniqueUsers = table.Column<long>(type: "bigint", nullable: false),
                    AffectedUserRatio = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    OccurrencesLastHour = table.Column<long>(type: "bigint", nullable: false),
                    OccurrencesLastDay = table.Column<long>(type: "bigint", nullable: false),
                    OccurrencesLastWeek = table.Column<long>(type: "bigint", nullable: false),
                    UsersLastHour = table.Column<long>(type: "bigint", nullable: false),
                    UsersLastDay = table.Column<long>(type: "bigint", nullable: false),
                    UsersLastWeek = table.Column<long>(type: "bigint", nullable: false),
                    GrowthRate = table.Column<decimal>(type: "numeric(9,2)", precision: 9, scale: 2, nullable: false),
                    CalculatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorStatistics_ErrorGroups_ErrorGroupId",
                        column: x => x.ErrorGroupId,
                        principalSchema: "diagnostics_core",
                        principalTable: "ErrorGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorCriticalityFactors",
                schema: "diagnostics_core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ErrorCriticalityId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
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
                        name: "FK_ErrorCriticalityFactors_DiagnosticDefinitions_DefinitionId",
                        column: x => x.DefinitionId,
                        principalSchema: "diagnostics_lookup",
                        principalTable: "DiagnosticDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ErrorCriticalityFactors_ErrorCriticalities_ErrorCriticality~",
                        column: x => x.ErrorCriticalityId,
                        principalSchema: "diagnostics_core",
                        principalTable: "ErrorCriticalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "app_logs_lookup",
                table: "AuditActions",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "CREATE", "Audit.Action.Create.Description", null, true, true, "Audit.Action.Create", 10 },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "UPDATE", "Audit.Action.Update.Description", null, true, true, "Audit.Action.Update", 20 },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "DELETE", "Audit.Action.Delete.Description", null, true, true, "Audit.Action.Delete", 30 },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "RESTORE", "Audit.Action.Restore.Description", null, true, true, "Audit.Action.Restore", 40 }
                });

            migrationBuilder.InsertData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), "EXPLICIT_CONTEXT", "Diagnostics.AreaResolutionRule.ExplicitContext.Description", null, true, true, "Diagnostics.AreaResolutionRule.ExplicitContext", 10 },
                    { new Guid("50000000-0000-0000-0000-000000000002"), "EXCEPTION_TYPE", "Diagnostics.AreaResolutionRule.ExceptionType.Description", null, true, true, "Diagnostics.AreaResolutionRule.ExceptionType", 20 },
                    { new Guid("50000000-0000-0000-0000-000000000003"), "NAMESPACE", "Diagnostics.AreaResolutionRule.Namespace.Description", null, true, true, "Diagnostics.AreaResolutionRule.Namespace", 30 },
                    { new Guid("50000000-0000-0000-0000-000000000004"), "ENDPOINT", "Diagnostics.AreaResolutionRule.Endpoint.Description", null, true, true, "Diagnostics.AreaResolutionRule.Endpoint", 40 },
                    { new Guid("50000000-0000-0000-0000-000000000005"), "HTTP_PIPELINE", "Diagnostics.AreaResolutionRule.HttpPipeline.Description", null, true, true, "Diagnostics.AreaResolutionRule.HttpPipeline", 50 }
                });

            migrationBuilder.InsertData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionSources",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), "EXPLICIT", "Diagnostics.AreaResolutionSource.Explicit.Description", null, true, true, "Diagnostics.AreaResolutionSource.Explicit", 10 },
                    { new Guid("40000000-0000-0000-0000-000000000002"), "AUTOMATIC", "Diagnostics.AreaResolutionSource.Automatic.Description", null, true, true, "Diagnostics.AreaResolutionSource.Automatic", 20 }
                });

            migrationBuilder.InsertData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "ImportanceScore", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), "API", "Diagnostics.Area.Api.Description", null, 90, true, true, "Diagnostics.Area.Api", 10 },
                    { new Guid("30000000-0000-0000-0000-000000000002"), "DIAGNOSTICS", "Diagnostics.Area.Diagnostics.Description", null, 80, true, true, "Diagnostics.Area.Diagnostics", 20 },
                    { new Guid("30000000-0000-0000-0000-000000000003"), "ENCRYPTION", "Diagnostics.Area.Encryption.Description", null, 95, true, true, "Diagnostics.Area.Encryption", 30 },
                    { new Guid("30000000-0000-0000-0000-000000000004"), "AUTHENTICATION", "Diagnostics.Area.Authentication.Description", null, 100, true, true, "Diagnostics.Area.Authentication", 40 },
                    { new Guid("30000000-0000-0000-0000-000000000005"), "AUTHORIZATION", "Diagnostics.Area.Authorization.Description", null, 100, true, true, "Diagnostics.Area.Authorization", 50 },
                    { new Guid("30000000-0000-0000-0000-000000000006"), "REGISTRATION", "Diagnostics.Area.Registration.Description", null, 90, true, true, "Diagnostics.Area.Registration", 60 },
                    { new Guid("30000000-0000-0000-0000-000000000007"), "SUPPORT", "Diagnostics.Area.Support.Description", null, 70, true, true, "Diagnostics.Area.Support", 70 }
                });

            migrationBuilder.InsertData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), "SEVERITY", "Diagnostic.DefinitionGroup.Severity.Description", null, true, true, "Diagnostic.DefinitionGroup.Severity", 10 },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "ERROR_GROUP_STATUS", "Diagnostic.DefinitionGroup.ErrorGroupStatus.Description", null, true, true, "Diagnostic.DefinitionGroup.ErrorGroupStatus", 20 },
                    { new Guid("20000000-0000-0000-0000-000000000003"), "CRITICALITY_FACTOR", "Diagnostic.DefinitionGroup.CriticalityFactor.Description", null, true, true, "Diagnostic.DefinitionGroup.CriticalityFactor", 30 },
                    { new Guid("20000000-0000-0000-0000-000000000004"), "METRIC_BUCKET_TYPE", "Diagnostic.DefinitionGroup.MetricBucketType.Description", null, true, true, "Diagnostic.DefinitionGroup.MetricBucketType", 40 }
                });

            migrationBuilder.InsertData(
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("60000000-0000-0000-0000-000000000001"), "PENDING", "Diagnostics.StatisticsWorkItemStatus.Pending.Description", null, true, true, "Diagnostics.StatisticsWorkItemStatus.Pending", 10 },
                    { new Guid("60000000-0000-0000-0000-000000000002"), "PROCESSING", "Diagnostics.StatisticsWorkItemStatus.Processing.Description", null, true, true, "Diagnostics.StatisticsWorkItemStatus.Processing", 20 },
                    { new Guid("60000000-0000-0000-0000-000000000003"), "COMPLETED", "Diagnostics.StatisticsWorkItemStatus.Completed.Description", null, true, true, "Diagnostics.StatisticsWorkItemStatus.Completed", 30 },
                    { new Guid("60000000-0000-0000-0000-000000000004"), "FAILED", "Diagnostics.StatisticsWorkItemStatus.Failed.Description", null, true, true, "Diagnostics.StatisticsWorkItemStatus.Failed", 40 }
                });

            migrationBuilder.InsertData(
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionMappings",
                columns: new[] { "Id", "Confidence", "DiagnosticAreaId", "IsActive", "IsSystem", "Pattern", "Priority", "RuleId" },
                values: new object[,]
                {
                    { new Guid("60000000-0000-0000-0000-000000000001"), 90m, new Guid("30000000-0000-0000-0000-000000000001"), true, true, "^IKP\\.Api(?:\\.|$)", 100, new Guid("50000000-0000-0000-0000-000000000003") },
                    { new Guid("60000000-0000-0000-0000-000000000002"), 95m, new Guid("30000000-0000-0000-0000-000000000001"), true, true, "^/api(?:/|$)", 100, new Guid("50000000-0000-0000-0000-000000000004") },
                    { new Guid("60000000-0000-0000-0000-000000000003"), 85m, new Guid("30000000-0000-0000-0000-000000000001"), true, true, "^(GET|POST|PUT|PATCH|DELETE|OPTIONS|HEAD)\\s+/api(?:/|$)", 80,
    new Guid("50000000-0000-0000-0000-000000000005") },
                    { new Guid("60000000-0000-0000-0000-000000000004"), 95m, new Guid("30000000-0000-0000-0000-000000000002"), true, true, "^IKP\\.Application\\.Diagnostics(?:\\.|$)", 100, new Guid("50000000-0000-0000-0000-000000000003") },
                    { new Guid("60000000-0000-0000-0000-000000000005"), 95m, new Guid("30000000-0000-0000-0000-000000000002"), true, true, "^/api/diagnostics(?:/|$)", 100, new Guid("50000000-0000-0000-0000-000000000004") }
                });

            migrationBuilder.InsertData(
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                columns: new[] { "Id", "Code", "DescriptionKey", "FileId", "GroupId", "IsActive", "IsSystem", "NameKey", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("21000000-0000-0000-0000-000000000001"), "INFORMATION", "Diagnostic.Severity.Information.Description", null, new Guid("20000000-0000-0000-0000-000000000001"), true, true, "Diagnostic.Severity.Information", 10 },
                    { new Guid("21000000-0000-0000-0000-000000000002"), "WARNING", "Diagnostic.Severity.Warning.Description", null, new Guid("20000000-0000-0000-0000-000000000001"), true, true, "Diagnostic.Severity.Warning", 20 },
                    { new Guid("21000000-0000-0000-0000-000000000003"), "ERROR", "Diagnostic.Severity.Error.Description", null, new Guid("20000000-0000-0000-0000-000000000001"), true, true, "Diagnostic.Severity.Error", 30 },
                    { new Guid("21000000-0000-0000-0000-000000000004"), "CRITICAL", "Diagnostic.Severity.Critical.Description", null, new Guid("20000000-0000-0000-0000-000000000001"), true, true, "Diagnostic.Severity.Critical", 40 },
                    { new Guid("22000000-0000-0000-0000-000000000001"), "NEW", "Diagnostic.ErrorGroupStatus.New.Description", null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.New", 10 },
                    { new Guid("22000000-0000-0000-0000-000000000002"), "INVESTIGATING", "Diagnostic.ErrorGroupStatus.Investigating.Description", null, new Guid("20000000-0000-0000-0000-000000000002"), true, true,
    "Diagnostic.ErrorGroupStatus.Investigating", 20 },
                    { new Guid("22000000-0000-0000-0000-000000000003"), "KNOWN", "Diagnostic.ErrorGroupStatus.Known.Description", null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.Known", 30 },
                    { new Guid("22000000-0000-0000-0000-000000000004"), "FIX_PLANNED", "Diagnostic.ErrorGroupStatus.FixPlanned.Description", null, new Guid("20000000-0000-0000-0000-000000000002"), true, true,
    "Diagnostic.ErrorGroupStatus.FixPlanned", 40 },
                    { new Guid("22000000-0000-0000-0000-000000000005"), "FIXED", "Diagnostic.ErrorGroupStatus.Fixed.Description", null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.Fixed", 50 },
                    { new Guid("22000000-0000-0000-0000-000000000006"), "MONITORING", "Diagnostic.ErrorGroupStatus.Monitoring.Description", null, new Guid("20000000-0000-0000-0000-000000000002"), true, true,
    "Diagnostic.ErrorGroupStatus.Monitoring", 60 },
                    { new Guid("22000000-0000-0000-0000-000000000007"), "RESOLVED", "Diagnostic.ErrorGroupStatus.Resolved.Description", null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.Resolved", 70 },
                    { new Guid("22000000-0000-0000-0000-000000000008"), "IGNORED", "Diagnostic.ErrorGroupStatus.Ignored.Description", null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.Ignored", 80 },
                    { new Guid("22000000-0000-0000-0000-000000000009"), "WONT_FIX", "Diagnostic.ErrorGroupStatus.WontFix.Description", null, new Guid("20000000-0000-0000-0000-000000000002"), true, true, "Diagnostic.ErrorGroupStatus.WontFix", 90 },
                    { new Guid("23000000-0000-0000-0000-000000000001"), "OCCURRENCE_RATE", "Diagnostic.CriticalityFactor.OccurrenceRate.Description", null, new Guid("20000000-0000-0000-0000-000000000003"), true, true,
    "Diagnostic.CriticalityFactor.OccurrenceRate", 10 },
                    { new Guid("23000000-0000-0000-0000-000000000002"), "AFFECTED_USERS", "Diagnostic.CriticalityFactor.AffectedUsers.Description", null, new Guid("20000000-0000-0000-0000-000000000003"), true, true,
    "Diagnostic.CriticalityFactor.AffectedUsers", 20 },
                    { new Guid("23000000-0000-0000-0000-000000000003"), "AFFECTED_USER_RATIO", "Diagnostic.CriticalityFactor.AffectedUserRatio.Description", null, new Guid("20000000-0000-0000-0000-000000000003"), true, true,
    "Diagnostic.CriticalityFactor.AffectedUserRatio", 30 },
                    { new Guid("23000000-0000-0000-0000-000000000004"), "FEATURE_IMPORTANCE", "Diagnostic.CriticalityFactor.FeatureImportance.Description", null, new Guid("20000000-0000-0000-0000-000000000003"), true, true,
    "Diagnostic.CriticalityFactor.FeatureImportance", 40 },
                    { new Guid("23000000-0000-0000-0000-000000000005"), "TREND", "Diagnostic.CriticalityFactor.Trend.Description", null, new Guid("20000000-0000-0000-0000-000000000003"), true, true, "Diagnostic.CriticalityFactor.Trend", 50 },
                    { new Guid("23000000-0000-0000-0000-000000000006"), "FAILURE_RATE", "Diagnostic.CriticalityFactor.FailureRate.Description", null, new Guid("20000000-0000-0000-0000-000000000003"), true, true,
    "Diagnostic.CriticalityFactor.FailureRate", 60 },
                    { new Guid("23000000-0000-0000-0000-000000000007"), "REGRESSION", "Diagnostic.CriticalityFactor.Regression.Description", null, new Guid("20000000-0000-0000-0000-000000000003"), true, true,
    "Diagnostic.CriticalityFactor.Regression", 70 },
                    { new Guid("23000000-0000-0000-0000-000000000008"), "BUSINESS_IMPACT", "Diagnostic.CriticalityFactor.BusinessImpact.Description", null, new Guid("20000000-0000-0000-0000-000000000003"), true, true,
    "Diagnostic.CriticalityFactor.BusinessImpact", 80 },
                    { new Guid("23000000-0000-0000-0000-000000000009"), "SUPPORT_IMPACT", "Diagnostic.CriticalityFactor.SupportImpact.Description", null, new Guid("20000000-0000-0000-0000-000000000003"), true, true,
    "Diagnostic.CriticalityFactor.SupportImpact", 90 },
                    { new Guid("23000000-0000-0000-0000-000000000010"), "DATA_LOSS_RISK", "Diagnostic.CriticalityFactor.DataLossRisk.Description", null, new Guid("20000000-0000-0000-0000-000000000003"), true, true,
    "Diagnostic.CriticalityFactor.DataLossRisk", 100 },
                    { new Guid("24000000-0000-0000-0000-000000000001"), "MINUTE", "Diagnostic.MetricBucketType.Minute.Description", null, new Guid("20000000-0000-0000-0000-000000000004"), true, true, "Diagnostic.MetricBucketType.Minute", 10 },
                    { new Guid("24000000-0000-0000-0000-000000000002"), "HOUR", "Diagnostic.MetricBucketType.Hour.Description", null, new Guid("20000000-0000-0000-0000-000000000004"), true, true, "Diagnostic.MetricBucketType.Hour", 20 },
                    { new Guid("24000000-0000-0000-0000-000000000003"), "DAY", "Diagnostic.MetricBucketType.Day.Description", null, new Guid("20000000-0000-0000-0000-000000000004"), true, true, "Diagnostic.MetricBucketType.Day", 30 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditActions_Code",
                schema: "app_logs_lookup",
                table: "AuditActions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditActions_IsActive_SortOrder",
                schema: "app_logs_lookup",
                table: "AuditActions",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogChanges_AuditLogId",
                schema: "app_logs",
                table: "AuditLogChanges",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ActionId",
                schema: "app_logs",
                table: "AuditLogs",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_CreatedAt",
                schema: "app_logs",
                table: "AuditLogs",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntitySchema_EntityType_EntityId",
                schema: "app_logs",
                table: "AuditLogs",
                columns: new[] { "EntitySchema", "EntityType", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionEvidences_DiagnosticAreaResolutionId",
                schema: "diagnostics_core",
                table: "DiagnosticAreaResolutionEvidences",
                column: "DiagnosticAreaResolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionEvidences_MappingId",
                schema: "diagnostics_core",
                table: "DiagnosticAreaResolutionEvidences",
                column: "MappingId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionEvidences_RuleId",
                schema: "diagnostics_core",
                table: "DiagnosticAreaResolutionEvidences",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionMappings_DiagnosticAreaId_RuleId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionMappings",
                columns: new[] { "DiagnosticAreaId", "RuleId" });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionMappings_IsActive_RuleId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionMappings",
                columns: new[] { "IsActive", "RuleId" });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionMappings_RuleId",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionMappings",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionRules_Code",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionRules_IsActive_SortOrder",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionRules",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutions_DiagnosticAreaId",
                schema: "diagnostics_core",
                table: "DiagnosticAreaResolutions",
                column: "DiagnosticAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutions_RuleId",
                schema: "diagnostics_core",
                table: "DiagnosticAreaResolutions",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutions_SourceId",
                schema: "diagnostics_core",
                table: "DiagnosticAreaResolutions",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionSources_Code",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionSources",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreaResolutionSources_IsActive_SortOrder",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreaResolutionSources",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticAreas_Code",
                schema: "diagnostics_lookup",
                table: "DiagnosticAreas",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticDefinitionGroups_Code",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticDefinitionGroups_IsActive_SortOrder",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitionGroups",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticDefinitions_GroupId_Code",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                columns: new[] { "GroupId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticDefinitions_GroupId_IsActive_SortOrder",
                schema: "diagnostics_lookup",
                table: "DiagnosticDefinitions",
                columns: new[] { "GroupId", "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticStatisticsWorkItems_ErrorGroupId",
                schema: "diagnostics_core",
                table: "DiagnosticStatisticsWorkItems",
                column: "ErrorGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticStatisticsWorkItems_StatusId_AvailableAt",
                schema: "diagnostics_core",
                table: "DiagnosticStatisticsWorkItems",
                columns: new[] { "StatusId", "AvailableAt" });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticStatisticsWorkItemStatuses_Code",
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticStatisticsWorkItemStatuses_IsActive_SortOrder",
                schema: "diagnostics_lookup",
                table: "DiagnosticStatisticsWorkItemStatuses",
                columns: new[] { "IsActive", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IsDeleted",
                schema: "app",
                table: "Employees",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCriticalities_EffectiveScore",
                schema: "diagnostics_core",
                table: "ErrorCriticalities",
                column: "EffectiveScore");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCriticalities_EffectiveScore_CalculatedAt",
                schema: "diagnostics_core",
                table: "ErrorCriticalities",
                columns: new[] { "EffectiveScore", "CalculatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCriticalities_ErrorGroupId",
                schema: "diagnostics_core",
                table: "ErrorCriticalities",
                column: "ErrorGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCriticalityFactors_DefinitionId",
                schema: "diagnostics_core",
                table: "ErrorCriticalityFactors",
                column: "DefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCriticalityFactors_ErrorCriticalityId_DefinitionId",
                schema: "diagnostics_core",
                table: "ErrorCriticalityFactors",
                columns: new[] { "ErrorCriticalityId", "DefinitionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_ApplicationId",
                schema: "diagnostics_core",
                table: "ErrorGroups",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_DiagnosticAreaResolutionId",
                schema: "diagnostics_core",
                table: "ErrorGroups",
                column: "DiagnosticAreaResolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_Fingerprint_FingerprintVersion",
                schema: "diagnostics_core",
                table: "ErrorGroups",
                columns: new[] { "Fingerprint", "FingerprintVersion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_LastOccurredAt",
                schema: "diagnostics_core",
                table: "ErrorGroups",
                column: "LastOccurredAt");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_SeverityId",
                schema: "diagnostics_core",
                table: "ErrorGroups",
                column: "SeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorGroups_StatusId",
                schema: "diagnostics_core",
                table: "ErrorGroups",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMetricBuckets_BucketTypeId_BucketStart",
                schema: "diagnostics_statistics",
                table: "ErrorMetricBuckets",
                columns: new[] { "BucketTypeId", "BucketStart" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMetricBuckets_CalculatedAt",
                schema: "diagnostics_statistics",
                table: "ErrorMetricBuckets",
                column: "CalculatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMetricBuckets_ErrorGroupId_BucketStart",
                schema: "diagnostics_statistics",
                table: "ErrorMetricBuckets",
                columns: new[] { "ErrorGroupId", "BucketStart" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMetricBuckets_ErrorGroupId_BucketTypeId_BucketStart_Bu~",
                schema: "diagnostics_statistics",
                table: "ErrorMetricBuckets",
                columns: new[] { "ErrorGroupId", "BucketTypeId", "BucketStart", "BucketDurationMinutes" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_ApplicationVersion",
                schema: "diagnostics_core",
                table: "ErrorOccurrences",
                column: "ApplicationVersion");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_CorrelationId",
                schema: "diagnostics_core",
                table: "ErrorOccurrences",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_DiagnosticAreaResolutionId",
                schema: "diagnostics_core",
                table: "ErrorOccurrences",
                column: "DiagnosticAreaResolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_ErrorGroupId_OccurredAt",
                schema: "diagnostics_core",
                table: "ErrorOccurrences",
                columns: new[] { "ErrorGroupId", "OccurredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_OccurredAt",
                schema: "diagnostics_core",
                table: "ErrorOccurrences",
                column: "OccurredAt");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_Platform",
                schema: "diagnostics_core",
                table: "ErrorOccurrences",
                column: "Platform");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_RequestId",
                schema: "diagnostics_core",
                table: "ErrorOccurrences",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorOccurrences_UserId",
                schema: "diagnostics_core",
                table: "ErrorOccurrences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorStatistics_CalculatedAt",
                schema: "diagnostics_statistics",
                table: "ErrorStatistics",
                column: "CalculatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorStatistics_ErrorGroupId",
                schema: "diagnostics_statistics",
                table: "ErrorStatistics",
                column: "ErrorGroupId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogChanges",
                schema: "app_logs");

            migrationBuilder.DropTable(
                name: "DiagnosticAreaResolutionEvidences",
                schema: "diagnostics_core");

            migrationBuilder.DropTable(
                name: "DiagnosticStatisticsWorkItems",
                schema: "diagnostics_core");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "app");

            migrationBuilder.DropTable(
                name: "ErrorCriticalityFactors",
                schema: "diagnostics_core");

            migrationBuilder.DropTable(
                name: "ErrorMetricBuckets",
                schema: "diagnostics_statistics");

            migrationBuilder.DropTable(
                name: "ErrorOccurrences",
                schema: "diagnostics_core");

            migrationBuilder.DropTable(
                name: "ErrorStatistics",
                schema: "diagnostics_statistics");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "app_logs");

            migrationBuilder.DropTable(
                name: "DiagnosticAreaResolutionMappings",
                schema: "diagnostics_lookup");

            migrationBuilder.DropTable(
                name: "DiagnosticStatisticsWorkItemStatuses",
                schema: "diagnostics_lookup");

            migrationBuilder.DropTable(
                name: "ErrorCriticalities",
                schema: "diagnostics_core");

            migrationBuilder.DropTable(
                name: "AuditActions",
                schema: "app_logs_lookup");

            migrationBuilder.DropTable(
                name: "ErrorGroups",
                schema: "diagnostics_core");

            migrationBuilder.DropTable(
                name: "DiagnosticAreaResolutions",
                schema: "diagnostics_core");

            migrationBuilder.DropTable(
                name: "DiagnosticDefinitions",
                schema: "diagnostics_lookup");

            migrationBuilder.DropTable(
                name: "DiagnosticAreaResolutionRules",
                schema: "diagnostics_lookup");

            migrationBuilder.DropTable(
                name: "DiagnosticAreaResolutionSources",
                schema: "diagnostics_lookup");

            migrationBuilder.DropTable(
                name: "DiagnosticAreas",
                schema: "diagnostics_lookup");

            migrationBuilder.DropTable(
                name: "DiagnosticDefinitionGroups",
                schema: "diagnostics_lookup");
        }
    }
}
