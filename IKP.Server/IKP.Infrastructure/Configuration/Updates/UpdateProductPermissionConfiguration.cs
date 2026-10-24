using IKP.Domain.Entities.Updates;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Updates;

public sealed class UpdateProductPermissionConfiguration : IEntityTypeConfiguration<UpdateProductPermission>
{
    public void Configure(EntityTypeBuilder<UpdateProductPermission> builder)
    {
        builder.ToTable("UpdateProductPermissions", DatabaseSchemas.Updates);

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.UserId, x.ProductId }).IsUnique();

        builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.AuthUser).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}
