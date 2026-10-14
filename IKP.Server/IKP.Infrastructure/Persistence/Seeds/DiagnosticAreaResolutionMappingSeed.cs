using IKP.Domain.Common.Diagnostics;
using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds
{
    /// <summary>
    /// Provides system mappings used to resolve diagnostic areas from runtime diagnostic context.
    /// </summary>
    public static class DiagnosticAreaResolutionMappingSeed
    {
        private const string AreaLookup = "Areas";
        private const string RuleLookup = "Rules";
        private const string MappingLookup = "Mappings";

        private const uint areaBaseIndex = 3;
        private const uint ruleBaseIndex = 5;
        private const uint mappingBaseIndex = 6;

        private static readonly IReadOnlyCollection<string> Areas = new[]
        {
            DiagnosticAreaCodes.Api.ToPascalCase(),
            DiagnosticAreaCodes.Diagnostics.ToPascalCase()
        };

        private static readonly IReadOnlyCollection<string> Rules = new[]
        {
            DiagnosticAreaResolutionRuleCodes.Namespace.ToPascalCase(),
            DiagnosticAreaResolutionRuleCodes.Endpoint.ToPascalCase(),
            DiagnosticAreaResolutionRuleCodes.HttpPipeline.ToPascalCase()
        };

        private static readonly IReadOnlyDictionary<string, string> MappingException = new Dictionary<string, string>
        {
            { DiagnosticAreaCodes.Diagnostics.ToPascalCase(), DiagnosticAreaResolutionRuleCodes.HttpPipeline.ToPascalCase() }
        };

        private static readonly IReadOnlyDictionary<string, string> LookupKeys = new Dictionary<string, string>()
        {
            { AreaLookup, "AreaId" },
            { RuleLookup, "RuleId" },
            { MappingLookup, "MappingId" },
        };

        private static readonly ILookup<string, KeyValuePair<string, Guid>> guids =
            Areas.Select((area, i) => new KeyValuePair<string, Guid>(area + LookupKeys[AreaLookup], areaBaseIndex.GetGuidForLevel(0, (ulong)i + 1))).Concat(
            Rules.Select((rule, i) => new KeyValuePair<string, Guid>(rule + LookupKeys[RuleLookup], ruleBaseIndex.GetGuidForLevel(0, (ulong)(i + 3))))).Concat(
            Areas.SelectMany(area => Rules.Where(rule => !IsExcluded(area, rule)), (area, rule) => (Area: area, Rule: rule))
                .Select((pair, i) => new KeyValuePair<string, Guid>(pair.Area + pair.Rule + LookupKeys[MappingLookup], mappingBaseIndex.GetGuidForLevel(0, (ulong)i + 1))))
        .ToLookup(pair => pair.Key.EndsWith(LookupKeys[AreaLookup]) ? AreaLookup : pair.Key.EndsWith(LookupKeys[RuleLookup]) ? RuleLookup : MappingLookup, pair => pair);

        private static readonly IReadOnlyCollection<(IEnumerable<Guid> guids, string pattern, (int priority, decimal confidence) grades)> mappings = new[]
        {
            (GetGuidsForMapping(Areas.ElementAt(0), Rules.ElementAt(0)), @"^IKP\.Api(?:\.|$)", (100, 90m)),
            (GetGuidsForMapping(Areas.ElementAt(0), Rules.ElementAt(1)), @"^/api(?:/|$)", (100, 95m)),
            (GetGuidsForMapping(Areas.ElementAt(0), Rules.ElementAt(2)), @"^(GET|POST|PUT|PATCH|DELETE|OPTIONS|HEAD)\s+/api(?:/|$)", (80, 85m)),
            (GetGuidsForMapping(Areas.ElementAt(1), Rules.ElementAt(0)), @"^IKP\.Application\.Diagnostics(?:\.|$)", (100, 95m)),
            (GetGuidsForMapping(Areas.ElementAt(1), Rules.ElementAt(1)), @"^/api/diagnostics(?:/|$)", (100, 95m)),
        };

        /// <summary>
        /// Adds system diagnostic area resolution mappings to the EF Core model.
        /// </summary>
        /// <param name="modelBuilder">Entity Framework model builder.</param>
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiagnosticAreaResolutionMapping>().HasData(

                mappings.Select(mapping => new DiagnosticAreaResolutionMapping
                {
                    Id = mapping.guids.ElementAt(0),
                    DiagnosticAreaId = mapping.guids.ElementAt(1),
                    RuleId = mapping.guids.ElementAt(2),
                    Pattern = mapping.pattern,
                    Priority = mapping.grades.priority,
                    Confidence = mapping.grades.confidence,
                    IsActive = true,
                    IsSystem = true
                })
            );
        }

        private static Guid GetGuidFromLookupAndKey(string lookup, string key) => guids[lookup].FirstOrDefault(pair => pair.Key == key).Value;

        private static IEnumerable<Guid> GetGuidsForMapping(string area, string rule)
        {
            for (int time = 1; time <= 3; time++)
            {
                yield return time switch
                {
                    1 => GetGuidFromLookupAndKey(MappingLookup, area + rule + LookupKeys[MappingLookup]),
                    2 => GetGuidFromLookupAndKey(AreaLookup, area + LookupKeys[AreaLookup]),
                    3 => GetGuidFromLookupAndKey(RuleLookup, rule + LookupKeys[RuleLookup]),
                    _ => Guid.NewGuid()
                };
            }
        }

        private static bool IsExcluded(string area, string rule) => MappingException.TryGetValue(area, out string? excludedRule) && excludedRule == rule;
    }
}
