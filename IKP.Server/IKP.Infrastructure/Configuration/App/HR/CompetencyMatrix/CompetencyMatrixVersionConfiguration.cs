using IKP.Domain.Entities.App.HR.CompetencyMatrix;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.CompetencyMatrix
{
    public sealed class CompetencyMatrixVersionConfiguration : IEntityTypeConfiguration<CompetencyMatrixVersion>
    {
        public void Configure(EntityTypeBuilder<CompetencyMatrixVersion> builder)
        {
            builder.ToTable("CompetencyMatrixVersions", DatabaseSchemas.App);

            builder.HasKey(x => x.Id); builder.Property(x => x.VersionNumber).IsRequired();

            builder.Property(x => x.MatrixId).IsRequired();
            builder.Property(x => x.VersionNumber).IsRequired();
            builder.Property(x => x.ChangeNotes);
            builder.Property(x => x.EffectiveFrom).IsRequired();
            builder.Property(x => x.EffectiveTo);
            builder.Property(x => x.IsPublished);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasIndex(x => new { x.MatrixId, x.VersionNumber }).IsUnique();

            builder.HasOne(x => x.Matrix).WithMany(x => x.Versions).HasForeignKey(x => x.MatrixId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
