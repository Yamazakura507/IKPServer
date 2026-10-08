using IKP.Domain.Entities.Logs;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Logs
{
    public class AuditLogChangeConfiguration : IEntityTypeConfiguration<AuditLogChange>
    {
        public void Configure(EntityTypeBuilder<AuditLogChange> builder)
        {
            builder.ToTable("AuditLogChanges", DatabaseSchemas.Logs);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PropertyName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.PropertyType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.OldValue).HasColumnType("jsonb");
            builder.Property(x => x.NewValue).HasColumnType("jsonb");

            builder.HasIndex(x => x.AuditLogId);
        }
    }
}
