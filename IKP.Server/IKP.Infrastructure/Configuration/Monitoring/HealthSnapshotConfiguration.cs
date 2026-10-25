using IKP.Domain.Entities.Monitoring;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Monitoring;

public sealed class HealthSnapshotConfiguration : IEntityTypeConfiguration<HealthSnapshot>
{
    public void Configure(EntityTypeBuilder<HealthSnapshot> builder)
    {
        builder.ToTable("HealthSnapshots", DatabaseSchemas.Monitoring); 
        
        builder.HasKey(x=>x.Id);

        builder.Property(x=>x.Component).IsRequired().HasMaxLength(200); 
        builder.Property(x=>x.StatusCode).IsRequired().HasMaxLength(50); 
        builder.Property(x=>x.DurationMilliseconds); 
        builder.Property(x=>x.Details).HasMaxLength(10000); 
        builder.Property(x=>x.CheckedAt).IsRequired();
        
        builder.HasIndex(x=>new{x.Component,x.CheckedAt});
    }
}
