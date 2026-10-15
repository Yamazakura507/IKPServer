using IKP.Domain.Entities.Auth.Users;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Auth.Users;

/// <summary>Configures authentication user storage.</summary>
public class AuthUserConfiguration : IEntityTypeConfiguration<AuthUser>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AuthUser> builder)
    {
        builder.ToTable("Users", DatabaseSchemas.Auth);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.NormalizedUserName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Email).HasMaxLength(320);
        builder.Property(x => x.NormalizedEmail).HasMaxLength(320);
        builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.IsBootstrapAccount).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.FailedLoginCount).IsRequired();
        builder.Property(x => x.LockoutEndAt);
        builder.Property(x => x.LastLoginAt);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);
        builder.HasIndex(x => x.NormalizedUserName).IsUnique();
        builder.HasIndex(x => x.NormalizedEmail).IsUnique().HasFilter("\"NormalizedEmail\" IS NOT NULL");
        builder.HasIndex(x => new { x.IsActive, x.IsDeleted });
        builder.HasIndex(x => x.IsBootstrapAccount).IsUnique().HasFilter("\"IsBootstrapAccount\" = TRUE");
    }
}
