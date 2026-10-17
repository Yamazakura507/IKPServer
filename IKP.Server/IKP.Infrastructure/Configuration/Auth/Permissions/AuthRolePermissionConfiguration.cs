using IKP.Domain.Entities.Auth.Permissions;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Auth.Permissions;

/// <summary>Configures role-permission assignments.</summary>
public class AuthRolePermissionConfiguration : IEntityTypeConfiguration<AuthRolePermission>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AuthRolePermission> builder)
    {
        builder.ToTable("RolePermissions", DatabaseSchemas.Auth);

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasOne(x => x.Role).WithMany(x => x.RolePermissions).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Permission).WithMany(x => x.RolePermissions).HasForeignKey(x => x.PermissionId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.PermissionId);
    }
}
