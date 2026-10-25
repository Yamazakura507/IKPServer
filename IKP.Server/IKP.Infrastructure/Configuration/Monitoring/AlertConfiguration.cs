using IKP.Domain.Entities.Monitoring;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Monitoring;
public sealed class AlertConfiguration : IEntityTypeConfiguration<Alert>
{
    public void Configure(EntityTypeBuilder<Alert> builder)
    {
        builder.ToTable("Alerts", DatabaseSchemas.Monitoring); 
        
        builder.HasKey(x => x.Id);
  
        builder.Property(x=>x.AlertRuleId).IsRequired(); 
        builder.Property(x=>x.StatusCode).IsRequired().HasMaxLength(50); 
        builder.Property(x=>x.Message).IsRequired().HasMaxLength(10000);
        builder.Property(x => x.TriggeredAt).IsRequired();
        builder.Property(x => x.ResolvedAt);

        builder.HasIndex(x=>new{x.StatusCode, x.TriggeredAt}); 
        
        builder.HasOne(x=>x.AlertRule).WithMany().HasForeignKey(x=>x.AlertRuleId).OnDelete(DeleteBehavior.Cascade);
    }
}
