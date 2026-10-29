using IKP.Domain.Common.Codes.Diagnostics;
using IKP.Domain.Entities.Diagnostics.Statistics;
using IKP.Infrastructure.Persistence.Seeds.Records;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds.Seeds.Diagnostic
{
    /// <summary>
    /// Provides seed data for diagnostic statistics work item statuses.
    /// </summary>
    public static class DiagnosticStatisticsWorkItemStatusSeed
    {
        private const string pathKey = "Diagnostics.StatisticsWorkItemStatus";
        private const uint baseIndex = 6;

        private static readonly Guid PendingId = baseIndex.GetGuidForLevel(0, 1);
        private static readonly Guid ProcessingId = baseIndex.GetGuidForLevel(0, 2);
        private static readonly Guid CompletedId = baseIndex.GetGuidForLevel(0, 3);
        private static readonly Guid FailedId = baseIndex.GetGuidForLevel(0, 4);

        /// <summary>
        /// Adds system diagnostic statistics work item statuses to the EF Core model.
        /// </summary>
        /// <param name="modelBuilder">The EF Core model builder.</param>
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiagnosticStatisticsWorkItemStatus>().HasData(
                new[]
                {
                    new SeedRecord<DiagnosticStatisticsWorkItemStatus>(PendingId, DiagnosticStatisticsWorkItemStatusCodes.Pending),
                    new SeedRecord<DiagnosticStatisticsWorkItemStatus>(ProcessingId, DiagnosticStatisticsWorkItemStatusCodes.Processing),
                    new SeedRecord<DiagnosticStatisticsWorkItemStatus>(CompletedId, DiagnosticStatisticsWorkItemStatusCodes.Completed),
                    new SeedRecord<DiagnosticStatisticsWorkItemStatus>(FailedId, DiagnosticStatisticsWorkItemStatusCodes.Failed)
                }.CreateListDefinitionLocalized(pathKey)
            );
        }
    }
}
