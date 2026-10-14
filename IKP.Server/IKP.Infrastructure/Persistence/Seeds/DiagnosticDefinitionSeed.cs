using IKP.Domain.Common.Diagnostics;
using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Infrastructure.Services.Extensions;
using IKP.Infrastructure.Persistence.Seeds.Records;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds
{
    /// <summary>
    /// Seeds system diagnostic definition groups and definitions.
    /// </summary>
    public static class DiagnosticDefinitionSeed
    {
        private const string pathBaseKey = "Diagnostic";
        private const string pathGroupKey = $"{pathBaseKey}.DefinitionGroup";
        private const uint baseIndex = 2;

        private static readonly Guid DiagnosticSeverityGroupId = baseIndex.GetGuidForLevel(0, 1);
        private static readonly Guid ErrorGroupStatusGroupId = baseIndex.GetGuidForLevel(0, 2);
        private static readonly Guid CriticalityFactorGroupId = baseIndex.GetGuidForLevel(0, 3);
        private static readonly Guid MetricBucketTypeGroupId = baseIndex.GetGuidForLevel(0, 4);

        private static readonly IReadOnlyDictionary<Guid, string> pathSubsGroups = new Dictionary<Guid, string>
        {
            { DiagnosticSeverityGroupId, $"{pathBaseKey}.Severity" },
            { ErrorGroupStatusGroupId, $"{pathBaseKey}.ErrorGroupStatus" },
            { CriticalityFactorGroupId, $"{pathBaseKey}.CriticalityFactor" },
            { MetricBucketTypeGroupId, $"{pathBaseKey}.MetricBucketType" }
        };

        /// <summary>
        /// Adds system diagnostic definition groups and definitions to the model.
        /// </summary>
        /// <param name="modelBuilder">Entity Framework model builder.</param>
        public static void Seed(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DiagnosticDefinitionGroup>().HasData(
                new[]
                {
                    new SeedRecord<DiagnosticDefinitionGroup>(DiagnosticSeverityGroupId, DiagnosticDefinitionGroupCodes.Severity),
                    new SeedRecord<DiagnosticDefinitionGroup>(ErrorGroupStatusGroupId, DiagnosticDefinitionGroupCodes.ErrorGroupStatus),
                    new SeedRecord<DiagnosticDefinitionGroup>(CriticalityFactorGroupId, DiagnosticDefinitionGroupCodes.CriticalityFactor),
                    new SeedRecord<DiagnosticDefinitionGroup>(MetricBucketTypeGroupId, DiagnosticDefinitionGroupCodes.MetricBucketType)
                }.CreateListDefinition(pathGroupKey)
            );

            modelBuilder.Entity<DiagnosticDefinition>().HasData(
                new[]
                {
                    new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(1, 1), DiagnosticSeverityCodes.Information, Configure: entity => entity.GroupId = DiagnosticSeverityGroupId),
                    new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(1, 2), DiagnosticSeverityCodes.Warning, Configure: entity => entity.GroupId = DiagnosticSeverityGroupId),
                    new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(1, 3), DiagnosticSeverityCodes.Error, Configure: entity => entity.GroupId = DiagnosticSeverityGroupId),
                    new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(1, 4), DiagnosticSeverityCodes.Critical, Configure: entity => entity.GroupId = DiagnosticSeverityGroupId)
                }.CreateListDefinition(pathSubsGroups.GetValueOrDefault(DiagnosticSeverityGroupId)).Concat(
                    new[]
                    {
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(2, 1), ErrorGroupStatusCodes.New, Configure: entity => entity.GroupId = ErrorGroupStatusGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(2, 2), ErrorGroupStatusCodes.Investigating, Configure: entity => entity.GroupId = ErrorGroupStatusGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(2, 3), ErrorGroupStatusCodes.Known, Configure: entity => entity.GroupId = ErrorGroupStatusGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(2, 4), ErrorGroupStatusCodes.FixPlanned, Configure: entity => entity.GroupId = ErrorGroupStatusGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(2, 5), ErrorGroupStatusCodes.Fixed, Configure: entity => entity.GroupId = ErrorGroupStatusGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(2, 6), ErrorGroupStatusCodes.Monitoring, Configure: entity => entity.GroupId = ErrorGroupStatusGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(2, 7), ErrorGroupStatusCodes.Resolved, Configure: entity => entity.GroupId = ErrorGroupStatusGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(2, 8), ErrorGroupStatusCodes.Ignored, Configure: entity => entity.GroupId = ErrorGroupStatusGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(2, 9), ErrorGroupStatusCodes.WontFix, Configure: entity => entity.GroupId = ErrorGroupStatusGroupId)
                    }.CreateListDefinition(pathSubsGroups.GetValueOrDefault(ErrorGroupStatusGroupId))
                ).Concat(
                    new[]
                    {
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(3, 1), CriticalityFactorCodes.OccurrenceRate, Configure: entity => entity.GroupId = CriticalityFactorGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(3, 2), CriticalityFactorCodes.AffectedUsers, Configure: entity => entity.GroupId = CriticalityFactorGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(3, 3), CriticalityFactorCodes.AffectedUserRatio, Configure: entity => entity.GroupId = CriticalityFactorGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(3, 4), CriticalityFactorCodes.FeatureImportance, Configure: entity => entity.GroupId = CriticalityFactorGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(3, 5), CriticalityFactorCodes.Trend, Configure: entity => entity.GroupId = CriticalityFactorGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(3, 6), CriticalityFactorCodes.FailureRate, Configure: entity => entity.GroupId = CriticalityFactorGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(3, 7), CriticalityFactorCodes.Regression, Configure: entity => entity.GroupId = CriticalityFactorGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(3, 8), CriticalityFactorCodes.BusinessImpact, Configure: entity => entity.GroupId = CriticalityFactorGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(3, 9), CriticalityFactorCodes.SupportImpact, Configure: entity => entity.GroupId = CriticalityFactorGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(3, 10), CriticalityFactorCodes.DataLossRisk, Configure: entity => entity.GroupId = CriticalityFactorGroupId)
                    }.CreateListDefinition(pathSubsGroups.GetValueOrDefault(CriticalityFactorGroupId))
                ).Concat(
                    new[]
                    {
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(4, 1), MetricBucketTypeCodes.Minute, Configure: entity => entity.GroupId = MetricBucketTypeGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(4, 2), MetricBucketTypeCodes.Hour, Configure: entity => entity.GroupId = MetricBucketTypeGroupId),
                        new SeedRecord<DiagnosticDefinition>(baseIndex.GetGuidForLevel(4, 3), MetricBucketTypeCodes.Day, Configure: entity => entity.GroupId = MetricBucketTypeGroupId)
                    }.CreateListDefinition(pathSubsGroups.GetValueOrDefault(MetricBucketTypeGroupId))
                )
            );
        }
    }
}
