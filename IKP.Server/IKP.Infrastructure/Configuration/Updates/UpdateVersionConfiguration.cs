using IKP.Domain.Entities.Files;
using IKP.Domain.Entities.Updates;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Updates
{
    public sealed class UpdateVersionConfiguration : IEntityTypeConfiguration<UpdateVersion>
    {
        public void Configure(EntityTypeBuilder<UpdateVersion> builder)
        {
            builder.ToTable("Versions", DatabaseSchemas.Updates);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Version).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ContentHash).HasMaxLength(128);
            builder.Property(x => x.Size);
            builder.Property(x => x.IsPrerelease).IsRequired();
            builder.Property(x => x.IsPublished).IsRequired();
            builder.Property(x => x.IsRequired).IsRequired();
            builder.Property(x => x.ReleaseAt);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.FileId);

            builder.HasIndex(x => new { x.ProductId, x.Version }).IsUnique();
            builder.HasIndex(x => new { x.ProductId, x.IsPublished, x.ReleaseAt });

            builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<StoredFile>().WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
