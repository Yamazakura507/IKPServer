using IKP.Domain.Entities.Logs;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Logs
{
    public class AuditActionDefinitionConfiguration : IEntityTypeConfiguration<AuditActionDefinition>
    {
        public void Configure(EntityTypeBuilder<AuditActionDefinition> builder)
        {
            builder.ToTable("AuditActions", DatabaseSchemas.Logs);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
            builder.Property(x => x.NameKey).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DescriptionKey).HasMaxLength(500);
            builder.Property(x => x.IsSystem).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.FileId);

            builder.HasIndex(x => x.Code).IsUnique();
            builder.HasIndex(x => new
            {
                x.IsActive,
                x.SortOrder
            });
        }
    }
}
