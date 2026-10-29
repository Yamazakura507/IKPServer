using IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Probation;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.DevelopmentAndAttestation.Probation
{
    public sealed class ProbationTaskConfiguration : IEntityTypeConfiguration<ProbationTask>
    {
        public void Configure(EntityTypeBuilder<ProbationTask> builder)
        {
            builder.ToTable("ProbationTasks", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PlanId).IsRequired();
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Description).HasMaxLength(4000);
            builder.Property(x => x.Deadline);
            builder.Property(x => x.CompletionPercent).IsRequired().HasPrecision(6, 2);
            builder.Property(x => x.WeeklyReport);
            builder.Property(x => x.Comment);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasIndex(x => new { x.PlanId, x.SortOrder });

            builder.HasOne(x => x.Plan).WithMany(x => x.Tasks).HasForeignKey(x => x.PlanId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
