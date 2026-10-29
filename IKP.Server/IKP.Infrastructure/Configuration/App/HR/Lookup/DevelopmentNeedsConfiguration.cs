using IKP.Domain.Entities.App.HR.Lookup;
using IKP.Domain.Entities.Files;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.Lookup; 

public sealed class DevelopmentNeedsConfiguration : IEntityTypeConfiguration<DevelopmentNeeds> 
{ 
    public void Configure(EntityTypeBuilder<DevelopmentNeeds> builder) 
    { 
        builder.ToTable("DevelopmentNeeds", DatabaseSchemas.AppLookup); 
        
        builder.HasKey(x=>x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.TypeId);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasOne(x => x.DevelopmentItemType).WithMany().HasForeignKey(x => x.TypeId).OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.TypeId);
        builder.HasIndex(x => new { x.IsDeleted });
    } 
}