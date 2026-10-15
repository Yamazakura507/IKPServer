using IKP.Domain.Entities.Auth.Sessions;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Auth.Sessions;

/// <summary>Configures authentication session storage.</summary>
public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.ToTable("UserSessions", DatabaseSchemas.Auth);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RefreshTokenHash).IsRequired().HasMaxLength(64);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.ExpiresAt).IsRequired();
        builder.Property(x => x.LastUsedAt);
        builder.Property(x => x.RevokedAt);
        builder.Property(x => x.IpAddress).HasMaxLength(64);
        builder.Property(x => x.UserAgent).HasMaxLength(512);
        builder.HasOne(x => x.User).WithMany(x => x.Sessions).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.ReplacedBySession).WithMany().HasForeignKey(x => x.ReplacedBySessionId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => x.RefreshTokenHash).IsUnique();
        builder.HasIndex(x => new { x.UserId, x.RevokedAt, x.ExpiresAt });
        builder.HasIndex(x => x.ReplacedBySessionId);
    }
}
