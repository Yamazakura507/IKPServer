using IKP.Application.Reports;
using IKP.Application.Reports.Interfaces;
using IKP.Domain.Entities.Reports.Lookups;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.Reports;

public sealed class ReportParameterService(AppDbContext db) : IReportParameterService
{
    public async ValueTask<ReportParameterResponse> CreateAsync(Guid reportId, CreateReportParameterRequest request, CancellationToken cancellationToken = default)
    {
        if(!await db.ReportDefinitions.AnyAsync(x => x.Id == reportId, cancellationToken)) throw new KeyNotFoundException();

        string code = request.Code.Trim();

        if(await db.ReportParameters.AnyAsync(x => x.ReportDefinitionId == reportId && x.Code == code, cancellationToken)) throw new InvalidOperationException("Report parameter code already exists.");
        
        ReportParameter reportParameter = new () 
        {
            ReportDefinitionId = reportId,
            Code = code,
            NameKey = request.NameKey.Trim(),
            DataTypeCodeId = request.DataTypeCodeId,
            IsRequired = request.IsRequired,
            DefaultValue = request.DefaultValue,
            SortOrder = request.SortOrder,
            IsActive = true
        };
        
        db.ReportParameters.Add(reportParameter);
        await db.SaveChangesAsync(cancellationToken);
        
        return To(reportParameter);
    }
    
    public async ValueTask<ReportParameterResponse> UpdateAsync(Guid id, UpdateReportParameterRequest request, CancellationToken cancellationToken = default)
    {
        ReportParameter reportParameter = await db.ReportParameters.SingleOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException();
        
        reportParameter.NameKey = request.NameKey.Trim();
        reportParameter.DataTypeCodeId = request.DataTypeCodeId;
        reportParameter.IsRequired = request.IsRequired;
        reportParameter.DefaultValue = request.DefaultValue;
        reportParameter.SortOrder = request.SortOrder;
        reportParameter.IsActive = request.IsActive;

        await db.SaveChangesAsync(cancellationToken);
        
        return To(reportParameter);
    }
    
    public async ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        ReportParameter reportParameter = await db.ReportParameters.SingleOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException();
        
        reportParameter.IsActive = false;

        await db.SaveChangesAsync(cancellationToken);
    }
    
    private static ReportParameterResponse To(ReportParameter x) => new() 
    {
        Id = x.Id,
        ReportDefinitionId = x.ReportDefinitionId,
        Code = x.Code,
        NameKey = x.NameKey,
        DataTypeCodeId = x.DataTypeCodeId,
        IsRequired = x.IsRequired,
        DefaultValue = x.DefaultValue,
        SortOrder = x.SortOrder,
        IsActive = x.IsActive
    };
}
