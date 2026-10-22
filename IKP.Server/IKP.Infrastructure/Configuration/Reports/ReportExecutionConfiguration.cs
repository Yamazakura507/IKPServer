using IKP.Domain.Entities.Reports;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Reports;

public sealed class ReportExecutionConfiguration : IEntityTypeConfiguration<ReportExecution>
{
    public void Configure(EntityTypeBuilder<ReportExecution> builder)
    {
        builder.ToTable("ReportExecutions", DatabaseSchemas.Reports);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ReportDefinitionId).IsRequired();
        builder.Property(x => x.TemplateVersionId);
        builder.Property(x => x.RequestedBy);
        builder.Property(x => x.StatusCodeId).IsRequired().HasMaxLength(50);
        builder.Property(x => x.OutputFileId).HasMaxLength(100);
        builder.Property(x => x.OutputContentType).HasMaxLength(200);
        builder.Property(x => x.ErrorMessage).HasMaxLength(10000);
        builder.Property(x => x.StartedAt).IsRequired();
        builder.Property(x => x.CompletedAt);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);

        builder.HasIndex(x => new { x.ReportDefinitionId, x.StartedAt });
        builder.HasIndex(x => new { x.StatusCodeId, x.StartedAt });

        builder.HasOne(x => x.ReportDefinition).WithMany().HasForeignKey(x => x.ReportDefinitionId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ReportExecutionStatus).WithMany().HasForeignKey(x => x.StatusCodeId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.TemplateVersion).WithMany().HasForeignKey(x => x.TemplateVersionId).OnDelete(DeleteBehavior.SetNull);
    }
}
