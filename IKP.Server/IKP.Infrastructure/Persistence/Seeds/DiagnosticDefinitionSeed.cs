using IKP.Domain.Common.Diagnostics;
using IKP.Domain.Entities.Diagnostics.Definitions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds
{
    /// <summary>
    /// Seeds system diagnostic definition groups and definitions.
    /// </summary>
    public static class DiagnosticDefinitionSeed
    {
        private static readonly Guid DiagnosticSeverityGroupId = new("20000000-0000-0000-0000-000000000001");
        private static readonly Guid ErrorGroupStatusGroupId = new("20000000-0000-0000-0000-000000000002");
        private static readonly Guid CriticalityFactorGroupId = new("20000000-0000-0000-0000-000000000003");
        private static readonly Guid MetricBucketTypeGroupId = new("20000000-0000-0000-0000-000000000004");

        /// <summary>
        /// Adds system diagnostic definition groups and definitions to the model.
        /// </summary>
        /// <param name="modelBuilder">Entity Framework model builder.</param>
        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedGroups(modelBuilder);
            SeedDiagnosticSeverity(modelBuilder);
            SeedErrorGroupStatus(modelBuilder);
            SeedCriticalityFactors(modelBuilder);
            SeedMetricBucketTypes(modelBuilder);
        }

        private static void SeedGroups(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiagnosticDefinitionGroup>().HasData(
                new DiagnosticDefinitionGroup
                {
                    Id = DiagnosticSeverityGroupId,
                    Code = "DIAGNOSTIC_SEVERITY",
                    NameKey = "Diagnostic.DefinitionGroup.Severity",
                    DescriptionKey = "Diagnostic.DefinitionGroup.Severity.Description",
                    SortOrder = 10,
                    IsActive = true,
                    IsSystem = true
                },
                new DiagnosticDefinitionGroup
                {
                    Id = ErrorGroupStatusGroupId,
                    Code = "ERROR_GROUP_STATUS",
                    NameKey = "Diagnostic.DefinitionGroup.ErrorGroupStatus",
                    DescriptionKey = "Diagnostic.DefinitionGroup.ErrorGroupStatus.Description",
                    SortOrder = 20,
                    IsActive = true,
                    IsSystem = true
                },
                new DiagnosticDefinitionGroup
                {
                    Id = CriticalityFactorGroupId,
                    Code = "CRITICALITY_FACTOR",
                    NameKey = "Diagnostic.DefinitionGroup.CriticalityFactor",
                    DescriptionKey = "Diagnostic.DefinitionGroup.CriticalityFactor.Description",
                    SortOrder = 30,
                    IsActive = true,
                    IsSystem = true
                },
                new DiagnosticDefinitionGroup
                {
                    Id = MetricBucketTypeGroupId,
                    Code = "METRIC_BUCKET_TYPE",
                    NameKey = "Diagnostic.DefinitionGroup.MetricBucketType",
                    DescriptionKey = "Diagnostic.DefinitionGroup.MetricBucketType.Description",
                    SortOrder = 40,
                    IsActive = true,
                    IsSystem = true
                });
        }

        private static void SeedDiagnosticSeverity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiagnosticDefinition>().HasData(
                CreateDefinition("21000000-0000-0000-0000-000000000001", DiagnosticSeverityGroupId, DiagnosticSeverityCodes.Information, "Diagnostic.Severity.Information", 10),
                CreateDefinition("21000000-0000-0000-0000-000000000002", DiagnosticSeverityGroupId, DiagnosticSeverityCodes.Warning, "Diagnostic.Severity.Warning", 20),
                CreateDefinition("21000000-0000-0000-0000-000000000003", DiagnosticSeverityGroupId, DiagnosticSeverityCodes.Error, "Diagnostic.Severity.Error", 30),
                CreateDefinition("21000000-0000-0000-0000-000000000004", DiagnosticSeverityGroupId, DiagnosticSeverityCodes.Critical, "Diagnostic.Severity.Critical", 40));
        }

        private static void SeedErrorGroupStatus(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiagnosticDefinition>().HasData(
                CreateDefinition("22000000-0000-0000-0000-000000000001", ErrorGroupStatusGroupId, ErrorGroupStatusCodes.New, "Diagnostic.ErrorGroupStatus.New", 10),
                CreateDefinition("22000000-0000-0000-0000-000000000002", ErrorGroupStatusGroupId, ErrorGroupStatusCodes.Investigating, "Diagnostic.ErrorGroupStatus.Investigating", 20),
                CreateDefinition("22000000-0000-0000-0000-000000000003", ErrorGroupStatusGroupId, ErrorGroupStatusCodes.Known, "Diagnostic.ErrorGroupStatus.Known", 30),
                CreateDefinition("22000000-0000-0000-0000-000000000004", ErrorGroupStatusGroupId, ErrorGroupStatusCodes.FixPlanned, "Diagnostic.ErrorGroupStatus.FixPlanned", 40),
                CreateDefinition("22000000-0000-0000-0000-000000000005", ErrorGroupStatusGroupId, ErrorGroupStatusCodes.Fixed, "Diagnostic.ErrorGroupStatus.Fixed", 50),
                CreateDefinition("22000000-0000-0000-0000-000000000006", ErrorGroupStatusGroupId, ErrorGroupStatusCodes.Monitoring, "Diagnostic.ErrorGroupStatus.Monitoring", 60),
                CreateDefinition("22000000-0000-0000-0000-000000000007", ErrorGroupStatusGroupId, ErrorGroupStatusCodes.Resolved, "Diagnostic.ErrorGroupStatus.Resolved", 70),
                CreateDefinition("22000000-0000-0000-0000-000000000008", ErrorGroupStatusGroupId, ErrorGroupStatusCodes.Ignored, "Diagnostic.ErrorGroupStatus.Ignored", 80),
                CreateDefinition("22000000-0000-0000-0000-000000000009", ErrorGroupStatusGroupId, ErrorGroupStatusCodes.WontFix, "Diagnostic.ErrorGroupStatus.WontFix", 90));
        }

        private static void SeedCriticalityFactors(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiagnosticDefinition>().HasData(
                CreateDefinition("23000000-0000-0000-0000-000000000001", CriticalityFactorGroupId, CriticalityFactorCodes.OccurrenceRate, "Diagnostic.CriticalityFactor.OccurrenceRate", 10),
                CreateDefinition("23000000-0000-0000-0000-000000000002", CriticalityFactorGroupId, CriticalityFactorCodes.AffectedUsers, "Diagnostic.CriticalityFactor.AffectedUsers", 20),
                CreateDefinition("23000000-0000-0000-0000-000000000003", CriticalityFactorGroupId, CriticalityFactorCodes.AffectedUserRatio, "Diagnostic.CriticalityFactor.AffectedUserRatio", 30),
                CreateDefinition("23000000-0000-0000-0000-000000000004", CriticalityFactorGroupId, CriticalityFactorCodes.FeatureImportance, "Diagnostic.CriticalityFactor.FeatureImportance", 40),
                CreateDefinition("23000000-0000-0000-0000-000000000005", CriticalityFactorGroupId, CriticalityFactorCodes.Trend, "Diagnostic.CriticalityFactor.Trend", 50),
                CreateDefinition("23000000-0000-0000-0000-000000000006", CriticalityFactorGroupId, CriticalityFactorCodes.FailureRate, "Diagnostic.CriticalityFactor.FailureRate", 60),
                CreateDefinition("23000000-0000-0000-0000-000000000007", CriticalityFactorGroupId, CriticalityFactorCodes.Regression, "Diagnostic.CriticalityFactor.Regression", 70),
                CreateDefinition("23000000-0000-0000-0000-000000000008", CriticalityFactorGroupId, CriticalityFactorCodes.BusinessImpact, "Diagnostic.CriticalityFactor.BusinessImpact", 80),
                CreateDefinition("23000000-0000-0000-0000-000000000009", CriticalityFactorGroupId, CriticalityFactorCodes.SupportImpact, "Diagnostic.CriticalityFactor.SupportImpact", 90),
                CreateDefinition("23000000-0000-0000-0000-000000000010", CriticalityFactorGroupId, CriticalityFactorCodes.DataLossRisk, "Diagnostic.CriticalityFactor.DataLossRisk", 100));
        }

        private static void SeedMetricBucketTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiagnosticDefinition>().HasData(
                CreateDefinition("24000000-0000-0000-0000-000000000001", MetricBucketTypeGroupId, MetricBucketTypeCodes.Minute, "Diagnostic.MetricBucketType.Minute", 10),
                CreateDefinition("24000000-0000-0000-0000-000000000002", MetricBucketTypeGroupId, MetricBucketTypeCodes.Hour, "Diagnostic.MetricBucketType.Hour", 20),
                CreateDefinition("24000000-0000-0000-0000-000000000003", MetricBucketTypeGroupId, MetricBucketTypeCodes.Day, "Diagnostic.MetricBucketType.Day", 30));
        }

        private static DiagnosticDefinition CreateDefinition(string id, Guid groupId, string code, string nameKey, int sortOrder)
        {
            return new DiagnosticDefinition
            {
                Id = new Guid(id),
                GroupId = groupId,
                Code = code,
                NameKey = nameKey,
                DescriptionKey = nameKey + ".Description",
                SortOrder = sortOrder,
                IsActive = true,
                IsSystem = true
            };
        }
    }
}
