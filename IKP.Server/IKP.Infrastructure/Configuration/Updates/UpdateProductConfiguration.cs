using IKP.Domain.Entities.Files;
using IKP.Domain.Entities.Updates.Lookups;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Updates
{
    public sealed class UpdateProductConfiguration : IEntityTypeConfiguration<UpdateProduct>
    {
        public void Configure(EntityTypeBuilder<UpdateProduct> builder)
        {
            builder.ToTable("Products", DatabaseSchemas.UpdatesLookup);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.PlatformId);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
            builder.Property(x => x.NameKey).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DescriptionKey).HasMaxLength(500);
            builder.Property(x => x.FileId);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsSystem).IsRequired();

            builder.HasIndex(x => x.FileId);
            builder.HasIndex(x => x.Code).IsUnique();
            builder.HasIndex(x => new { x.IsActive, x.SortOrder });
            builder.HasIndex(x => new { x.CategoryId, x.Code }).IsUnique();

            builder.HasOne(x => x.File).WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Platform).WithMany().HasForeignKey(x => x.PlatformId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
