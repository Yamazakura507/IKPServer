using IKP.Domain.Entities.Auth.Roles;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Auth.Roles;

/// <summary>Configures user-role assignments.</summary>
public class AuthUserRoleConfiguration : IEntityTypeConfiguration<AuthUserRole>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AuthUserRole> builder)
    {
        builder.ToTable("UserRoles", DatabaseSchemas.Auth);

        builder.HasKey(x => new { x.UserId, x.RoleId });

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.RoleId);
    }
}
