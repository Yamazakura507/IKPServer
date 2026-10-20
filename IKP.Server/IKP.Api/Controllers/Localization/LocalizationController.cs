using IKP.Api.Infrastructure.Query;
using IKP.Api.Models.Localization;
using IKP.Application.Auth.Authorization;
using IKP.Application.Common.Query;
using IKP.Application.Files.Interfaces;
using IKP.Application.Files.Models;
using IKP.Infrastructure.Persistence;
using IKP.Domain.Entities.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IKP.Api.Controllers.Localization;

/// <summary>
/// Provides localization language and package metadata.
/// </summary>
[ApiController]
[Route("api/localization")]
[Authorize(Policy = AuthorizationPolicyNames.LocalizationRead)]
public sealed class LocalizationController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<Language, object?>>> LanguageFields = new Dictionary<string, Expression<Func<Language, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id,
        ["code"] = x => x.Code,
        ["cultureCode"] = x => x.CultureCode,
        ["nameKey"] = x => x.NameKey,
        ["descriptionKey"] = x => x.DescriptionKey,
        ["fileId"] = x => x.FileId,
        ["nativeName"] = x => x.NativeName,
        ["sortOrder"] = x => x.SortOrder,
        ["isActive"] = x => x.IsActive,
        ["isSystem"] = x => x.IsSystem,
        ["isDefault"] = x => x.IsDefault,
        ["isRightToLeft"] = x => x.IsRightToLeft
    };

    private readonly IDbContextFactory<AppDbContext> dbContextFactory;
    private readonly IFileService fileService;

    /// <summary>
    /// Initializes the controller.
    /// </summary>
    public LocalizationController(IDbContextFactory<AppDbContext> dbContextFactory, IFileService fileService)
    {
        this.dbContextFactory = dbContextFactory;
        this.fileService = fileService;
    }

    /// <summary>
    /// Gets supported languages with compact projections and universal query support.
    /// </summary>
    [HttpPost("languages/query")]
    public async Task<ActionResult<PagedResponse<LanguageResponse>>> QueryLanguages([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        IQueryable<Language> query = db.Languages.AsNoTracking().Where(x => x.IsActive);

        return Ok(await query.ToPagedResponseAsync(request, LanguageFields, x => new LanguageResponse
        {
            Id = x.Id,
            Code = x.Code,
            CultureCode = x.CultureCode,
            NameKey = x.NameKey,
            DescriptionKey = x.DescriptionKey,
            NativeName = x.NativeName,
            FileId = x.FileId,
            SortOrder = x.SortOrder,
            IsActive = x.IsActive,
            IsSystem = x.IsSystem,
            IsDefault = x.IsDefault,
            IsRightToLeft = x.IsRightToLeft,
            ActivePackageVersion = db.LanguagePackages.Where(p => p.LanguageId == x.Id && p.IsActive).OrderByDescending(p => p.CreatedAt).Select(p => p.Version).FirstOrDefault()
        }, cancellationToken));
    }

    /// <summary>
    /// Gets all active package versions for a language.
    /// </summary>
    [HttpPost("languages/{languageId:guid}/packages/query")]
    public async Task<ActionResult<PagedResponse<LanguagePackageResponse>>> QueryPackages(Guid languageId, [FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        if (!await db.Languages.AnyAsync(x => x.Id == languageId, cancellationToken)) return NotFound();

        IReadOnlyDictionary<string, Expression<Func<LanguagePackage, object?>>> fields = new Dictionary<string, Expression<Func<LanguagePackage, object?>>>(StringComparer.OrdinalIgnoreCase)
        {
            ["id"] = x => x.Id,
            ["languageId"] = x => x.LanguageId,
            ["version"] = x => x.Version,
            ["fileId"] = x => x.FileId,
            ["contentHash"] = x => x.ContentHash,
            ["size"] = x => x.Size,
            ["isActive"] = x => x.IsActive,
            ["isSystem"] = x => x.IsSystem,
            ["createdAt"] = x => x.CreatedAt
        };
        IQueryable<LanguagePackage> query = db.LanguagePackages.AsNoTracking().Where(x => x.LanguageId == languageId);

        return Ok(await query.ToPagedResponseAsync(request, fields, x => new LanguagePackageResponse
        {
            Id = x.Id,
            LanguageId = x.LanguageId,
            LanguageCode = x.Language.Code,
            Version = x.Version,
            FileId = x.FileId,
            ContentHash = x.ContentHash,
            Size = x.Size,
            IsActive = x.IsActive,
            IsSystem = x.IsSystem,
            CreatedAt = x.CreatedAt,
            CreatedBy = x.CreatedBy,
            UpdatedAt = x.UpdatedAt,
            UpdatedBy = x.UpdatedBy
        }, cancellationToken));
    }

    /// <summary>
    /// Gets the currently active package for a language.
    /// </summary>
    [HttpGet("languages/{languageId:guid}/package")]
    public async Task<ActionResult<LanguagePackageResponse>> GetActivePackage(Guid languageId, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        LanguagePackageResponse? response = await db.LanguagePackages
                                                    .AsNoTracking()
                                                    .Where(x => x.LanguageId == languageId && x.IsActive)
                                                    .OrderByDescending(x => x.CreatedAt)
                                                    .Select(x => 
                                                        new LanguagePackageResponse
                                                        {
                                                            Id = x.Id,
                                                            LanguageId = x.LanguageId,
                                                            LanguageCode = x.Language.Code,
                                                            Version = x.Version,
                                                            FileId = x.FileId,
                                                            ContentHash = x.ContentHash,
                                                            Size = x.Size,
                                                            IsActive = x.IsActive,
                                                            IsSystem = x.IsSystem,
                                                            CreatedAt = x.CreatedAt,
                                                            CreatedBy = x.CreatedBy,
                                                            UpdatedAt = x.UpdatedAt,
                                                            UpdatedBy = x.UpdatedBy
                                                        }).FirstOrDefaultAsync(cancellationToken);

        return response is null ? NotFound() : Ok(response);
    }

    /// <summary>
    /// Registers a new language package that has already been uploaded through the Files API.
    /// </summary>
    [HttpPost("languages/{languageId:guid}/packages")]
    [Authorize(Policy = AuthorizationPolicyNames.LocalizationManage)]
    public async Task<ActionResult<LanguagePackageResponse>> RegisterPackage(Guid languageId, [FromBody] RegisterLanguagePackageRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Version)) return BadRequest("Version is required.");
        if (request.Version.Length > 50) return BadRequest("Version is too long.");

        Guid userId = GetRequiredUserId();
        StoredFileResponse? file = await fileService.GetAsync(request.FileId, cancellationToken);

        if (file is null) return NotFound("The package file was not found.");

        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        Language? language = await db.Languages.FirstOrDefaultAsync(x => x.Id == languageId, cancellationToken);

        if (language is null) return NotFound("The language was not found.");

        bool versionExists = await db.LanguagePackages.AnyAsync(x => x.LanguageId == languageId && x.Version == request.Version, cancellationToken);

        if (versionExists) return Conflict("A package with this version already exists for the language.");

        await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);
        await db.LanguagePackages
            .Where(x => x.LanguageId == languageId && x.IsActive)
            .ExecuteUpdateAsync(setters => setters
                                            .SetProperty(x => x.IsActive, false)
                                            .SetProperty(x => x.UpdatedAt, DateTime.UtcNow).
                                            SetProperty(x => x.UpdatedBy, userId), 
            cancellationToken);

        LanguagePackage package = new()
        {
            LanguageId = languageId,
            Version = request.Version.Trim(),
            FileId = file.Id,
            ContentHash = file.ContentHash,
            Size = file.Size,
            IsActive = true,
            IsSystem = request.IsSystem,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId
        };

        db.LanguagePackages.Add(package);
        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return CreatedAtAction(nameof(GetActivePackage), new { languageId }, new LanguagePackageResponse
        {
            Id = package.Id,
            LanguageId = package.LanguageId,
            LanguageCode = language.Code,
            Version = package.Version,
            FileId = package.FileId,
            ContentHash = package.ContentHash,
            Size = package.Size,
            IsActive = package.IsActive,
            IsSystem = package.IsSystem,
            CreatedAt = package.CreatedAt,
            CreatedBy = package.CreatedBy,
            UpdatedAt = package.UpdatedAt,
            UpdatedBy = package.UpdatedBy
        });
    }

    /// <summary>
    /// Deactivates a language package without deleting its physical file.
    /// </summary>
    [HttpDelete("languages/{languageId:guid}/packages/{packageId:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.LocalizationManage)]
    public async Task<IActionResult> DeactivatePackage(Guid languageId, Guid packageId, CancellationToken cancellationToken)
    {
        Guid userId = GetRequiredUserId();
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        LanguagePackage? package = await db.LanguagePackages.FirstOrDefaultAsync(x => x.Id == packageId && x.LanguageId == languageId, cancellationToken);
        
        if (package is null) return NotFound();
        if (package.IsSystem) return Conflict("System language packages cannot be deleted.");

        package.IsActive = false;
        package.UpdatedAt = DateTime.UtcNow;
        package.UpdatedBy = userId;

        await db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    private Guid GetRequiredUserId()
    {
        string? value = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(value, out Guid userId)) throw new InvalidOperationException("The authenticated user identifier is missing.");

        return userId;
    }
}
