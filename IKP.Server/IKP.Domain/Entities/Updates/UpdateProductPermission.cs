using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Auth.Users;
using IKP.Domain.Entities.Updates.Lookups;

namespace IKP.Domain.Entities.Updates;

/// <summary>
/// Stores an explicit user permission to install a product.
/// </summary>
public sealed class UpdateProductPermission : Entity
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }

    public AuthUser AuthUser { get; set; } = null!;
    public UpdateProduct Product { get; set; } = null!;
}
