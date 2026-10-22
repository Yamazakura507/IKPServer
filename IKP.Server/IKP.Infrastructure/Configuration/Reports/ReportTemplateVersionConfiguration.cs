using IKP.Domain.Entities.Files;
using IKP.Domain.Entities.Reports;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Reports;

public sealed class ReportTemplateVersionConfiguration : IEntityTypeConfiguration<ReportTemplateVersion>
{
    public void Configure(EntityTypeBuilder<ReportTemplateVersion> builder)
    {
        builder.ToTable("ReportTemplateVersions", DatabaseSchemas.Reports);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ReportDefinitionId).IsRequired();
        builder.Property(x => x.FileId).IsRequired();
        builder.Property(x => x.Version).IsRequired();
        builder.Property(x => x.ContentHash).HasMaxLength(128);
        builder.Property(x => x.Size);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsPublished).IsRequired();
        builder.Property(x => x.PublishedAt);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);

        builder.HasIndex(x => new { x.ReportDefinitionId, x.Version }).IsUnique();
        builder.HasIndex(x => new { x.ReportDefinitionId, x.IsPublished, x.PublishedAt });
        builder.HasIndex(x => x.FileId).IsUnique();

        builder.HasOne(x => x.ReportDefinition).WithMany().HasForeignKey(x => x.ReportDefinitionId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<StoredFile>().WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.Restrict);
    }
}
