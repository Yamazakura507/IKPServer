using IKP.Domain.Entities.System;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.System;

public sealed class BackgroundJobConfiguration : IEntityTypeConfiguration<BackgroundJob>
{
    public void Configure(EntityTypeBuilder<BackgroundJob> builder)
    {
        builder.ToTable("BackgroundJobs", DatabaseSchemas.System);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.JobType).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Payload).HasMaxLength(20000);
        builder.Property(x => x.StatusCode).IsRequired().HasMaxLength(50);
        builder.Property(x => x.AttemptCount).IsRequired();
        builder.Property(x => x.ErrorMessage).HasMaxLength(10000);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.HasIndex(x => new { x.StatusCode, x.NextAttemptAt });
        builder.HasIndex(x => new { x.JobType, x.CreatedAt });
    }
}
