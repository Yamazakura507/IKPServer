using IKP.Domain.Entities.App.HR;
using IKP.Domain.Entities.Files;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR;

public sealed class EmployeeQualificationConfiguration : IEntityTypeConfiguration<EmployeeQualification> 
{ 
    public void Configure(EntityTypeBuilder<EmployeeQualification> builder) 
    { 
        builder.ToTable("EmployeeQualifications",DatabaseSchemas.App); 
        
        builder.HasKey(x=>x.Id); 
        
        builder.Property(x=>x.EmployeeId).IsRequired(); 
        builder.Property(x=>x.Name).IsRequired().HasMaxLength(300); 
        builder.Property(x=>x.Issuer).HasMaxLength(300); 
        builder.Property(x=>x.IssuedAt); 
        builder.Property(x=>x.ExpiresAt); 
        builder.Property(x=>x.Number).HasMaxLength(100);
        builder.Property(x => x.FileId);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasIndex(x => x.EmployeeId);
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => x.FileId);

        builder.HasOne(x=>x.Employee).WithMany(x=>x.Qualifications).HasForeignKey(x=>x.EmployeeId).OnDelete(DeleteBehavior.Cascade); 
        builder.HasOne(x => x.File).WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.SetNull);
    } 
}