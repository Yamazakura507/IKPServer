using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Updates.Lookups;

namespace IKP.Domain.Entities.Updates;

/// <summary>
/// Defines a product dependency that is installed and authorized together with another product.
/// </summary>
public sealed class UpdateProductRelation : Entity
{
    public Guid ProductId { get; set; }
    public Guid RelatedProductId { get; set; }

    public UpdateProduct Product { get; set; } = null!;
    public UpdateProduct RelatedProduct { get; set; } = null!;
}
