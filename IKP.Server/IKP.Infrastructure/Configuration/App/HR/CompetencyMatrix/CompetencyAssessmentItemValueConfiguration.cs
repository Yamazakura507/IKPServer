using IKP.Domain.Entities.App.HR.CompetencyMatrix;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.CompetencyMatrix
{
    public sealed class CompetencyAssessmentItemValueConfiguration : IEntityTypeConfiguration<CompetencyAssessmentItemValue>
    {
        public void Configure(EntityTypeBuilder<CompetencyAssessmentItemValue> builder)
        {
            builder.ToTable("CompetencyAssessmentItemValues", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Value).IsRequired().HasPrecision(6, 2);
            builder.Property(x => x.Comment).HasMaxLength(1000);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasIndex(x => x.IsDeleted);
        }
    }
}
