using IKP.Domain.Common.Diagnostics;
using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Infrastructure.Persistence.Seeds.Records;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds
{
    /// <summary>
    /// Provides seed data for diagnostic area resolution sources and rules.
    /// </summary>
    public static class DiagnosticAreaResolutionSeed
    {
        private const string sourcePathKey = "Diagnostics.AreaResolutionSource";
        private const string rulePathKey = "Diagnostics.AreaResolutionRule";
        private const uint sourceBaseIndex = 4;
        private const uint ruleBaseIndex = 5;

        private static readonly Guid ExplicitSourceId = sourceBaseIndex.GetGuidForLevel(0, 1);
        private static readonly Guid AutomaticSourceId = sourceBaseIndex.GetGuidForLevel(0, 2);

        private static readonly Guid ExplicitContextRuleId = ruleBaseIndex.GetGuidForLevel(0, 1);
        private static readonly Guid ExceptionTypeRuleId = ruleBaseIndex.GetGuidForLevel(0, 2);
        private static readonly Guid NamespaceRuleId = ruleBaseIndex.GetGuidForLevel(0, 3);
        private static readonly Guid EndpointRuleId = ruleBaseIndex.GetGuidForLevel(0, 4);
        private static readonly Guid HttpPipelineRuleId = ruleBaseIndex.GetGuidForLevel(0, 5);

        /// <summary>
        /// Adds diagnostic area resolution sources and rules to the EF Core model.
        /// </summary>
        /// <param name="modelBuilder">The EF Core model builder.</param>
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiagnosticAreaResolutionSource>().HasData(
                new[]
                {
                    new SeedRecord<DiagnosticAreaResolutionSource>(ExplicitSourceId, DiagnosticAreaResolutionSourceCodes.Explicit),
                    new SeedRecord<DiagnosticAreaResolutionSource>(AutomaticSourceId, DiagnosticAreaResolutionSourceCodes.Automatic)
                }.CreateListDefinition(sourcePathKey)
            );

            modelBuilder.Entity<DiagnosticAreaResolutionRule>().HasData(
                new[]
                {
                    new SeedRecord<DiagnosticAreaResolutionRule>(ExplicitContextRuleId, DiagnosticAreaResolutionRuleCodes.ExplicitContext),
                    new SeedRecord<DiagnosticAreaResolutionRule>(ExceptionTypeRuleId, DiagnosticAreaResolutionRuleCodes.ExceptionType),
                    new SeedRecord<DiagnosticAreaResolutionRule>(NamespaceRuleId, DiagnosticAreaResolutionRuleCodes.Namespace),
                    new SeedRecord<DiagnosticAreaResolutionRule>(EndpointRuleId, DiagnosticAreaResolutionRuleCodes.Endpoint),
                    new SeedRecord<DiagnosticAreaResolutionRule>(HttpPipelineRuleId, DiagnosticAreaResolutionRuleCodes.HttpPipeline)
                }.CreateListDefinition(rulePathKey)
            );
        }
    }
}
