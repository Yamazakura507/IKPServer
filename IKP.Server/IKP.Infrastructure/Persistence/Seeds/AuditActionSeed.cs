using IKP.Domain.Common.Audit;
using IKP.Domain.Entities.Logs;
using IKP.Infrastructure.Persistence.Seeds.Records;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds;

/// <summary>
/// Seeds audit action definitions.
/// </summary>
public static class AuditActionSeed
{
    private const string PathKey = "Audit.Action";
    private const uint BaseIndex = 1;

    /// <summary>
    /// Gets the stable identifier of CREATE.
    /// </summary>
    public static readonly Guid CreateId = BaseIndex.GetGuidForLevel(0, 1);
    /// <summary>
    /// Gets the stable identifier of UPDATE.
    /// </summary>
    public static readonly Guid UpdateId = BaseIndex.GetGuidForLevel(0, 2);
    /// <summary>
    /// Gets the stable identifier of DELETE.
    /// </summary>
    public static readonly Guid DeleteId = BaseIndex.GetGuidForLevel(0, 3);
    /// <summary>
    /// Gets the stable identifier of RESTORE_DELETE.
    /// </summary>
    public static readonly Guid RestoreDeleteId = BaseIndex.GetGuidForLevel(0, 4);
    /// <summary>
    /// Gets the stable identifier of RESTORE_UPDATE.
    /// </summary>
    public static readonly Guid RestoreUpdateId = BaseIndex.GetGuidForLevel(0, 5);
    /// <summary>
    /// Adds audit action definitions to the EF model.
    /// </summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditActionDefinition>().HasData(
            new[]
            {
                new SeedRecord<AuditActionDefinition>(CreateId, AuditActionCodes.Create),
                new SeedRecord<AuditActionDefinition>(UpdateId, AuditActionCodes.Update),
                new SeedRecord<AuditActionDefinition>(DeleteId, AuditActionCodes.Delete),
                new SeedRecord<AuditActionDefinition>(RestoreDeleteId, AuditActionCodes.RestoreDelete),
                new SeedRecord<AuditActionDefinition>(RestoreUpdateId, AuditActionCodes.RestoreUpdate)
            }.CreateListDefinition(PathKey));
    }
}
