using IKP.Application.Updates;
using IKP.Application.Updates.Interfaces;
using IKP.Domain.Entities.Updates.Lookups;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.Updates;

public sealed class UpdateProductService(AppDbContext db) : IUpdateProductService
{
    public async ValueTask<UpdateProductResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default) => 
        await db.UpdateProducts
        .AsNoTracking()
        .Where(x => x.Id == id)
        .Select(x => new UpdateProductResponse 
        { 
            Id = x.Id, 
            CategoryId = x.CategoryId, 
            PlatformId = x.PlatformId, 
            Code = x.Code, 
            NameKey = x.NameKey,
            DescriptionKey = x.DescriptionKey,
            FileId = x.FileId, IsActive = x.IsActive
        }).SingleOrDefaultAsync(cancellationToken);

    public async ValueTask<UpdateProductResponse> CreateAsync(CreateProductRequest request, Guid? userId, CancellationToken cancellationToken = default)
    {
        if(await db.UpdateProducts.AnyAsync(x => x.Code == request.Code, cancellationToken)) throw new InvalidOperationException("Update product code already exists.");

        UpdateProduct updateProduct = new () 
        {
            CategoryId = request.CategoryId,
            PlatformId = request.PlatformId,
            Code = request.Code.Trim(),
            NameKey = request.NameKey.Trim(),
            DescriptionKey = request.DescriptionKey,
            FileId = request.FileId,
            IsActive = true
        };
        
        db.UpdateProducts.Add(updateProduct);
        await db.SaveChangesAsync(cancellationToken);
        
        return (await GetAsync(updateProduct.Id, cancellationToken))!;
    }
 
    public async ValueTask<UpdateProductResponse> UpdateAsync(Guid id, UpdateProductRequest request, Guid? userId,CancellationToken cancellationToken = default)
    {
        UpdateProduct updateProduct = await db.UpdateProducts.SingleOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException(); 

        updateProduct.CategoryId = request.CategoryId; 
        updateProduct.PlatformId = request.PlatformId; 
        updateProduct.NameKey = request.NameKey.Trim();
        updateProduct.DescriptionKey = request.DescriptionKey;
        updateProduct.FileId = request.FileId; 
        updateProduct.IsActive = request.IsActive;
        
        await db.SaveChangesAsync(cancellationToken);
        
        return (await GetAsync(id,cancellationToken))!;
    }
 
    public async ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        UpdateProduct updateProduct = await db.UpdateProducts.SingleOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException();
        
        updateProduct.IsActive = false;
        
        await db.SaveChangesAsync(cancellationToken);
    }
}
