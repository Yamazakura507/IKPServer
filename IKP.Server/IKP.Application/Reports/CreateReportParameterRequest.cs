namespace IKP.Application.Reports;

/// <summary>
/// Defines a report input parameter.
/// </summary>
public sealed class CreateReportParameterRequest
{
    public string Code { get; set; } = null!;
    public string NameKey { get; set; } = null!;
    public Guid DataTypeCodeId { get; set; }
    public bool IsRequired { get; set; }
    public string? DefaultValue { get; set; }
    public int SortOrder { get; set; }
}
