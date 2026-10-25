using IKP.Domain.Entities.Monitoring;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Monitoring;

public sealed class AlertRuleConfiguration : IEntityTypeConfiguration<AlertRule>
{
    public void Configure(EntityTypeBuilder<AlertRule> builder)
    {
        builder.ToTable("AlertRules", DatabaseSchemas.Monitoring); 

        builder.HasKey(x=>x.Id);
  
        builder.Property(x=>x.Code).IsRequired().HasMaxLength(100); 
        builder.Property(x=>x.NameKey).IsRequired().HasMaxLength(200); 
        builder.Property(x=>x.DescriptionKey).HasMaxLength(500); 
        builder.Property(x=>x.Condition).IsRequired().HasMaxLength(4000); 
        builder.Property(x=>x.SeverityCode).IsRequired().HasMaxLength(50);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasIndex(x=>x.Code).IsUnique();
    }
}
