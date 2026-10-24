using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Auth.Authorization;
using IKP.Application.Common.Query;
using IKP.Application.Updates;
using IKP.Application.Updates.Interfaces;
using IKP.Domain.Entities.Updates.Lookups;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Updates;

[ApiController]
[Route("api/updates/products")]
[Authorize(Policy=AuthorizationPolicyNames.UpdatesRead)]
public sealed class UpdateProductsController(IUpdateProductService service, IDbContextFactory<AppDbContext> factory) : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<UpdateProduct, object?>>> Fields 
        = new Dictionary<string, Expression<Func<UpdateProduct, object?>>>(StringComparer.OrdinalIgnoreCase)
        {
            ["id"] = x => x.Id, 
            ["code"] = x => x.Code, 
            ["categoryId"] = x => x.CategoryId,
            ["categoryCode"] = x => x.Category.Code,
            ["platformId"] = x => x.PlatformId,
            ["platformCode"] = x => x.Platform!.Code,
            ["isActive"] = x => x.IsActive
        };
    
    [HttpPost("query")]
    public async Task<IActionResult> Query(UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.UpdateProducts
            .AsNoTracking()
            .ToPagedResponseAsync(request, Fields, p => new
            {
                p.Id,
                p.Code,
                p.CategoryId,
                CategoryCode = p.Category.Code,
                p.PlatformId,
                PlatformCode = p.Platform == null ? null : p.Platform.Code,
                p.NameKey,
                p.DescriptionKey,
                p.FileId,
                p.IsActive,
                p.IsSystem
            }, cancellationToken));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken) => OkOrNotFound(await service.GetAsync(id, cancellationToken));
    
    [HttpPost]
    [Authorize(Policy=AuthorizationPolicyNames.UpdatesManage)]
    public async Task<IActionResult> Create(CreateProductRequest request, CancellationToken cancellationToken)
    {
        UpdateProductResponse response = await service.CreateAsync(request, null, cancellationToken);

        return CreatedAtAction(nameof(Get), new{ id = response.Id}, response);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.UpdatesManage)]
    public async Task<IActionResult> Update(Guid id, UpdateProductRequest request, CancellationToken cancellationToken) => Ok(await service.UpdateAsync(id, request, null, cancellationToken));
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.UpdatesManage)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
    
    private IActionResult OkOrNotFound(UpdateProductResponse? response) => response is null ? NotFound() : Ok(response);
}
