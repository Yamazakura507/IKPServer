namespace IKP.Application.Updates.Interfaces;

public interface IUpdateVersionService
{ 
    ValueTask<UpdateVersionResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default); 
    ValueTask<UpdateVersionResponse> CreateAsync(Guid productId, CreateVersionRequest request, Guid? userId, CancellationToken cancellationToken = default); 
    ValueTask<UpdateVersionResponse> UpdateAsync(Guid id, UpdateVersionRequest request, Guid? userId, CancellationToken cancellationToken = default); 
    ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default); 
}
