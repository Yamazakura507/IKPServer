using IKP.Domain.Entities.App.HR.CompetencyMatrix;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.CompetencyMatrix
{
    public sealed class CompetencyMatrixSectionConfiguration : IEntityTypeConfiguration<CompetencyMatrixSection>
    {
        public void Configure(EntityTypeBuilder<CompetencyMatrixSection> builder)
        {
            builder.ToTable("CompetencyMatrixSections", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MatrixVersionId).IsRequired();
            builder.Property(x => x.SectionTypeId).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(300);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasIndex(x => new { x.MatrixVersionId, x.SortOrder });

            builder.HasOne(x => x.MatrixVersion).WithMany(x => x.Sections).HasForeignKey(x => x.MatrixVersionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.SectionType).WithMany().HasForeignKey(x => x.SectionTypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
