using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Reports.Lookups;

/// <summary>
/// Defines a parameter accepted by a report.
/// </summary>
public sealed class ReportParameter : FullLookupLocalized
{
    public Guid ReportDefinitionId { get; set; }
    public Guid DataTypeCodeId { get; set; }
    public bool IsRequired { get; set; }
    public string? DefaultValue { get; set; }

    public ReportDefinition ReportDefinition { get; set; } = null!;
    public ReportDataType ReportDataType { get; set; } = null!;
}
