using IKP.Domain.Common.Audit;
using IKP.Domain.Entities.Logs;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds
{
    public static class AuditActionSeed
    {
        public static readonly Guid CreateId = Guid.Parse("10000000-0000-0000-0000-000000000001");
        public static readonly Guid UpdateId = Guid.Parse("10000000-0000-0000-0000-000000000002");
        public static readonly Guid DeleteId = Guid.Parse("10000000-0000-0000-0000-000000000003");
        public static readonly Guid RestoreId = Guid.Parse("10000000-0000-0000-0000-000000000004");

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditActionDefinition>().HasData(
                new AuditActionDefinition
                {
                    Id = CreateId,
                    Code = AuditActionCodes.Create,
                    NameKey = "Audit.Action.Create",
                    DescriptionKey = "Audit.Action.Create.Description",
                    IsSystem = true,
                    IsActive = true,
                    SortOrder = 10,
                    FileId = null
                },
                new AuditActionDefinition
                {
                    Id = UpdateId,
                    Code = AuditActionCodes.Update,
                    NameKey = "Audit.Action.Update",
                    DescriptionKey = "Audit.Action.Update.Description",
                    IsSystem = true,
                    IsActive = true,
                    SortOrder = 20,
                    FileId = null
                },
                new AuditActionDefinition
                {
                    Id = DeleteId,
                    Code = AuditActionCodes.Delete,
                    NameKey = "Audit.Action.Delete",
                    DescriptionKey = "Audit.Action.Delete.Description",
                    IsSystem = true,
                    IsActive = true,
                    SortOrder = 30,
                    FileId = null
                },
                new AuditActionDefinition
                {
                    Id = RestoreId,
                    Code = AuditActionCodes.Restore,
                    NameKey = "Audit.Action.Restore",
                    DescriptionKey = "Audit.Action.Restore.Description",
                    IsSystem = true,
                    IsActive = true,
                    SortOrder = 40,
                    FileId = null
                }
            );
        }
    }
}
