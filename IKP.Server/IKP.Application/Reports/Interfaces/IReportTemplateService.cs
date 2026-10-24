namespace IKP.Application.Reports.Interfaces;

public interface IReportTemplateService
{
    ValueTask<ReportTemplateResponse>CreateAsync(Guid reportId, CreateReportTemplateVersionRequest request, CancellationToken cancellationToken = default);
    ValueTask<ReportTemplateResponse>UpdateAsync(Guid id, UpdateReportTemplateRequest request, CancellationToken cancellationToken = default);
    ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
