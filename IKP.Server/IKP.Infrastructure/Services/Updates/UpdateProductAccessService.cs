using IKP.Application.Updates.Interfaces;
using IKP.Domain.Entities.Updates;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.Updates;

public sealed class UpdateProductAccessService(AppDbContext db) : IUpdateProductAccessService
{
    public async ValueTask AddAsync(Guid userId, Guid productId, CancellationToken cancellationToken = default) => await ChangeAsync(userId, productId, true, cancellationToken);

    public async ValueTask RemoveAsync(Guid userId, Guid productId, CancellationToken cancellationToken = default) => await ChangeAsync(userId, productId, false, cancellationToken);

    public async ValueTask<IReadOnlyList<Guid>> GetAllowedProductIdsAsync(Guid userId, CancellationToken cancellationToken = default) => 
        await db.UpdateProductPermissions.AsNoTracking().Where(x => x.UserId == userId).Select(x => x.ProductId).ToListAsync(cancellationToken);

    private async ValueTask ChangeAsync(Guid userId, Guid root, bool add, CancellationToken cancellationToken)
    {
        HashSet<Guid> ids = await CollectComponentAsync(root,cancellationToken);
        HashSet<Guid> existing = (await db.UpdateProductPermissions.Where(x => x.UserId == userId && ids.Contains(x.ProductId)).Select(x => x.ProductId).ToListAsync(cancellationToken)).ToHashSet();

        if(add)
        {
            foreach (Guid id in ids)
            {
                if (!existing.Contains(id))
                {
                    db.UpdateProductPermissions.Add(new () 
                    { 
                        UserId = userId, 
                        ProductId = id 
                    });
                }
            }
        }
        else
        {
            foreach(Guid id in ids.Where(x => existing.Contains(x)))
            {
                if(await HasOtherGrantedConnectionAsync(userId, id, cancellationToken)) continue;
                
                UpdateProductPermission row = await db.UpdateProductPermissions.SingleAsync(x => x.UserId == userId && x.ProductId == id,cancellationToken);

                db.UpdateProductPermissions.Remove(row);
            }
        }
    
        await db.SaveChangesAsync(cancellationToken);
    }

    private async ValueTask<bool> HasOtherGrantedConnectionAsync(Guid userId, Guid productId, CancellationToken cancellationToken)
    {
        List<Guid> granted = await db.UpdateProductPermissions.Where(x => x.UserId == userId && x.ProductId != productId).Select(x => x.ProductId).ToListAsync(cancellationToken);
        
        if(granted.Count == 0) return false;
        
        var relations = await db.UpdateProductRelations
                                .AsNoTracking()
                                .Where(x => x.ProductId == productId || x.RelatedProductId == productId)
                                .Select(x => new {x.ProductId, x.RelatedProductId})
                                .ToListAsync(cancellationToken);
        
        return relations.Any(x => granted.Contains(x.ProductId == productId? x.RelatedProductId: x.ProductId));
    }

    private async ValueTask<HashSet<Guid>> CollectComponentAsync(Guid root, CancellationToken cancellationToken)
    {
        HashSet<Guid> all = new () { root };
        Queue<Guid> queue = new ();

        queue.Enqueue(root);
        
        while(queue.Count > 0)
        {
            Guid id = queue.Dequeue();

            List<Guid> next = await db.UpdateProductRelations
                .AsNoTracking()
                .Where(x =>  x.ProductId == id || x.RelatedProductId == id)
                .Select(x => x.ProductId == id ? x.RelatedProductId : x.ProductId)
                .ToListAsync(cancellationToken);

            foreach (Guid n in next)
            {
                if (all.Add(n)) queue.Enqueue(n);
            }
        }
        
        return all;
    }
}
