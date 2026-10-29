using IKP.Domain.Common.Codes.Support;
using IKP.Domain.Entities.Support.Lookups;
using IKP.Infrastructure.Persistence.Seeds.Records;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds.Seeds.Support;

/// <summary>
/// Seeds built-in support ticket categories.
/// </summary>
public static class SupportTicketCategorySeed
{
    private const string PathKey = "Support.TicketCategory";
    private const uint BaseIndex = 9;

    private static readonly Guid GeneralId = BaseIndex.GetGuidForLevel(2, 1);
    private static readonly Guid TechnicalId = BaseIndex.GetGuidForLevel(2, 2);
    private static readonly Guid AccessId = BaseIndex.GetGuidForLevel(2, 3);
    private static readonly Guid FilesId = BaseIndex.GetGuidForLevel(2, 4);
    private static readonly Guid OtherId = BaseIndex.GetGuidForLevel(2, 5);

    /// <summary>
    /// Adds built-in support categories to the EF model.
    /// </summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupportTicketCategory>().HasData(
            new[]
            {
                new SeedRecord<SupportTicketCategory>(GeneralId, SupportTicketCategoryCodes.General),
                new SeedRecord<SupportTicketCategory>(TechnicalId, SupportTicketCategoryCodes.Technical),
                new SeedRecord<SupportTicketCategory>(AccessId, SupportTicketCategoryCodes.Access),
                new SeedRecord<SupportTicketCategory>(FilesId, SupportTicketCategoryCodes.Files),
                new SeedRecord<SupportTicketCategory>(OtherId, SupportTicketCategoryCodes.Other)
            }.CreateListDefinitionLocalized(PathKey));

    }
}
