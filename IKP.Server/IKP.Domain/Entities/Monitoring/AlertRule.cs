using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Common.Interfaces.Lookup;

namespace IKP.Domain.Entities.Monitoring;

public sealed class AlertRule : Entity, IHasCode, IHasActiveState, IShortLocalizedLookup
{
    public string Code { get; set; } = null!;
    public string NameKey { get; set; } = null!;
    public string? DescriptionKey { get; set; }
    public string Condition { get; set; } = null!;
    public string SeverityCode { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
}
