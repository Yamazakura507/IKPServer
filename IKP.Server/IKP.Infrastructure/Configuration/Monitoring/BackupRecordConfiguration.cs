using IKP.Domain.Entities.Monitoring;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Monitoring;

public sealed class BackupRecordConfiguration : IEntityTypeConfiguration<BackupRecord>
{
    public void Configure(EntityTypeBuilder<BackupRecord> builder)
    {
        builder.ToTable("BackupRecords", DatabaseSchemas.Monitoring); 
        
        builder.HasKey(x=>x.Id);

        builder.Property(x=>x.BackupType).IsRequired().HasMaxLength(100); 
        builder.Property(x=>x.StatusCode).IsRequired().HasMaxLength(50); 
        builder.Property(x=>x.StorageKey).HasMaxLength(1000); 
        builder.Property(x=>x.Size); 
        builder.Property(x=>x.ContentHash).HasMaxLength(128); 
        builder.Property(x=>x.ErrorMessage).HasMaxLength(10000);
        builder.Property(x => x.StartedAt).IsRequired();
        builder.Property(x => x.CompletedAt);

        builder.HasIndex(x=>x.StartedAt);
    }
}
