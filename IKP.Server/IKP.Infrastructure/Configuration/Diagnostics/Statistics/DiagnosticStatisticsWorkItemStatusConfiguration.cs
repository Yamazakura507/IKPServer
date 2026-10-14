using IKP.Domain.Entities.Diagnostics.Statistics;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Statistics
{
    /// <summary>
    /// Configures diagnostic statistics work item status lookup entries.
    /// </summary>
    public class DiagnosticStatisticsWorkItemStatusConfiguration : IEntityTypeConfiguration<DiagnosticStatisticsWorkItemStatus>
    {
        /// <summary>
        /// Configures the diagnostic statistics work item status lookup entity.
        /// </summary>
        /// <param name="builder">Entity type builder.</param>
        public void Configure(EntityTypeBuilder<DiagnosticStatisticsWorkItemStatus> builder)
        {
            builder.ToTable("DiagnosticStatisticsWorkItemStatuses", DatabaseSchemas.DiagnosticsLookup);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
            builder.Property(x => x.NameKey).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DescriptionKey).HasMaxLength(500);
            builder.Property(x => x.FileId);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsSystem).IsRequired();

            builder.HasIndex(x => x.Code).IsUnique();
            builder.HasIndex(x => new { x.IsActive, x.SortOrder });
        }
    }
}
