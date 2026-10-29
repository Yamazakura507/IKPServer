using IKP.Domain.Entities.App.HR.Lookup;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.Lookup; 

public sealed class GradeConfiguration : IEntityTypeConfiguration<Grade> 
{ 
    public void Configure(EntityTypeBuilder<Grade> builder) 
    { 
        builder.ToTable("Grades",DatabaseSchemas.AppLookup); 
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200); 
        builder.Property(x => x.Description).HasMaxLength(1000);
        builder.Property(x => x.SortOrder).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasIndex(x => new { x.IsActive, x.IsDeleted, x.SortOrder });
    } 
}