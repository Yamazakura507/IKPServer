using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Areas
{
    /// <summary>
    /// Configures diagnostic area resolution records.
    /// </summary>
    public class DiagnosticAreaResolutionConfiguration : IEntityTypeConfiguration<DiagnosticAreaResolution>
    {
        /// <summary>
        /// Configures the diagnostic area resolution entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<DiagnosticAreaResolution> builder)
        {
            builder.ToTable("DiagnosticAreaResolutions", DatabaseSchemas.DiagnosticsCore);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DiagnosticAreaId);
            builder.Property(x => x.SourceId);
            builder.Property(x => x.RuleId);
            builder.Property(x => x.Confidence).IsRequired().HasPrecision(5, 2);
            builder.Property(x => x.Version).IsRequired();

            builder.ToTable(table => table.HasCheckConstraint("CK_DiagnosticAreaResolutions_Confidence", "\"Confidence\" >= 0 AND \"Confidence\" <= 100"));
            builder.ToTable(table => table.HasCheckConstraint("CK_DiagnosticAreaResolutions_Version", "\"Version\" > 0"));

            builder.HasOne(x => x.DiagnosticArea).WithMany(x => x.Resolutions).HasForeignKey(x => x.DiagnosticAreaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Source).WithMany().HasForeignKey(x => x.SourceId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Rule).WithMany().HasForeignKey(x => x.RuleId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.DiagnosticAreaId);
            builder.HasIndex(x => x.SourceId);
            builder.HasIndex(x => x.RuleId);
        }
    }
}
