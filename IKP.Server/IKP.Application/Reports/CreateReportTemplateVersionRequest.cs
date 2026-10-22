namespace IKP.Application.Reports;

/// <summary>
/// Registers a report template file as a new version.
/// </summary>
public sealed class CreateReportTemplateVersionRequest
{
    public Guid FileId { get; set; }
    public string? ContentHash { get; set; }
    public long? Size { get; set; }
    public bool IsPublished { get; set; }
}
