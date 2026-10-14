using IKP.Domain.Entities.Diagnostics.Statistics;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Statistics
{
    /// <summary>
    /// Configures durable diagnostic statistics work items.
    /// </summary>
    public class DiagnosticStatisticsWorkItemConfiguration : IEntityTypeConfiguration<DiagnosticStatisticsWorkItem>
    {
        /// <summary>
        /// Configures the diagnostic statistics work item entity.
        /// </summary>
        /// <param name="builder">Entity type builder.</param>
        public void Configure(EntityTypeBuilder<DiagnosticStatisticsWorkItem> builder)
        {
            builder.ToTable("DiagnosticStatisticsWorkItems", DatabaseSchemas.DiagnosticsCore);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ErrorGroupId).IsRequired();
            builder.Property(x => x.RequestedAt).IsRequired();
            builder.Property(x => x.AvailableAt).IsRequired();
            builder.Property(x => x.StartedAt);
            builder.Property(x => x.CompletedAt);
            builder.Property(x => x.AttemptCount).IsRequired();
            builder.Property(x => x.StatusId).IsRequired();
            builder.Property(x => x.LastError).HasMaxLength(4000);

            builder.HasOne(x => x.Status).WithMany(x => x.WorkItems).HasForeignKey(x => x.StatusId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ErrorGroup).WithMany().HasForeignKey(x => x.ErrorGroupId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.ErrorGroupId).IsUnique();
            builder.HasIndex(x => new { x.StatusId, x.AvailableAt });
        }
    }
}
