using IKP.Domain.Entities.App.HR;
using IKP.Domain.Entities.Files;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR;

public sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department> 
{ 
    public void Configure(EntityTypeBuilder<Department> builder) 
    { 
        builder.ToTable("Departments",DatabaseSchemas.App); 
        
        builder.HasKey(x=>x.Id);

        builder.Property(x => x.HeadEmployeeId);
        builder.Property(x => x.ParentDepartmentId);
        builder.Property(x=>x.Name).IsRequired().HasMaxLength(200); builder.Property(x=>x.Description).HasMaxLength(1000); 
        builder.Property(x=>x.IsActive).IsRequired();

        builder.Property(x => x.FileId);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);
       

        builder.HasIndex(x=>new{x.ParentDepartmentId,x.IsActive,x.IsDeleted}); 
        
        builder.HasOne(x=>x.ParentDepartment).WithMany(x=>x.Children).HasForeignKey(x=>x.ParentDepartmentId).OnDelete(DeleteBehavior.Restrict); 
        builder.HasOne(x=>x.HeadEmployee).WithMany().HasForeignKey(x=>x.HeadEmployeeId).OnDelete(DeleteBehavior.SetNull); 
        builder.HasOne(x => x.File).WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.SetNull);
    } 
}