namespace IKP.Application.Updates.Interfaces;

public interface IUpdateProductAccessService
{ 
    ValueTask AddAsync(Guid userId, Guid productId, CancellationToken cancellationToken = default); 
    ValueTask RemoveAsync(Guid userId, Guid productId, CancellationToken cancellationToken = default); 
    ValueTask<IReadOnlyList<Guid>> GetAllowedProductIdsAsync(Guid userId, CancellationToken cancellationToken = default); 
}
