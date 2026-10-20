using IKP.Domain.Common.Support;
using IKP.Domain.Entities.Support.Lookups;
using IKP.Infrastructure.Persistence.Seeds.Records;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds;

/// <summary>
/// Seeds built-in support ticket statuses.
/// </summary>
public static class SupportTicketStatusSeed
{
    private const string PathKey = "Support.TicketStatus";
    private const uint BaseIndex = 9;

    private static readonly Guid NewId = BaseIndex.GetGuidForLevel(0, 1);
    private static readonly Guid OpenId = BaseIndex.GetGuidForLevel(0, 2);
    private static readonly Guid InProgressId = BaseIndex.GetGuidForLevel(0, 3);
    private static readonly Guid WaitingForUserId = BaseIndex.GetGuidForLevel(0, 4);
    private static readonly Guid ResolvedId = BaseIndex.GetGuidForLevel(0, 5);
    private static readonly Guid ClosedId = BaseIndex.GetGuidForLevel(0, 6);

    /// <summary>
    /// Adds built-in support statuses to the EF model.
    /// </summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupportTicketStatus>().HasData(
            new[]
            {
                new SeedRecord<SupportTicketStatus>(NewId, SupportTicketStatusCodes.New),
                new SeedRecord<SupportTicketStatus>(OpenId, SupportTicketStatusCodes.Open),
                new SeedRecord<SupportTicketStatus>(InProgressId, SupportTicketStatusCodes.InProgress),
                new SeedRecord<SupportTicketStatus>(WaitingForUserId, SupportTicketStatusCodes.WaitingForUser),
                new SeedRecord<SupportTicketStatus>(ResolvedId, SupportTicketStatusCodes.Resolved, Configure: x => x.IsTerminal = true),
                new SeedRecord<SupportTicketStatus>(ClosedId, SupportTicketStatusCodes.Closed, Configure: x => x.IsTerminal = true)
            }.CreateListDefinitionLocalized(PathKey));

    }
}
