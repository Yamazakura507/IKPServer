using IKP.Domain.Entities.Files;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Files;

/// <summary>Configures in-progress upload session metadata.</summary>
public sealed class FileUploadSessionConfiguration : IEntityTypeConfiguration<FileUploadSession>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<FileUploadSession> builder)
    {
        builder.ToTable("UploadSessions", DatabaseSchemas.Files);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.StorageKey).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.OriginalFileName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.PathAreaFileName).HasMaxLength(500);
        builder.Property(x => x.ContentType).IsRequired().HasMaxLength(255);
        builder.Property(x => x.ExpectedSize);
        builder.Property(x => x.BytesReceived).IsRequired();
        builder.Property(x => x.LastActivityAt).IsRequired();
        builder.Property(x => x.ExpiresAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.IsCompleted).IsRequired();

        builder.HasIndex(x => new { x.ExpiresAt, x.IsCompleted });
        builder.HasIndex(x => x.CreatedBy);
    }
}
