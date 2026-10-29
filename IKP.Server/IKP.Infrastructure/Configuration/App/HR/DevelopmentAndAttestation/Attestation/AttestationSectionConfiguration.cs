using IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Attestation;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.DevelopmentAndAttestation.Attestation
{
    public sealed class AttestationSectionConfiguration : IEntityTypeConfiguration<AttestationSection>
    {
        public void Configure(EntityTypeBuilder<AttestationSection> builder)
        {
            builder.ToTable("AttestationSections", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AttestationId).IsRequired();
            builder.Property(x => x.AttestationSectionTypeId).IsRequired();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(500);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasIndex(x => new { x.AttestationId, x.SortOrder });

            builder.HasOne(x => x.Attestation).WithMany(x => x.Sections).HasForeignKey(x => x.AttestationId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.SectionType).WithMany().HasForeignKey(x => x.AttestationSectionTypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
