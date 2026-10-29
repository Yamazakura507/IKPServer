using IKP.Domain.Entities.App.HR.CompetencyMatrix;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.CompetencyMatrix
{
    public sealed class CompetencyAssessmentConfiguration : IEntityTypeConfiguration<CompetencyAssessment>
    {
        public void Configure(EntityTypeBuilder<CompetencyAssessment> builder)
        {
            builder.ToTable("CompetencyAssessments", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmployeeId).IsRequired();
            builder.Property(x => x.AssessorEmployeeId).IsRequired();
            builder.Property(x => x.MatrixVersionId).IsRequired();
            builder.Property(x => x.AssessmentTypeId).IsRequired();
            builder.Property(x => x.AssessmentDate).IsRequired();
            builder.Property(x => x.Notes);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasIndex(x => new { x.EmployeeId, x.AssessmentDate });

            builder.HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.AssessorEmployee).WithMany().HasForeignKey(x => x.AssessorEmployeeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.MatrixVersion).WithMany().HasForeignKey(x => x.MatrixVersionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.AssessmentType).WithMany().HasForeignKey(x => x.AssessmentTypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
