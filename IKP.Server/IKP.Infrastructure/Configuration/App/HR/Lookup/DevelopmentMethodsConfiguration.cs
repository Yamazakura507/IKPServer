using IKP.Domain.Entities.App.HR.Lookup;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.App.HR.Lookup; 

public sealed class DevelopmentMethodssConfiguration : IEntityTypeConfiguration<DevelopmentMethods> 
{ 
    public void Configure(EntityTypeBuilder<DevelopmentMethods> builder) 
    { 
        builder.ToTable("DevelopmentMethods", DatabaseSchemas.AppLookup); 
        
        builder.HasKey(x=>x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.IsControl).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasIndex(x => new { x.IsDeleted });
    } 
}