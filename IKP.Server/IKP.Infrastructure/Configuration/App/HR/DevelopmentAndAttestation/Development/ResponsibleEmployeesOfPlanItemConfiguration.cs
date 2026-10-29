using IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Development;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.DevelopmentAndAttestation.Development
{
    public sealed class ResponsibleEmployeesOfPlanItemConfiguration : IEntityTypeConfiguration<ResponsibleEmployeesOfPlanItem>
    {
        public void Configure(EntityTypeBuilder<ResponsibleEmployeesOfPlanItem> builder)
        {
            builder.ToTable("ResponsibleEmployeesOfPlanItem", DatabaseSchemas.App);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PlanItemId).IsRequired();
            builder.Property(x => x.ResponsibleEmployeeId).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.DeletedBy);

            builder.HasOne(x => x.PlanItem).WithMany(x => x.ResponsibleEmployees).HasForeignKey(x => x.PlanItemId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.ResponsibleEmployee).WithMany().HasForeignKey(x => x.ResponsibleEmployeeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
