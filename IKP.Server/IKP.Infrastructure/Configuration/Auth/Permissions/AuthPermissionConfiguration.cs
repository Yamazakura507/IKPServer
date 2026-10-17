using IKP.Domain.Entities.Auth.Permissions;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Auth.Permissions;

/// <summary>Configures authorization permission storage.</summary>
public class AuthPermissionConfiguration : IEntityTypeConfiguration<AuthPermission>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AuthPermission> builder)
    {
        builder.ToTable("Permissions", DatabaseSchemas.AuthLookup);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).IsRequired().HasMaxLength(150);
        builder.Property(x => x.NameKey).IsRequired().HasMaxLength(200);
        builder.Property(x => x.DescriptionKey).HasMaxLength(500);
        builder.Property(x => x.FileId);
        builder.Property(x => x.SortOrder).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsSystem).IsRequired();

        builder.HasIndex(x => x.Code).IsUnique();
        builder.HasIndex(x => new { x.IsActive, x.SortOrder });
    }
}
