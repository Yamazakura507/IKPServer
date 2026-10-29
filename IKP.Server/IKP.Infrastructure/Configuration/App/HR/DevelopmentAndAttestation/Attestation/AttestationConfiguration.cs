using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.DevelopmentAndAttestation.Attestation
{
    public sealed class AttestationConfiguration : IEntityTypeConfiguration<Domain.Entities.App.HR.DevelopmentAndAttestation.Attestation.Attestation>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.App.HR.DevelopmentAndAttestation.Attestation.Attestation> builder)
        {
            builder.ToTable("Attestations", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmployeeId).IsRequired();
            builder.Property(x => x.ManagerEmployeeId);
            builder.Property(x => x.MatrixAssessmentId).IsRequired();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(500);
            builder.Property(x => x.PlannedDate);
            builder.Property(x => x.CompletedDate);
            builder.Property(x => x.FinalScore).HasPrecision(6, 2);
            builder.Property(x => x.FinalComment);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ManagerEmployee).WithMany().HasForeignKey(x => x.ManagerEmployeeId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.MatrixAssessment).WithMany().HasForeignKey(x => x.MatrixAssessmentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
