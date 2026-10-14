using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Definitions
{
    /// <summary>
    /// Configures diagnostic area resolution mappings.
    /// </summary>
    public class DiagnosticAreaResolutionMappingConfiguration : IEntityTypeConfiguration<DiagnosticAreaResolutionMapping>
    {
        /// <summary>
        /// Configures the diagnostic area resolution mapping entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<DiagnosticAreaResolutionMapping> builder)
        {
            builder.ToTable("DiagnosticAreaResolutionMappings", DatabaseSchemas.DiagnosticsLookup);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DiagnosticAreaId).IsRequired();
            builder.Property(x => x.RuleId).IsRequired();
            builder.Property(x => x.Pattern).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.Priority).IsRequired();
            builder.Property(x => x.Confidence).IsRequired().HasPrecision(5, 2);
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsSystem).IsRequired();

            builder.ToTable(table => table.HasCheckConstraint("CK_DiagnosticAreaResolutionMappings_Priority", "\"Priority\" >= 0 AND \"Priority\" <= 100"));
            builder.ToTable(table => table.HasCheckConstraint("CK_DiagnosticAreaResolutionMappings_Confidence", "\"Confidence\" >= 0 AND \"Confidence\" <= 100"));

            builder.HasOne(x => x.DiagnosticArea).WithMany(x => x.ResolutionMappings).HasForeignKey(x => x.DiagnosticAreaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Rule).WithMany(x => x.Mappings).HasForeignKey(x => x.RuleId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.DiagnosticAreaId, x.RuleId });
            builder.HasIndex(x => new { x.IsActive, x.RuleId });
        }
    }
}
