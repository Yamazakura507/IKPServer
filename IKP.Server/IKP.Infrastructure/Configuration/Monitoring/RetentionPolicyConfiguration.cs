using IKP.Domain.Entities.Monitoring;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Monitoring;

public sealed class RetentionPolicyConfiguration : IEntityTypeConfiguration<RetentionPolicy>
{
 public void Configure(EntityTypeBuilder<RetentionPolicy> builder)
 {
        builder.ToTable("RetentionPolicys", DatabaseSchemas.Monitoring); 
    
        builder.HasKey(x=>x.Id);

        builder.Property(x=>x.AreaCode).IsRequired().HasMaxLength(100); 
        builder.Property(x=>x.RetentionDays).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.LastRunAt);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);


        builder.HasIndex(x => x.AreaCode).IsUnique();
 }
}
