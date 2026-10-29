using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IKP.Infrastructure.Configuration.App.HR.CompetencyMatrix
{
    public sealed class CompetencyMatrixConfiguration : IEntityTypeConfiguration<Domain.Entities.App.HR.CompetencyMatrix.CompetencyMatrix>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.App.HR.CompetencyMatrix.CompetencyMatrix> builder)
        {
            builder.ToTable("CompetencyMatrices", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(300);
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasIndex(x => new { x.IsActive, x.IsDeleted });
        }
    }
}
