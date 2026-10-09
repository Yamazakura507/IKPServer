using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Areas
{
    public class DiagnosticAreaConfiguration : IEntityTypeConfiguration<DiagnosticArea>
    {
        public void Configure(EntityTypeBuilder<DiagnosticArea> builder)
        {
            builder.ToTable("DiagnosticAreas", DatabaseSchemas.Diagnostics);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
            builder.Property(x => x.NameKey).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DescriptionKey).HasMaxLength(500);
            builder.Property(x => x.ImportanceScore).IsRequired().HasPrecision(5, 2);
            builder.Property(x => x.IsActive).IsRequired();

            builder.HasIndex(x => x.Code).IsUnique();

            builder.HasIndex(x => new
            {
                x.IsActive,
                x.ImportanceScore
            });
        }
    }
}
