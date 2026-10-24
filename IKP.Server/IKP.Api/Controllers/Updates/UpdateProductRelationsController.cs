using IKP.Application.Auth.Authorization;
using IKP.Infrastructure.Persistence;
using IKP.Domain.Entities.Updates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Updates;

[ApiController]
[Route("api/updates/product-relations")]
[Authorize(Policy=AuthorizationPolicyNames.UpdatesRead)]
public sealed class UpdateProductRelationsController(AppDbContext db) : ControllerBase
{
    public sealed class RelatedRequest
    {
        public Guid RelatedProductId { get; set; }
    }

    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> Get(Guid productId, CancellationToken cancellationToken) => Ok(await db.UpdateProductRelations
                                                                                                                .AsNoTracking()
                                                                                                                .Where(x => x.ProductId == productId || x.RelatedProductId == productId)
                                                                                                                .Select(x => x.ProductId == productId ? x.RelatedProductId : x.ProductId)
                                                                                                                .ToListAsync(cancellationToken));
    
    [HttpPost("{productId:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.UpdatesManage)]
    public async Task<IActionResult> Add(Guid productId, RelatedRequest request, CancellationToken cancellationToken)
    {
        if(productId == request.RelatedProductId) return BadRequest();
        if(!await db.UpdateProducts.AnyAsync(x => x.Id == productId && x.Id == request.RelatedProductId, cancellationToken)) return NotFound();
        if(!await db.UpdateProductRelations.AnyAsync(x => x.CheckProductRelate(productId, request.RelatedProductId), cancellationToken))
        {
            db.UpdateProductRelations.Add(new UpdateProductRelation { ProductId = productId, RelatedProductId = request.RelatedProductId });
            await db.SaveChangesAsync(cancellationToken);
        }
        
        return NoContent();
    }
    
    [HttpDelete("{productId:guid}/{relatedProductId:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.UpdatesManage)]
    public async Task<IActionResult> Delete(Guid productId, Guid relatedProductId, CancellationToken cancellationToken)
    {
        UpdateProductRelation? productRelation = await db.UpdateProductRelations.FirstOrDefaultAsync(x => x.CheckProductRelate(productId, relatedProductId), cancellationToken);
        
        if(productRelation is null) return NotFound();
        
        db.Remove(productRelation);
        await db.SaveChangesAsync(cancellationToken);
        
        return NoContent();
    }
}

internal static class ProductRelationExtensions
{
    internal static bool CheckProductRelate(this UpdateProductRelation upr, Guid productId, Guid relatedProductId) =>
        (upr.ProductId == productId && upr.RelatedProductId == relatedProductId) || (upr.ProductId == relatedProductId && upr.RelatedProductId == productId);
}
