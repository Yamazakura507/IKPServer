using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Definitions
{
    /// <summary>
    /// Configures the diagnostic definition group entity.
    /// </summary>
    public class DiagnosticDefinitionGroupConfiguration : IEntityTypeConfiguration<DiagnosticDefinitionGroup>
    {
        /// <summary>
        /// Configures the diagnostic definition group entity.
        /// </summary>
        /// <param name="builder">Entity type builder.</param>
        public void Configure(EntityTypeBuilder<DiagnosticDefinitionGroup> builder)
        {
            builder.ToTable("DiagnosticDefinitionGroups", DatabaseSchemas.DiagnosticsLookup);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
            builder.Property(x => x.NameKey).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DescriptionKey).HasMaxLength(500);
            builder.Property(x => x.FileId);
            builder.HasOne(x => x.File).WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(x => x.FileId);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsSystem).IsRequired();

            builder.HasIndex(x => x.Code).IsUnique();
            builder.HasIndex(x => new
            {
                x.IsActive,
                x.SortOrder
            });
        }
    }
}
