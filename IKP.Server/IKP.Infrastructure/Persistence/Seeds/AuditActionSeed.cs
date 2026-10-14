using IKP.Domain.Common.Audit;
using IKP.Infrastructure.Services.Extensions;
using IKP.Domain.Entities.Logs;
using IKP.Infrastructure.Persistence.Seeds.Records;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds
{
    public static class AuditActionSeed
    {
        private const string pathKey = "Audit.Action";
        private const uint baseIndex = 1;

        public static readonly Guid CreateId = baseIndex.GetGuidForLevel(0, 1);
        public static readonly Guid UpdateId = baseIndex.GetGuidForLevel(0, 2);
        public static readonly Guid DeleteId = baseIndex.GetGuidForLevel(0, 3);
        public static readonly Guid RestoreId = baseIndex.GetGuidForLevel(0, 4);

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditActionDefinition>().HasData(
                new []
                {
                    new SeedRecord<AuditActionDefinition>(CreateId, AuditActionCodes.Create),
                    new SeedRecord<AuditActionDefinition>(UpdateId, AuditActionCodes.Update),
                    new SeedRecord<AuditActionDefinition>(DeleteId, AuditActionCodes.Delete),
                    new SeedRecord<AuditActionDefinition>(RestoreId, AuditActionCodes.Restore)
                }.CreateListDefinition(pathKey)
            );
        }
    }
}
