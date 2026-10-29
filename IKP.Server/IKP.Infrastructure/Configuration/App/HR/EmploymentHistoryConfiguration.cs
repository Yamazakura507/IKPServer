using IKP.Domain.Entities.App.HR;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR;

public sealed class EmploymentHistoryConfiguration : IEntityTypeConfiguration<EmploymentHistory> 
{ 
    public void Configure(EntityTypeBuilder<EmploymentHistory> builder) 
    { 
        builder.ToTable("EmploymentHistory",DatabaseSchemas.App); 

        builder.HasKey(x=>x.Id);
        
        builder.Property(x=>x.EmployeeId).IsRequired(); 
        builder.Property(x=>x.DepartmentId).IsRequired(); 
        builder.Property(x=>x.ManagerEmployeeId); 
        builder.Property(x=>x.RecordTypeId).IsRequired(); 
        builder.Property(x=>x.GradeId); 
        builder.Property(x=>x.PositionId).IsRequired(); 
        builder.Property(x=>x.ValidFrom).IsRequired(); 
        builder.Property(x=>x.Reason).HasMaxLength(1000); 
        builder.Property(x=>x.Notes).HasMaxLength(4000);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);

        builder.HasIndex(x=>new{x.EmployeeId,x.ValidFrom}); 
        builder.HasIndex(x=>new{x.EmployeeId,x.ValidTo}); 
        
        builder.HasOne(x=>x.Employee).WithMany(x=>x.EmploymentHistory).HasForeignKey(x=>x.EmployeeId).OnDelete(DeleteBehavior.Cascade); 
        builder.HasOne(x=>x.Department).WithMany().HasForeignKey(x=>x.DepartmentId).OnDelete(DeleteBehavior.Restrict); 
        builder.HasOne(x=>x.Position).WithMany().HasForeignKey(x=>x.PositionId).OnDelete(DeleteBehavior.Restrict); 
        builder.HasOne(x=>x.Grade).WithMany().HasForeignKey(x=>x.GradeId).OnDelete(DeleteBehavior.SetNull); 
        builder.HasOne(x=>x.ManagerEmployee).WithMany().HasForeignKey(x=>x.ManagerEmployeeId).OnDelete(DeleteBehavior.SetNull); 
        builder.HasOne(x=>x.RecordType).WithMany().HasForeignKey(x=>x.RecordTypeId).OnDelete(DeleteBehavior.Restrict); 
    } 
}