using IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Development;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.DevelopmentAndAttestation.Development
{
    public sealed class DevelopmentPlanConfiguration : IEntityTypeConfiguration<DevelopmentPlan>
    {
        public void Configure(EntityTypeBuilder<DevelopmentPlan> builder)
        {
            builder.ToTable("DevelopmentPlans", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmployeeId).IsRequired();
            builder.Property(x => x.ManagerEmployeeId).IsRequired();
            builder.Property(x => x.TypeId).IsRequired();
            builder.Property(x => x.PlanningDate).IsRequired();
            builder.Property(x => x.PeriodStart);
            builder.Property(x => x.PeriodEnd);
            builder.Property(x => x.ManagerEffectiveness).HasPrecision(6, 2);
            builder.Property(x => x.EmployeeEffectiveness).HasPrecision(6, 2);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasOne(x => x.Type).WithMany().HasForeignKey(x => x.TypeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ManagerEmployee).WithMany().HasForeignKey(x => x.ManagerEmployeeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
