using IKP.Domain.Entities.Files;
using IKP.Domain.Entities.Localization;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Localization;

/// <summary>Configures supported languages.</summary>
public sealed class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("Languages", DatabaseSchemas.LocalizationLookup);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).IsRequired().HasMaxLength(20);
        builder.Property(x => x.NameKey).IsRequired().HasMaxLength(200);
        builder.Property(x => x.DescriptionKey).HasMaxLength(500);
        builder.Property(x => x.FileId);
        builder.Property(x => x.SortOrder).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsSystem).IsRequired();
        builder.Property(x => x.CultureCode).IsRequired().HasMaxLength(35);
        builder.Property(x => x.NativeName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.IsDefault).IsRequired();
        builder.Property(x => x.IsRightToLeft).IsRequired();

        builder.HasOne(x => x.File).WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.Code).IsUnique();
        builder.HasIndex(x => x.CultureCode).IsUnique();
        builder.HasIndex(x => new { x.IsActive, x.SortOrder });
        builder.HasIndex(x => x.FileId);
    }
}
