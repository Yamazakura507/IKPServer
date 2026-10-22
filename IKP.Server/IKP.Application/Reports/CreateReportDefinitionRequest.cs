namespace IKP.Application.Reports;

/// <summary>
/// Defines a new report definition.
/// </summary>
public sealed class CreateReportDefinitionRequest
{
    public Guid CategoryId { get; set; }
    public string Code { get; set; } = null!;
    public string NameKey { get; set; } = null!;
    public string? DescriptionKey { get; set; }
    public bool IsHtmlDescription { get; set; }
}
