using IKP.Domain.Entities.App.HR;
using IKP.Domain.Entities.Files;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR;

public sealed class EmployeeEducationConfiguration : IEntityTypeConfiguration<EmployeeEducation> 
{ 
    public void Configure(EntityTypeBuilder<EmployeeEducation> builder) 
    { 
        builder.ToTable("EmployeeEducation",DatabaseSchemas.App); 
        
        builder.HasKey(x=>x.Id); 

        builder.Property(x=>x.EmployeeId).IsRequired(); 
        builder.Property(x=>x.EducationTypeId).IsRequired(); 
        builder.Property(x=>x.Organization).IsRequired().HasMaxLength(300); 
        builder.Property(x=>x.Faculty).HasMaxLength(300); 
        builder.Property(x=>x.Speciality).HasMaxLength(300); 
        builder.Property(x=>x.Qualification).HasMaxLength(300);
        builder.Property(x=>x.GraduationYear);
        builder.Property(x=>x.BeginOfDate);
        builder.Property(x=>x.EndOfDate);
        builder.Property(x=>x.ProofOfDocumentNumber).HasMaxLength(100);
        builder.Property(x => x.FileId);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasIndex(x => new { x.EmployeeId, x.EducationTypeId });
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => x.FileId);

        builder.HasOne(x=>x.Employee).WithMany(x=>x.Education).HasForeignKey(x=>x.EmployeeId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x=>x.EducationType).WithMany().HasForeignKey(x=>x.EducationTypeId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.File).WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.SetNull);
    } 
}