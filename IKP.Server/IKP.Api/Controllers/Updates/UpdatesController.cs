using IKP.Application.Auth.Authorization;
using IKP.Application.Updates;
using IKP.Domain.Entities.Updates;
using IKP.Domain.Entities.Updates.Lookups;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Updates;

[ApiController]
[Route("api/updates")]
[Authorize(Policy = AuthorizationPolicyNames.UpdatesRead)]
public sealed class UpdatesController(IDbContextFactory<AppDbContext> factory) : ControllerBase
{
    [HttpGet("categories")]
    public async Task<IActionResult> Categories(CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.UpdateCategories
                            .AsNoTracking()
                            .Where(x => x.IsActive)
                            .OrderBy(x => x.SortOrder)
                            .Select(x => new { x.Id, x.Code, x.NameKey, x.DescriptionKey, x.FileId })
                            .ToListAsync(cancellationToken));
    }

    [HttpGet("platforms")]
    public async Task<IActionResult> Platforms(CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.UpdatePlatforms.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.SortOrder).ToListAsync(cancellationToken));
    }

    [HttpGet("products")]
    public async Task<IActionResult> Products([FromQuery] Guid? categoryId, [FromQuery] Guid? platformId, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);
        IQueryable<UpdateProduct> products = db.UpdateProducts.AsNoTracking().Where(x => x.IsActive);

        if (categoryId.HasValue) products = products.Where(x => x.CategoryId == categoryId.Value);
        if (platformId.HasValue) products = products.Where(x => x.PlatformId == platformId.Value);

        return Ok(await products
                            .OrderBy(x => x.SortOrder)
                            .Select(x => new { x.Id, x.CategoryId, x.PlatformId, x.Code, x.NameKey, x.DescriptionKey, x.IsSystem })
                            .ToListAsync(cancellationToken));
    }

    [HttpGet("products/{productId:guid}/versions")]
    public async Task<IActionResult> Versions(Guid productId, [FromQuery] bool publishedOnly = false, CancellationToken cancellationToken = default)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        if (!await db.UpdateProducts.AnyAsync(x => x.Id == productId, cancellationToken)) return NotFound();

        IQueryable<UpdateVersion> versions = db.UpdateVersions.AsNoTracking().Where(x => x.ProductId == productId);

        if (publishedOnly) versions = versions.Where(x => x.IsPublished);

        return Ok(await versions
                            .OrderByDescending(x => x.ReleaseAt)
                            .ThenByDescending(x => x.Version)
                            .Select(x => 
                                        new 
                                        { 
                                            x.Id, 
                                            x.ProductId, 
                                            x.Version, 
                                            x.FileId, 
                                            x.ContentHash, 
                                            x.Size, 
                                            x.IsPrerelease, 
                                            x.IsPublished, 
                                            x.IsRequired, 
                                            x.ReleaseAt 
                                        })
                            .ToListAsync(cancellationToken));
    }

    [HttpGet("versions/{versionId:guid}")]
    public async Task<IActionResult> Version(Guid versionId, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);
        UpdateVersion? version = await db.UpdateVersions.AsNoTracking().Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == versionId, cancellationToken);
        
        if (version is null) return NotFound();

        List<UpdateReleaseNote> notes = await db.UpdateReleaseNotes
                                                    .AsNoTracking()
                                                    .Where(x => x.VersionId == versionId)
                                                    .Include(x => x.Attachments)
                                                    .OrderBy(x => x.SortOrder)
                                                    .ToListAsync(cancellationToken);
        List<UpdateDownloadLink> links = await db.UpdateDownloadLinks
                                                    .AsNoTracking()
                                                    .Where(x => x.VersionId == versionId && x.IsActive)
                                                    .ToListAsync(cancellationToken);
        return Ok(new 
        { 
            version.Id, 
            version.ProductId, 
            product = new 
            { 
                version.Product.Id, 
                version.Product.Code, 
                version.Product.NameKey, 
                version.Product.CategoryId, 
                version.Product.PlatformId 
            }, 
            version.Version, 
            version.FileId, 
            version.ContentHash, 
            version.Size, 
            version.IsPrerelease, 
            version.IsPublished, 
            version.IsRequired, 
            version.ReleaseAt, 
            notes, 
            downloadLinks = links 
        });
    }

    [Authorize(Policy = AuthorizationPolicyNames.UpdatesManage)]
    [HttpPost("products")]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        if (!await db.UpdateCategories.AnyAsync(x => x.Id == request.CategoryId && x.IsActive, cancellationToken)) return BadRequest("Unknown update category.");

        UpdateProduct product = new() 
        { 
            CategoryId = request.CategoryId, 
            PlatformId = request.PlatformId, 
            Code = request.Code.Trim(), 
            NameKey = request.NameKey.Trim(), 
            DescriptionKey = request.DescriptionKey 
        };
        
        db.UpdateProducts.Add(product); 
        await db.SaveChangesAsync(cancellationToken); 
        
        return Created($"api/updates/products/{product.Id}", product);
    }

    [Authorize(Policy = AuthorizationPolicyNames.UpdatesManage)]
    [HttpPost("products/{productId:guid}/versions")]
    public async Task<IActionResult> CreateVersion(Guid productId, CreateVersionRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        if (!await db.UpdateProducts.AnyAsync(x => x.Id == productId && x.IsActive, cancellationToken)) return NotFound();

        UpdateVersion version = new() 
        { 
            ProductId = productId, 
            Version = request.Version.Trim(), 
            FileId = request.FileId, 
            ContentHash = request.ContentHash, 
            Size = request.Size, 
            IsPrerelease = request.IsPrerelease, 
            IsPublished = request.IsPublished, 
            IsRequired = request.IsRequired, 
            ReleaseAt = request.ReleaseAt 
        };

        db.UpdateVersions.Add(version); 
        await db.SaveChangesAsync(cancellationToken); 

        return Created($"api/updates/versions/{version.Id}", version);
    }

    [Authorize(Policy = AuthorizationPolicyNames.UpdatesManage)]
    [HttpPost("versions/{versionId:guid}/notes")]
    public async Task<IActionResult> AddNote(Guid versionId, CreateReleaseNoteRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        if (!await db.UpdateVersions.AnyAsync(x => x.Id == versionId, cancellationToken)) return NotFound();

        UpdateReleaseNote note = new() 
        { 
            VersionId = versionId, 
            SortOrder = request.SortOrder, 
            Title = request.Title.Trim(), 
            Description = request.Description, 
            IsPlanned = request.IsPlanned 
        };
        
        db.UpdateReleaseNotes.Add(note); 
        await db.SaveChangesAsync(cancellationToken); 
        
        return Created($"api/updates/versions/{versionId}", note);
    }

    [Authorize(Policy = AuthorizationPolicyNames.UpdatesManage)]
    [HttpPost("notes/{noteId:guid}/attachments")]
    public async Task<IActionResult> AddAttachment(Guid noteId, AddAttachmentRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        if (!await db.UpdateReleaseNotes.AnyAsync(x => x.Id == noteId, cancellationToken)) return NotFound();

        UpdateReleaseNoteAttachment attachment = new() { ReleaseNoteId = noteId, FileId = request.FileId, SortOrder = request.SortOrder };

        db.UpdateReleaseNoteAttachments.Add(attachment); 
        await db.SaveChangesAsync(cancellationToken); 
        
        return Ok(attachment);
    }

    [Authorize(Policy = AuthorizationPolicyNames.UpdatesManage)]
    [HttpPost("versions/{versionId:guid}/download-links")]
    public async Task<IActionResult> AddDownloadLink(Guid versionId, AddDownloadLinkRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        if (!await db.UpdateVersions.AnyAsync(x => x.Id == versionId, cancellationToken)) return NotFound();
        if (request.IsPrimary) 
            await db.UpdateDownloadLinks
                        .Where(x => x.VersionId == versionId && x.IsPrimary)
                        .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsPrimary, false), cancellationToken);
        
        UpdateDownloadLink link = new() { VersionId = versionId, Url = request.Url.Trim(), Name = request.Name, IsPrimary = request.IsPrimary };
        
        db.UpdateDownloadLinks.Add(link); 
        await db.SaveChangesAsync(cancellationToken); 
        
        return Ok(link);
    }
}
