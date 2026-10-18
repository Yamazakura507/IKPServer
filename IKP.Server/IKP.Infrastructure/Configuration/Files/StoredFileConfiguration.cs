using IKP.Domain.Entities.Files;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Files;

/// <summary>Configures logical stored file metadata.</summary>
public sealed class StoredFileConfiguration : IEntityTypeConfiguration<StoredFile>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<StoredFile> builder)
    {
        builder.ToTable("Files", DatabaseSchemas.Files);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OriginalFileName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.ContentType).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Extension).HasMaxLength(32);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasOne(x => x.Blob).WithMany(x => x.Files).HasForeignKey(x => x.BlobId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.BlobId);
        builder.HasIndex(x => new { x.IsDeleted, x.CreatedAt });

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
