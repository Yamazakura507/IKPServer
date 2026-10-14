using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Areas
{
    /// <summary>
    /// Configures diagnostic area resolution evidence records.
    /// </summary>
    public class DiagnosticAreaResolutionEvidenceConfiguration : IEntityTypeConfiguration<DiagnosticAreaResolutionEvidence>
    {
        /// <summary>
        /// Configures the diagnostic area resolution evidence entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<DiagnosticAreaResolutionEvidence> builder)
        {
            builder.ToTable("DiagnosticAreaResolutionEvidences", DatabaseSchemas.DiagnosticsCore);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DiagnosticAreaResolutionId).IsRequired();
            builder.Property(x => x.MappingId);
            builder.Property(x => x.RuleId).IsRequired();
            builder.Property(x => x.Pattern).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.MatchedValue).IsRequired().HasMaxLength(4000);
            builder.Property(x => x.Priority).IsRequired();
            builder.Property(x => x.Confidence).IsRequired().HasPrecision(5, 2);
            builder.Property(x => x.Contribution).IsRequired().HasPrecision(5, 2);

            builder.ToTable(table => table.HasCheckConstraint("CK_DiagnosticAreaResolutionEvidences_Priority", "\"Priority\" >= 0 AND \"Priority\" <= 100"));
            builder.ToTable(table => table.HasCheckConstraint("CK_DiagnosticAreaResolutionEvidences_Confidence", "\"Confidence\" >= 0 AND \"Confidence\" <= 100"));
            builder.ToTable(table => table.HasCheckConstraint("CK_DiagnosticAreaResolutionEvidences_Contribution", "\"Contribution\" >= 0 AND \"Contribution\" <= 100"));

            builder.HasOne(x => x.DiagnosticAreaResolution).WithMany(x => x.Evidences).HasForeignKey(x => x.DiagnosticAreaResolutionId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Rule).WithMany(x => x.Evidences).HasForeignKey(x => x.RuleId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Mapping).WithMany().HasForeignKey(x => x.MappingId).OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(x => x.DiagnosticAreaResolutionId);
            builder.HasIndex(x => x.MappingId);
            builder.HasIndex(x => x.RuleId);
        }
    }
}
