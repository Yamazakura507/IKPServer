using IKP.Domain.Entities.App.HR;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR;

public sealed class EmployeeContactConfiguration : IEntityTypeConfiguration<EmployeeContact> 
{
    public void Configure(EntityTypeBuilder<EmployeeContact> builder)
    { 
        builder.ToTable("EmployeeContacts",DatabaseSchemas.App); 
        
        builder.HasKey(x=>x.Id); 
        
        builder.Property(x=>x.EmployeeId).IsRequired(); 
        builder.Property(x=>x.EmployeeContactTypeId).IsRequired(); 
        builder.Property(x=>x.Value).IsRequired().HasMaxLength(500);
        builder.Property(x=>x.Label).HasMaxLength(100); 
        builder.Property(x=>x.IsPrimary).IsRequired(); 
        builder.Property(x=>x.IsVerified).IsRequired();
        builder.Property(x=>x.VerifiedAt);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasIndex(x=>new{x.EmployeeId,x.EmployeeContactTypeId,x.IsPrimary}); 
        
        builder.HasOne(x=>x.Employee).WithMany(x=>x.Contacts).HasForeignKey(x=>x.EmployeeId).OnDelete(DeleteBehavior.Cascade); 
        builder.HasOne(x=>x.Type).WithMany().HasForeignKey(x=>x.EmployeeContactTypeId).OnDelete(DeleteBehavior.Restrict); 
    } 
}