using IKP.Domain.Entities.App.HR.CompetencyMatrix;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.CompetencyMatrix
{
    public sealed class CompetencyMatrixTargetConfiguration : IEntityTypeConfiguration<CompetencyMatrixTarget>
    {
        public void Configure(EntityTypeBuilder<CompetencyMatrixTarget> builder)
        {
            builder.ToTable("CompetencyMatrixTargets", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MatrixItemId).IsRequired();
            builder.Property(x => x.PositionId).IsRequired();
            builder.Property(x => x.GradeId);
            builder.Property(x => x.ReferenceValue).HasPrecision(6, 2);
            builder.Property(x => x.ReferenceText).HasMaxLength(1000);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasIndex(x => new { x.MatrixItemId, x.PositionId, x.GradeId }).IsUnique();

            builder.HasOne(x => x.MatrixItem).WithMany(x => x.Targets).HasForeignKey(x => x.MatrixItemId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Position).WithMany().HasForeignKey(x => x.PositionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Grade).WithMany().HasForeignKey(x => x.GradeId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
