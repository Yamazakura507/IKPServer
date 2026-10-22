using IKP.Domain.Entities.Reports.Lookups;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Reports;

public sealed class ReportParameterConfiguration : IEntityTypeConfiguration<ReportParameter>
{
    public void Configure(EntityTypeBuilder<ReportParameter> builder)
    {
        builder.ToTable("ReportParameters", DatabaseSchemas.Reports);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ReportDefinitionId).IsRequired();
        builder.Property(x => x.DataTypeCodeId).IsRequired();
        builder.Property(x => x.IsRequired).IsRequired();
        builder.Property(x => x.DefaultValue).HasMaxLength(4000);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
        builder.Property(x => x.NameKey).IsRequired().HasMaxLength(200);
        builder.Property(x => x.DescriptionKey).HasMaxLength(500);
        builder.Property(x => x.FileId);
        builder.Property(x => x.SortOrder).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsSystem).IsRequired();

        builder.HasIndex(x => new { x.ReportDefinitionId, x.Code }).IsUnique();
        builder.HasIndex(x => new { x.ReportDefinitionId, x.IsActive, x.SortOrder });

        builder.HasOne(x => x.ReportDefinition).WithMany().HasForeignKey(x => x.ReportDefinitionId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.ReportDataType).WithMany().HasForeignKey(x => x.DataTypeCodeId).OnDelete(DeleteBehavior.Cascade);
    }
}
