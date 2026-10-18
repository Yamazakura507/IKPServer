    using IKP.Domain.Entities.Files;
using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Areas
{
    /// <summary>
    /// Configures the diagnostic area entity.
    /// </summary>
    public class DiagnosticAreaConfiguration : IEntityTypeConfiguration<DiagnosticArea>
    {
        /// <summary>
        /// Configures the diagnostic area entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<DiagnosticArea> builder)
        {
            builder.ToTable("DiagnosticAreas", DatabaseSchemas.DiagnosticsLookup);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired().HasMaxLength(64);
            builder.Property(x => x.NameKey).IsRequired().HasMaxLength(256);
            builder.Property(x => x.DescriptionKey).HasMaxLength(256);
            builder.Property(x => x.FileId);
        builder.HasOne<StoredFile>()
            .WithMany()
            .HasForeignKey(x => x.FileId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.FileId);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsSystem).IsRequired();
            builder.Property(x => x.ImportanceScore).IsRequired();

            builder.HasIndex(x => x.Code).IsUnique();

            builder.ToTable(table => table.HasCheckConstraint("CK_DiagnosticAreas_ImportanceScore", "\"ImportanceScore\" >= 0 AND \"ImportanceScore\" <= 100"));
        }
    }
}
