using IKP.Domain.Entities.Auth.Roles;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Auth.Roles;

/// <summary>Configures authorization role storage.</summary>
public class AuthRoleConfiguration : IEntityTypeConfiguration<AuthRole>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AuthRole> builder)
    {
        builder.ToTable("Roles", DatabaseSchemas.AuthLookup);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
        builder.Property(x => x.NameKey).IsRequired().HasMaxLength(200);
        builder.Property(x => x.DescriptionKey).HasMaxLength(500);
        builder.Property(x => x.FileId);
        builder.HasOne(x => x.File).WithMany().HasForeignKey(x => x.FileId).OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.FileId);
        builder.Property(x => x.SortOrder).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsSystem).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasIndex(x => x.Code).IsUnique();
        builder.HasIndex(x => new { x.IsActive, x.SortOrder });
    }
}
