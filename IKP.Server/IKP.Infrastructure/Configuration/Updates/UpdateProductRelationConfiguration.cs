using IKP.Domain.Entities.Updates;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKP.Infrastructure.Configuration.Updates;

public sealed class UpdateProductRelationConfiguration : IEntityTypeConfiguration<UpdateProductRelation>
{
    public void Configure(EntityTypeBuilder<UpdateProductRelation> builder)
    {
        builder.ToTable("UpdateProductRelations", DatabaseSchemas.Updates);

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.ProductId, x.RelatedProductId }).IsUnique();

        builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.RelatedProduct).WithMany().HasForeignKey(x => x.RelatedProductId).OnDelete(DeleteBehavior.Cascade);
    }
}
