using IKP.Domain.Entities.App.HR.CompetencyMatrix;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.CompetencyMatrix
{
    public sealed class CompetencyMatrixItemConfiguration : IEntityTypeConfiguration<CompetencyMatrixItem>
    {
        public void Configure(EntityTypeBuilder<CompetencyMatrixItem> builder)
        {
            builder.ToTable("CompetencyMatrixItems", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SectionId).IsRequired();
            builder.Property(x => x.ParentItemId);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(500);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(4000);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasIndex(x => new { x.IsDeleted, x.SectionId, x.ParentItemId, x.SortOrder });

            builder.HasOne(x => x.Section).WithMany(x => x.Items).HasForeignKey(x => x.SectionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.ParentItem).WithMany(x => x.Children).HasForeignKey(x => x.ParentItemId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
