using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Audit;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.Files;
using IKP.Domain.Entities.Reports.Lookups;

namespace IKP.Domain.Entities.Reports;

/// <summary>
/// Defines a versioned report template stored through the Files subsystem.
/// </summary>
public sealed class ReportTemplateVersion : Entity, IAuditable, IHasActiveState
{
    public Guid ReportDefinitionId { get; set; }
    public Guid FileId { get; set; }
    public int Version { get; set; }
    public string? ContentHash { get; set; }
    public long? Size { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }

    public ReportDefinition ReportDefinition { get; set; } = null!;
    public StoredFile File { get; set; } = null!;
}
