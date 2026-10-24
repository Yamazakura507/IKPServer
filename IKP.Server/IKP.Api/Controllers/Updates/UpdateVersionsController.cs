using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Auth.Authorization;
using IKP.Application.Common.Query;
using IKP.Application.Updates;
using IKP.Application.Updates.Interfaces;
using IKP.Domain.Entities.Updates;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Updates;

[ApiController]
[Route("api/updates/versions")]
[Authorize(Policy=AuthorizationPolicyNames.UpdatesRead)]

public sealed class UpdateVersionsController(IUpdateVersionService service, IDbContextFactory<AppDbContext> factory) : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<UpdateVersion, object?>>> Fields = 
        new Dictionary<string, Expression<Func<UpdateVersion, object?>>>(StringComparer.OrdinalIgnoreCase)
        {
            ["id"] = x => x.Id,
            ["productId"] = x => x.ProductId,
            ["version"] = x => x.Version,
            ["isPrerelease"] = x => x.IsPrerelease,
            ["isPublished"] = x => x.IsPublished,
            ["isRequired"] = x => x.IsRequired,
            ["releaseAt"] = x => x.ReleaseAt
        };
    
    [HttpPost("query")]
    public async Task<IActionResult> Query(UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);
        
        return Ok(await db.UpdateVersions
            .AsNoTracking()
            .ToPagedResponseAsync(request, Fields, x => 
                new
                {
                    x.Id,
                    x.ProductId,
                    x.FileId,
                    x.Version,
                    x.ContentHash,
                    x.Size,
                    x.IsPrerelease,
                    x.IsPublished,
                    x.IsRequired,
                    x.ReleaseAt,
                    x.CreatedAt,
                    x.UpdatedAt
                },
            cancellationToken));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        UpdateVersionResponse? versionResponse = await service.GetAsync(id, cancellationToken);
        
        return versionResponse is null ? NotFound() : Ok(versionResponse);
    }
    
    [HttpPost("product/{productId:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.UpdatesManage)]
    public async Task<IActionResult> Create(Guid productId, CreateVersionRequest request, CancellationToken cancellationToken)
    {
        UpdateVersionResponse versionResponse = await service.CreateAsync(productId, request, null, cancellationToken);
        
        return CreatedAtAction(nameof(Get), new{id = versionResponse.Id}, versionResponse);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy=AuthorizationPolicyNames.UpdatesManage)]
    public async Task<IActionResult> Update(Guid id, UpdateVersionRequest request, CancellationToken cancellationToken) => Ok(await service.UpdateAsync(id, request, null, cancellationToken));
    
    [HttpDelete("{id:guid}")][Authorize(Policy=AuthorizationPolicyNames.UpdatesManage)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
}
