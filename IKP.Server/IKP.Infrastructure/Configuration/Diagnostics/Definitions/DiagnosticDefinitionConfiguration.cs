using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Diagnostics.Definitions
{
    /// <summary>
    /// Configures the diagnostic definition entity.
    /// </summary>
    public class DiagnosticDefinitionConfiguration : IEntityTypeConfiguration<DiagnosticDefinition>
    {
        /// <summary>
        /// Configures the diagnostic definition entity.
        /// </summary>
        /// <param name="builder">Entity type builder.</param>
        public void Configure(EntityTypeBuilder<DiagnosticDefinition> builder)
        {
            builder.ToTable("DiagnosticDefinitions", DatabaseSchemas.Diagnostics);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.GroupId).IsRequired();

            builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
            builder.Property(x => x.NameKey).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DescriptionKey).HasMaxLength(500);
            builder.Property(x => x.FileId);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsSystem).IsRequired();

            builder.HasOne(x => x.Group).WithMany(x => x.Definitions).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new
            {
                x.GroupId,
                x.Code
            }).IsUnique();
            builder.HasIndex(x => new
            {
                x.GroupId,
                x.IsActive,
                x.SortOrder
            });
        }
    }
}
