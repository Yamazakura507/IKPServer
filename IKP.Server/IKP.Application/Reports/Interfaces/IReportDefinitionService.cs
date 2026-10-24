namespace IKP.Application.Reports.Interfaces;

public interface IReportDefinitionService
{
    ValueTask<ReportDefinitionResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default); 
    ValueTask<ReportDefinitionResponse> CreateAsync(CreateReportDefinitionRequest request, CancellationToken cancellationToken = default); 
    ValueTask<ReportDefinitionResponse> UpdateAsync(Guid id, UpdateReportDefinitionRequest request, CancellationToken cancellationToken = default); 
    ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default); 
}
