using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Audit;
using IKP.Domain.Entities.Reports.Lookups;

namespace IKP.Domain.Entities.Reports;

/// <summary>
/// Stores the server-side lifecycle and result of a report generation request.
/// </summary>
public sealed class ReportExecution : Entity, IAuditable
{
    public Guid ReportDefinitionId { get; set; }
    public Guid? TemplateVersionId { get; set; }
    public Guid? RequestedBy { get; set; }
    public Guid StatusCodeId { get; set; }
    public string? OutputFileId { get; set; }
    public string? OutputContentType { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }

    public ReportDefinition ReportDefinition { get; set; } = null!;
    public ReportExecutionStatus ReportExecutionStatus { get; set; } = null!;
    public ReportTemplateVersion? TemplateVersion { get; set; }
}
