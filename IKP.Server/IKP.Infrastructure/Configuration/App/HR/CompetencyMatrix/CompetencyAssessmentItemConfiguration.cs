using IKP.Domain.Entities.App.HR.CompetencyMatrix;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.CompetencyMatrix
{
    public sealed class CompetencyAssessmentItemConfiguration : IEntityTypeConfiguration<CompetencyAssessmentItem>
    {
        public void Configure(EntityTypeBuilder<CompetencyAssessmentItem> builder)
        {
            builder.ToTable("CompetencyAssessmentItems", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AssessmentId).IsRequired();
            builder.Property(x => x.MatrixItemId).IsRequired();
            builder.Property(x => x.ReferenceAssessmentValueId);
            builder.Property(x => x.ManagerAssessmentValueId);
            builder.Property(x => x.EmployeeAssessmentValueId);


            builder.HasIndex(x => new { x.AssessmentId, x.MatrixItemId }).IsUnique();

            builder.HasOne(x => x.Assessment).WithMany(x => x.Items).HasForeignKey(x => x.AssessmentId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.MatrixItem).WithMany().HasForeignKey(x => x.MatrixItemId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ReferenceAssessmentValue).WithMany().HasForeignKey(x => x.ReferenceAssessmentValueId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.ManagerAssessmentValue).WithMany().HasForeignKey(x => x.ManagerAssessmentValueId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.EmployeeAssessmentValue).WithMany().HasForeignKey(x => x.EmployeeAssessmentValueId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
