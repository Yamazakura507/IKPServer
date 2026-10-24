using IKP.Application.Reports;
using IKP.Application.Reports.Interfaces;
using IKP.Domain.Entities.Reports.Lookups;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.Reports;

public sealed class ReportDefinitionService(AppDbContext db) : IReportDefinitionService
{
    public async ValueTask<ReportDefinitionResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default) => 
        await db.ReportDefinitions
        .AsNoTracking()
        .Where(x => x.Id == id)
        .Select(x => new ReportDefinitionResponse
        {
            Id = x.Id,
            CategoryId = x.CategoryId,
            Code = x.Code,
            NameKey = x.NameKey,
            DescriptionKey = x.DescriptionKey,
            IsHtmlDescription = x.IsHtmlDescription,
            IsActive = x.IsActive,
            IsSystem = x.IsSystem
        }).SingleOrDefaultAsync(cancellationToken);

    public async ValueTask<ReportDefinitionResponse> CreateAsync(CreateReportDefinitionRequest request, CancellationToken cancellationToken = default)
    {
        if(await db.ReportDefinitions.AnyAsync(x => x.Code == request.Code.Trim(), cancellationToken)) throw new InvalidOperationException("Report code already exists.");

        ReportDefinition reportDefinition = new ()
        {
            CategoryId = request.CategoryId,
            Code = request.Code.Trim(),
            NameKey = request.NameKey.Trim(),
            DescriptionKey = request.DescriptionKey,
            IsHtmlDescription = request.IsHtmlDescription,
            IsActive = true
        };
        
        db.ReportDefinitions.Add(reportDefinition);
        await db.SaveChangesAsync(cancellationToken);
        
        return (await GetAsync(reportDefinition.Id,cancellationToken))!;
    }
    
    public async ValueTask<ReportDefinitionResponse> UpdateAsync(Guid id, UpdateReportDefinitionRequest request, CancellationToken cancellationToken = default)
    {
        ReportDefinition reportDefinition = await db.ReportDefinitions.SingleOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException();
        
        if(reportDefinition.IsSystem) throw new InvalidOperationException("System reports cannot be modified.");
        
        reportDefinition.CategoryId = request.CategoryId;
        reportDefinition.NameKey = request.NameKey.Trim();
        reportDefinition.DescriptionKey = request.DescriptionKey;
        reportDefinition.IsHtmlDescription = request.IsHtmlDescription;
        reportDefinition.IsActive = request.IsActive;
        
        await db.SaveChangesAsync(cancellationToken);
        
        return (await GetAsync(id,cancellationToken))!;
    }
    
    public async ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        ReportDefinition reportDefinition = await db.ReportDefinitions.SingleOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException();
        
        if(reportDefinition.IsSystem) throw new InvalidOperationException("System reports cannot be deleted.");
        
        reportDefinition.IsActive = false;
        
        await db.SaveChangesAsync(cancellationToken);
    }
}
