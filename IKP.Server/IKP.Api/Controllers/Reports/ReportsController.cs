using IKP.Api.Infrastructure;
using IKP.Application.Auth.Authorization;
using IKP.Application.Reports;
using IKP.Domain.Entities.Reports;
using IKP.Domain.Entities.Reports.Lookups;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Reports;

[ApiController]
[Route("api/reports")]
[Authorize(Policy = AuthorizationPolicyNames.ReportsRead)]
public sealed class ReportsController(IDbContextFactory<AppDbContext> factory) : ControllerBase
{
    [HttpGet("categories")]
    public async Task<IActionResult> Categories(CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.ReportCategories.GetLookupList(cancellationToken));
    }

    [HttpGet("execution-statuses")]
    public async Task<IActionResult> ExecutionStatuses(CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.ReportExecutionStatuses.GetLookupList(cancellationToken));
    }

    [HttpGet("data-typies")]
    public async Task<IActionResult> DataTypies(CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.ReportDataTypies.GetLookupList(cancellationToken));
    }

    [HttpGet("definitions")]
    public async Task<IActionResult> Definitions([FromQuery] Guid? categoryId, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);
        IQueryable<ReportDefinition> query = db.ReportDefinitions.AsNoTracking().Where(x => x.IsActive);

        if (categoryId.HasValue) query = query.Where(x => x.CategoryId == categoryId.Value);

        return Ok(await query.OrderBy(x => x.SortOrder).Select(x => new
        {
            x.Id,
            x.CategoryId,
            x.Code,
            x.NameKey,
            x.DescriptionKey,
            x.IsHtmlDescription,
            x.IsSystem
        }).ToListAsync(cancellationToken));
    }

    [HttpGet("definitions/{reportId:guid}")]
    public async Task<IActionResult> Definition(Guid reportId, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);
        ReportDefinition? report = await db.ReportDefinitions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == reportId, cancellationToken);

        if (report is null) return NotFound();

        return Ok(new
        {
            report.Id,
            report.CategoryId,
            report.Code,
            report.NameKey,
            report.DescriptionKey,
            report.IsHtmlDescription,
            report.IsActive,
            report.IsSystem,
            parameters = await db.ReportParameters.AsNoTracking()
                .Where(x => x.ReportDefinitionId == reportId && x.IsActive)
                .OrderBy(x => x.SortOrder)
                .Select(x => new { x.Id, x.Code, x.NameKey, ReportDataTypeCode = x.ReportDataType.Code, x.IsRequired, x.DefaultValue, x.SortOrder })
                .ToListAsync(cancellationToken),
            templates = await db.ReportTemplateVersions.AsNoTracking()
                .Where(x => x.ReportDefinitionId == reportId)
                .OrderByDescending(x => x.Version)
                .Select(x => new { x.Id, x.Version, x.FileId, x.ContentHash, x.Size, x.IsActive, x.IsPublished, x.PublishedAt, x.CreatedAt })
                .ToListAsync(cancellationToken)
        });
    }

    [Authorize(Policy = AuthorizationPolicyNames.ReportsManage)]
    [HttpPost("definitions")]
    public async Task<IActionResult> CreateDefinition(CreateReportDefinitionRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        if (!await db.ReportCategories.AnyAsync(x => x.Id == request.CategoryId && x.IsActive, cancellationToken)) return BadRequest("Unknown report category.");

        string code = request.Code.Trim();

        if (await db.ReportDefinitions.AnyAsync(x => x.Code == code, cancellationToken)) return Conflict("Report code already exists.");

        ReportDefinition report = new()
        {
            CategoryId = request.CategoryId,
            Code = code,
            NameKey = request.NameKey.Trim(),
            DescriptionKey = request.DescriptionKey,
            IsHtmlDescription = request.IsHtmlDescription
        };

        db.ReportDefinitions.Add(report);
        await db.SaveChangesAsync(cancellationToken);

        return Created($"/api/reports/definitions/{report.Id}", report);
    }

    [Authorize(Policy = AuthorizationPolicyNames.ReportsManage)]
    [HttpPost("definitions/{reportId:guid}/templates")]
    public async Task<IActionResult> AddTemplate(Guid reportId, CreateReportTemplateVersionRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        if (!await db.ReportDefinitions.AnyAsync(x => x.Id == reportId && x.IsActive, cancellationToken)) return NotFound();
        if (!await db.Files.AnyAsync(x => x.Id == request.FileId && !x.IsDeleted, cancellationToken)) return BadRequest("Unknown file.");

        int nextVersion = (await db.ReportTemplateVersions.Where(x => x.ReportDefinitionId == reportId).Select(x => (int?)x.Version).MaxAsync(cancellationToken) ?? 0) + 1;

        if (request.IsPublished)
        {
            await db.ReportTemplateVersions
                .Where(x => x.ReportDefinitionId == reportId && x.IsPublished)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsPublished, false), cancellationToken);
        }

        ReportTemplateVersion template = new()
        {
            ReportDefinitionId = reportId,
            FileId = request.FileId,
            Version = nextVersion,
            ContentHash = request.ContentHash,
            Size = request.Size,
            IsPublished = request.IsPublished,
            PublishedAt = request.IsPublished ? DateTime.UtcNow : null
        };

        db.ReportTemplateVersions.Add(template);
        await db.SaveChangesAsync(cancellationToken);

        return Created($"/api/reports/definitions/{reportId}", template);
    }

    [Authorize(Policy = AuthorizationPolicyNames.ReportsManage)]
    [HttpPost("definitions/{reportId:guid}/parameters")]
    public async Task<IActionResult> AddParameter(Guid reportId, CreateReportParameterRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        if (!await db.ReportDefinitions.AnyAsync(x => x.Id == reportId && x.IsActive, cancellationToken)) return NotFound();

        string code = request.Code.Trim();

        if (await db.ReportParameters.AnyAsync(x => x.ReportDefinitionId == reportId && x.Code == code, cancellationToken)) return Conflict("Report parameter code already exists.");

        ReportParameter parameter = new()
        {
            ReportDefinitionId = reportId,
            Code = code,
            NameKey = request.NameKey.Trim(),
            DataTypeCodeId = request.DataTypeCodeId,
            IsRequired = request.IsRequired,
            DefaultValue = request.DefaultValue,
            SortOrder = request.SortOrder
        };

        db.ReportParameters.Add(parameter);
        await db.SaveChangesAsync(cancellationToken);

        return Created($"/api/reports/definitions/{reportId}", parameter);
    }
}
