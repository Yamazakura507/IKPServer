using IKP.Domain.Entities.Auth.Credentials;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Auth.Credentials;

/// <summary>Configures user credential storage.</summary>
public class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<UserCredential> builder)
    {
        builder.ToTable("UserCredentials", DatabaseSchemas.Auth);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SecretHash).IsRequired().HasMaxLength(1024);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.MustChangePassword).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasOne(x => x.User).WithMany(x => x.Credentials).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.AuthenticationMethod).WithMany(x => x.Credentials).HasForeignKey(x => x.AuthenticationMethodId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.UserId, x.AuthenticationMethodId }).IsUnique();
        builder.HasIndex(x => new { x.AuthenticationMethodId, x.IsActive });
    }
}
