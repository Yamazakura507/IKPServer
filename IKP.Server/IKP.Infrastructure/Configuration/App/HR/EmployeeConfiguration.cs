using IKP.Domain.Entities.App.HR;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee> 
{
    public void Configure(EntityTypeBuilder<Employee> builder) 
    { 
        builder.ToTable("Employees", DatabaseSchemas.App); 
        
        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(x=>x.LastName).IsRequired().HasMaxLength(100); 
        builder.Property(x=>x.FamilyName).HasMaxLength(100); 
        builder.Property(x=>x.NickName).HasMaxLength(100); 
        builder.Property(x=>x.ShortName).IsRequired().HasMaxLength(150);
        builder.Property(x=>x.DateOfBirth); 
        builder.Property(x=>x.HireDate); 
        builder.Property(x=>x.UserId); 
        builder.Property(x => x.FileId);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x=>x.UserId); 
        builder.HasIndex(x=>x.FileId); 
        builder.HasIndex(x=>new{x.LastName,x.FirstName}); 
        
        builder.HasOne(x=>x.User).WithMany().HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.File).WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.SetNull);
    } 
}