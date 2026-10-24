using IKP.Application.Reports;
using IKP.Application.Reports.Interfaces;
using IKP.Domain.Entities.Reports;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.Reports;

public sealed class ReportTemplateService(AppDbContext db) : IReportTemplateService
{
    public async ValueTask<ReportTemplateResponse> CreateAsync(Guid reportId, CreateReportTemplateVersionRequest request, CancellationToken cancellationToken = default)
    {
        if(!await db.ReportDefinitions.AnyAsync(x => x.Id == reportId, cancellationToken)) throw new KeyNotFoundException();
        
        int version = (await db.ReportTemplateVersions.Where(x => x.ReportDefinitionId == reportId).Select(x => (int?)x.Version).MaxAsync(cancellationToken) ?? 0) + 1;
        
        if(request.IsPublished) await db.ReportTemplateVersions
                                        .Where(x => x.ReportDefinitionId == reportId && x.IsPublished)
                                        .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsPublished, false), cancellationToken);

        ReportTemplateVersion templateVersion = new () 
        {
            ReportDefinitionId = reportId,
            FileId = request.FileId,
            Version = version,
            ContentHash = request.ContentHash,
            Size = request.Size,
            IsPublished = request.IsPublished,
            IsActive = true,
            PublishedAt = request.IsPublished ? DateTime.UtcNow : null
        };
        
        db.ReportTemplateVersions.Add(templateVersion);
        await db.SaveChangesAsync(cancellationToken);
        
        return To(templateVersion);
    }
    
    public async ValueTask<ReportTemplateResponse> UpdateAsync(Guid id, UpdateReportTemplateRequest request,CancellationToken cancellationToken = default)
    {
        ReportTemplateVersion templateVersion = await db.ReportTemplateVersions.SingleOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException();
        
        if(request.IsPublished) await db.ReportTemplateVersions
                                        .Where(v => v.ReportDefinitionId == templateVersion.ReportDefinitionId && v.Id != id && v.IsPublished)
                                        .ExecuteUpdateAsync(v => v.SetProperty(p => p.IsPublished, false), cancellationToken);
        
        templateVersion.FileId = request.FileId;
        templateVersion.ContentHash = request.ContentHash;
        templateVersion.Size = request.Size;
        templateVersion.IsPublished = request.IsPublished;
        templateVersion.IsActive = request.IsActive;
        templateVersion.PublishedAt = request.IsPublished ? templateVersion.PublishedAt ?? DateTime.UtcNow : null;
        
        await db.SaveChangesAsync(cancellationToken);
        
        return To(templateVersion);
    }
    
    public async ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        ReportTemplateVersion templateVersion = await db.ReportTemplateVersions.SingleOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException();

        templateVersion.IsActive = false;
        templateVersion.IsPublished = false;
        
        await db.SaveChangesAsync(cancellationToken);
    }
    
    private static ReportTemplateResponse To(ReportTemplateVersion templateVersion) => new() 
    {
        Id = templateVersion.Id,
        ReportDefinitionId = templateVersion.ReportDefinitionId,
        FileId = templateVersion.FileId,
        Version = templateVersion.Version,
        ContentHash = templateVersion.ContentHash,
        Size = templateVersion.Size,
        IsPublished = templateVersion.IsPublished,
        IsActive = templateVersion.IsActive,
        PublishedAt = templateVersion.PublishedAt
    };
}
