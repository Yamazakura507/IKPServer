namespace IKP.Application.Updates.Interfaces;

public interface IUpdateProductService
{
    ValueTask<UpdateProductResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<UpdateProductResponse> CreateAsync(CreateProductRequest request, Guid? userId, CancellationToken cancellationToken = default);
    ValueTask<UpdateProductResponse> UpdateAsync(Guid id, UpdateProductRequest request, Guid? userId, CancellationToken cancellationToken = default);
    ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
