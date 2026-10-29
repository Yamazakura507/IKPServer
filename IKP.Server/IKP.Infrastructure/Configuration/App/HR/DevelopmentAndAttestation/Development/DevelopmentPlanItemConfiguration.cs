using IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Development;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.DevelopmentAndAttestation.Development
{
    public sealed class DevelopmentPlanItemConfiguration : IEntityTypeConfiguration<DevelopmentPlanItem>
    {
        public void Configure(EntityTypeBuilder<DevelopmentPlanItem> builder)
        {
            builder.ToTable("DevelopmentPlanItems", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PlanId).IsRequired();
            builder.Property(x => x.ItemTypeId).IsRequired();
            builder.Property(x => x.DevelopmentNeedId).IsRequired();
            builder.Property(x => x.DevelopmentNeedId).IsRequired();
            builder.Property(x => x.ControlMethodId);
            builder.Property(x => x.DevelopmentMethodId);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.Goal).HasMaxLength(2000);
            builder.Property(x => x.Deadline);
            builder.Property(x => x.Actions);
            builder.Property(x => x.CompletionPercent).HasPrecision(6, 2);
            builder.Property(x => x.Comments);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasIndex(x => new { x.PlanId, x.SortOrder });

            builder.HasOne(x => x.Plan).WithMany(x => x.Items).HasForeignKey(x => x.PlanId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.ItemType).WithMany().HasForeignKey(x => x.ItemTypeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.DevelopmentNeed).WithMany().HasForeignKey(x => x.DevelopmentNeedId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.DevelopmentControlMethod).WithMany().HasForeignKey(x => x.ControlMethodId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.DevelopmentMethod).WithMany().HasForeignKey(x => x.DevelopmentMethodId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
