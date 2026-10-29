using IKP.Domain.Entities.Files;
using IKP.Domain.Entities.Reports.Lookups;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Reports;

public sealed class ReportDataTypeConfiguration : IEntityTypeConfiguration<ReportDataType>
{
    public void Configure(EntityTypeBuilder<ReportDataType> builder)
    {
        builder.ToTable("ReportDataTypies", DatabaseSchemas.ReportsLookup);

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

        builder.HasOne(x => x.File).WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.SetNull);
    }
}
