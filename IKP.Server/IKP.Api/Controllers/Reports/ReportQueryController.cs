using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Auth.Authorization;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.Reports;
using IKP.Domain.Entities.Reports.Lookups;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Reports;

[ApiController]
[Route("api/reports")]
[Authorize(Policy=AuthorizationPolicyNames.ReportsRead)]
public sealed class ReportQueryController(IDbContextFactory<AppDbContext> factory) : ControllerBase
{
    [HttpPost("templates/query")]
    public async Task<IActionResult> Templates(UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        IReadOnlyDictionary<string, Expression<Func<ReportTemplateVersion, object?>>> fields = 
            new Dictionary<string, Expression<Func<ReportTemplateVersion,  object?>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = x => x.Id,
                ["reportDefinitionId"] = x => x.ReportDefinitionId,
                ["version"] = x => x.Version,
                ["isPublished"] = x => x.IsPublished,
                ["isActive"] = x => x.IsActive
            };
        
        return Ok(await db.ReportTemplateVersions
                            .AsNoTracking()
                            .ToPagedResponseAsync(request, fields, x => 
                                new
                                {
                                    x.Id,
                                    x.ReportDefinitionId,
                                    x.FileId,
                                    x.Version,
                                    x.ContentHash,
                                    x.Size,
                                    x.IsPublished,
                                    x.IsActive,
                                    x.PublishedAt,
                                    x.CreatedAt
                                },
                            cancellationToken));
    }
    
    [HttpPost("parameters/query")]
    public async Task<IActionResult> Parameters(UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await factory.CreateDbContextAsync(cancellationToken);

        IReadOnlyDictionary<string, Expression<Func<ReportParameter, object?>>> fields = 
            new Dictionary<string,Expression<Func<ReportParameter,object?>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = x => x.Id,
                ["reportDefinitionId"] = x => x.ReportDefinitionId,
                ["code"] = x => x.Code,
                ["dataTypeCodeId"] = x => x.DataTypeCodeId,
                ["isRequired"] = x => x.IsRequired,
                ["isActive"] = x => x.IsActive,
                ["sortOrder"] = x => x.SortOrder
            };
        
        return Ok(await db.ReportParameters
                            .AsNoTracking()
                            .ToPagedResponseAsync(request, fields, x => 
                                new 
                                { 
                                    x.Id,
                                    x.ReportDefinitionId,
                                    x.Code,
                                    x.NameKey,
                                    x.DataTypeCodeId,
                                    x.IsRequired,
                                    x.DefaultValue,
                                    x.SortOrder,
                                    x.IsActive
                                },
                                cancellationToken));
    }
}