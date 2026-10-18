using IKP.Domain.Common.Support;
using IKP.Domain.Entities.Support.Lookups;
using IKP.Infrastructure.Persistence.Seeds.Records;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds;

/// <summary>
/// Seeds built-in support ticket priorities.
/// </summary>
public static class SupportTicketPrioritySeed
{
    private const string PathKey = "Support.TicketPriority";
    private const uint BaseIndex = 9;

    private static readonly Guid LowId = BaseIndex.GetGuidForLevel(1, 1);
    private static readonly Guid NormalId = BaseIndex.GetGuidForLevel(1, 2);
    private static readonly Guid HighId = BaseIndex.GetGuidForLevel(1, 3);
    private static readonly Guid CriticalId = BaseIndex.GetGuidForLevel(1, 4);

    /// <summary>
    /// Adds built-in support priorities to the EF model.
    /// </summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupportTicketPriority>().HasData(
            new[]
            {
                new SeedRecord<SupportTicketPriority>(LowId, SupportTicketPriorityCodes.Low, Configure: x => x.Weight = 10),
                new SeedRecord<SupportTicketPriority>(NormalId, SupportTicketPriorityCodes.Normal, Configure: x => x.Weight = 20),
                new SeedRecord<SupportTicketPriority>(HighId, SupportTicketPriorityCodes.High, Configure: x => x.Weight = 30),
                new SeedRecord<SupportTicketPriority>(CriticalId, SupportTicketPriorityCodes.Critical, Configure: x => x.Weight = 40)
            }.CreateListDefinition(PathKey));

    }
}
