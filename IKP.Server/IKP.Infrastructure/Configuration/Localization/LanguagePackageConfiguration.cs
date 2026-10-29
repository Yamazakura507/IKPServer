using IKP.Domain.Entities.Files;
using IKP.Domain.Entities.Localization;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Localization;

/// <summary>Configures language packages.</summary>
public sealed class LanguagePackageConfiguration : IEntityTypeConfiguration<LanguagePackage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<LanguagePackage> builder)
    {
        builder.ToTable("LanguagePackages", DatabaseSchemas.Localization);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Version).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ContentHash).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Size).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsSystem).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt);

        builder.HasOne(x => x.Language).WithMany().HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.File).WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.LanguageId, x.Version }).IsUnique();
        builder.HasIndex(x => new { x.LanguageId, x.IsActive });
        builder.HasIndex(x => x.FileId).IsUnique();
    }
}
