using IKP.Domain.Common.Base;
using IKP.Infrastructure.Persistence.Seeds;
using IKP.Infrastructure.Persistence.Seeds.Records;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.Extensions
{
    internal static class SeedExtensions
    {
        private const string FormatGuid = "{0}{1}-0000-0000-0000-{2:D12}";
        private const int sortOrderDefault = 10;

        internal static TSeed CreateDefinitionLocalized<TSeed>(this SeedRecord<TSeed> seedRecord, string? pathKey = null) where TSeed : FullLookupLocalized, new()
        {
            string nameKey = seedRecord.NameKey ?? (pathKey is null ? throw new ArgumentNullException(nameof(pathKey)) : $"{pathKey}.{seedRecord.Code.ToPascalCase()}");

            TSeed seed = new()
            {
                Id = seedRecord.Id,
                Code = seedRecord.Code,
                NameKey = nameKey,
                DescriptionKey = seedRecord.DescriptionKey ?? nameKey + ".Description",
                SortOrder = seedRecord.SortOrder ?? sortOrderDefault,
                IsActive = true,
                IsSystem = true,
                FileId = seedRecord.FileId,
            };

            seedRecord.Configure?.Invoke(seed);

            return seed;
        }

        internal static TSeed CreateDefinition<TSeed>(this SeedRecord<TSeed> seedRecord, string? pathKey = null) where TSeed : FullLookup, new()
        {
            string name = seedRecord.NameKey ?? (pathKey is null ? throw new ArgumentNullException(nameof(pathKey)) : $"{pathKey}.{seedRecord.Code.ToPascalCase()}");

            TSeed seed = new()
            {
                Id = seedRecord.Id,
                Code = seedRecord.Code,
                Name = name,
                Description = seedRecord.DescriptionKey,
                SortOrder = seedRecord.SortOrder ?? sortOrderDefault,
                IsActive = true,
                IsSystem = true,
                FileId = seedRecord.FileId,
            };

            seedRecord.Configure?.Invoke(seed);

            return seed;
        }

        internal static TSeed[] CreateListDefinitionLocalized<TSeed>(this IReadOnlyCollection<SeedRecord<TSeed>> seedRecords, string? pathKey = null) 
            where TSeed : FullLookupLocalized, new() =>
                seedRecords
                    .Select((seedRecord, i) => seedRecord.SortOrder is null ? seedRecord with { SortOrder = sortOrderDefault * (i + 1) } : seedRecord)
                    .Select(record => record.CreateDefinitionLocalized(pathKey))
                    .ToArray();

        internal static TSeed[] CreateListDefinition<TSeed>(this IReadOnlyCollection<SeedRecord<TSeed>> seedRecords, string? pathKey = null)
            where TSeed : FullLookup, new() =>
                seedRecords
                    .Select((seedRecord, i) => seedRecord.SortOrder is null ? seedRecord with { SortOrder = sortOrderDefault * (i + 1) } : seedRecord)
                    .Select(record => record.CreateDefinition(pathKey))
                    .ToArray();

        internal static Guid GetGuidForLevel(this uint baseIndex, uint level, ulong index)
        {
            int pow = 8 - level.ToString().Length - baseIndex.ToString().Length;

            if (pow < 0) throw new ArgumentException($"level or baseNum is overweight. level:{level}, baseNum:{baseIndex}");
            if (index < 1 || index >= 1e13) throw new ArgumentException($"index is not correct. index:{index}");

            return new(String.Format(FormatGuid, baseIndex, (level * (int)Math.Pow(10, pow)).ToString("D7"), index));
        }

        internal static void OnSeeds(this ModelBuilder modelBuilder)
        {
            AuditActionSeed.Seed(modelBuilder);

            AuthenticationMethodSeed.Seed(modelBuilder);
            AuthorizationSeed.Seed(modelBuilder);

            DiagnosticDefinitionSeed.Seed(modelBuilder);
            DiagnosticAreaSeed.Seed(modelBuilder);
            DiagnosticAreaResolutionSeed.Seed(modelBuilder);
            DiagnosticAreaResolutionMappingSeed.Seed(modelBuilder);
            DiagnosticStatisticsWorkItemStatusSeed.Seed(modelBuilder);

            SupportTicketStatusSeed.Seed(modelBuilder);
            SupportTicketPrioritySeed.Seed(modelBuilder);
            SupportTicketCategorySeed.Seed(modelBuilder);
            SupportAuthorizationSeed.Seed(modelBuilder);

            LanguageSeed.Seed(modelBuilder);

            UpdateSeed.Seed(modelBuilder);
        }
    }
}
