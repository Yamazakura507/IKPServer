using IKP.Application.Updates;
using IKP.Application.Updates.Interfaces;
using IKP.Domain.Entities.Updates;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.Updates;

public sealed class UpdateVersionService(AppDbContext db) : IUpdateVersionService
{
    public async ValueTask<UpdateVersionResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default) => 
        await db.UpdateVersions
        .AsNoTracking()
        .Where(x => x.Id == id)
        .Select(x => new UpdateVersionResponse
            {
                Id = x.Id,
                ProductId = x.ProductId,
                FileId = x.FileId,
                Version = x.Version,
                ContentHash = x.ContentHash,
                Size = x.Size,
                IsPrerelease = x.IsPrerelease,
                IsPublished = x.IsPublished,
                IsRequired = x.IsRequired,
                ReleaseAt = x.ReleaseAt
            }).SingleOrDefaultAsync(cancellationToken);
    
    public async ValueTask<UpdateVersionResponse> CreateAsync(Guid productId, CreateVersionRequest request, Guid? userId, CancellationToken cancellationToken = default)
    {
        if(!await db.UpdateProducts.AnyAsync(x => x.Id == productId, cancellationToken)) throw new KeyNotFoundException();
        if(await db.UpdateVersions.AnyAsync(x => x.ProductId == productId && x.Version == request.Version, cancellationToken)) throw new InvalidOperationException("Version already exists.");
        
        if(request.IsPublished) await db.UpdateVersions
                                        .Where(x => x.ProductId == productId && x.IsPublished)
                                        .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsPublished, false), cancellationToken);
        
        UpdateVersion updateVersion = new () 
        {
            ProductId = productId,
            FileId = request.FileId,
            Version = request.Version.Trim(),
            ContentHash = request.ContentHash,
            Size = request.Size,
            IsPrerelease = request.IsPrerelease,
            IsPublished = request.IsPublished,
            IsRequired = request.IsRequired,
            ReleaseAt = request.ReleaseAt
        };
        
        db.UpdateVersions.Add(updateVersion);
        await db.SaveChangesAsync(cancellationToken);
        
        return (await GetAsync(updateVersion.Id,cancellationToken))!;
    }
    
    public async ValueTask<UpdateVersionResponse> UpdateAsync(Guid id, UpdateVersionRequest request, Guid? userId, CancellationToken cancellationToken = default)
    {
        UpdateVersion updateVersion = await db.UpdateVersions.SingleOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException();
        
        if(request.IsPublished)
        {
            Guid productId = updateVersion.ProductId;
            
            await db.UpdateVersions
                .Where(v => v.ProductId == productId && v.Id != id && v.IsPublished)
                .ExecuteUpdateAsync(v => v.SetProperty(p => p.IsPublished, false), cancellationToken);
        }
        
        updateVersion.FileId = request.FileId;
        updateVersion.Version = request.Version.Trim();
        updateVersion.ContentHash = request.ContentHash;
        updateVersion.Size = request.Size;
        updateVersion.IsPrerelease = request.IsPrerelease;
        updateVersion.IsPublished = request.IsPublished;
        updateVersion.IsRequired = request.IsRequired;
        updateVersion.ReleaseAt = request.ReleaseAt;
        
        await db.SaveChangesAsync(cancellationToken);
        
        return (await GetAsync(id, cancellationToken))!;
    }
   
    public async ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        UpdateVersion updateVersion = await db.UpdateVersions.SingleOrDefaultAsync(x => x.Id == id,cancellationToken) ?? throw new KeyNotFoundException();
        
        db.UpdateVersions.Remove(updateVersion);
        await db.SaveChangesAsync(cancellationToken);
    }
}
