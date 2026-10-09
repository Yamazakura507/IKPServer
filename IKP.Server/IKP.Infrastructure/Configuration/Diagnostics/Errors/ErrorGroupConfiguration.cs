using IKP.Domain.Entities.Diagnostics.Errors;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Errors
{
    public class ErrorGroupConfiguration : IEntityTypeConfiguration<ErrorGroup>
    {
        public void Configure(EntityTypeBuilder<ErrorGroup> builder)
        {
            builder.ToTable("ErrorGroups", DatabaseSchemas.Diagnostics);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Fingerprint).IsRequired().HasMaxLength(128);
            builder.Property(x => x.FingerprintVersion).IsRequired();
            builder.Property(x => x.ApplicationId).IsRequired().HasMaxLength(100);
            builder.Property(x => x.DiagnosticAreaId);
            builder.Property(x => x.ExceptionType).IsRequired().HasMaxLength(500);
            builder.Property(x => x.ErrorCode).HasMaxLength(100);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Summary).HasMaxLength(4000);
            builder.Property(x => x.SeverityId).IsRequired();
            builder.Property(x => x.StatusId).IsRequired();
            builder.Property(x => x.FirstOccurredAt).IsRequired();
            builder.Property(x => x.LastOccurredAt).IsRequired();
            builder.Property(x => x.FirstSeenVersion).HasMaxLength(100);
            builder.Property(x => x.LastSeenVersion).HasMaxLength(100);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();

            builder.HasOne(x => x.DiagnosticArea).WithMany(x => x.ErrorGroups).HasForeignKey(x => x.DiagnosticAreaId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.SeverityDefinition).WithMany().HasForeignKey(x => x.SeverityId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.StatusDefinition).WithMany().HasForeignKey(x => x.StatusId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.Fingerprint,
                x.FingerprintVersion
            }).IsUnique();
            builder.HasIndex(x => x.SeverityId);
            builder.HasIndex(x => x.StatusId);
            builder.HasIndex(x => x.DiagnosticAreaId);
            builder.HasIndex(x => x.LastOccurredAt);
            builder.HasIndex(x => x.ApplicationId);
        }
    }
}
