using IKP.Domain.Entities.Logs;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Logs
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs", DatabaseSchemas.Logs);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EntitySchema).IsRequired().HasMaxLength(100);
            builder.Property(x => x.EntityType).IsRequired().HasMaxLength(200);
            builder.Property(x => x.EntityId).IsRequired();
            builder.Property(x => x.ActionId).IsRequired();
            builder.Property(x => x.UserId);
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasOne(x => x.Action).WithMany().HasForeignKey(x => x.ActionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Changes).WithOne(x => x.AuditLog).HasForeignKey(x => x.AuditLogId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new
            {
                x.EntitySchema,
                x.EntityType,
                x.EntityId
            });
            builder.HasIndex(x => x.CreatedAt);
            builder.HasIndex(x => x.ActionId);
        }
    }
}
