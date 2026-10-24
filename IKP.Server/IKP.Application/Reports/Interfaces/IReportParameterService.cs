namespace IKP.Application.Reports.Interfaces;

public interface IReportParameterService
{
    ValueTask<ReportParameterResponse>CreateAsync(Guid reportId, CreateReportParameterRequest request, CancellationToken cancellationToken = default);
    ValueTask<ReportParameterResponse>UpdateAsync(Guid id, UpdateReportParameterRequest request, CancellationToken cancellationToken = default);
    ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
