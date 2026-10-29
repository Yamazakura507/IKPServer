using IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Probation;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.DevelopmentAndAttestation.Probation
{
    public sealed class ProbationPlanConfiguration : IEntityTypeConfiguration<ProbationPlan>
    {
        public void Configure(EntityTypeBuilder<ProbationPlan> builder)
        {
            builder.ToTable("ProbationPlans", DatabaseSchemas.App);

            builder.HasKey(x => x.Id); builder.Property(x => x.StartDate).IsRequired();

            builder.Property(x => x.EmployeeId).IsRequired();
            builder.Property(x => x.ManagerEmployeeId).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.Notes);
            builder.Property(x => x.FinalCompletionPercent).HasPrecision(6, 2);
            builder.Property(x => x.FinalComment);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ManagerEmployee).WithMany().HasForeignKey(x => x.ManagerEmployeeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
