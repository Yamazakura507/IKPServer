using IKP.Domain.Entities.Files;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Files;

/// <summary>Configures physical file blob metadata.</summary>
public sealed class FileBlobConfiguration : IEntityTypeConfiguration<FileBlob>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<FileBlob> builder)
    {
        builder.ToTable("FileBlobs", DatabaseSchemas.Files);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ContentHash).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Size).IsRequired();
        builder.Property(x => x.StorageProvider).IsRequired().HasMaxLength(50);
        builder.Property(x => x.StorageKey).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasIndex(x => new { x.ContentHash, x.Size }).IsUnique();
        builder.HasIndex(x => x.StorageKey).IsUnique();
        builder.HasIndex(x => x.CreatedAt);
    }
}
