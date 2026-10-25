using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Monitoring;

public sealed class Alert : Entity
{
    public Guid AlertRuleId { get; set; }
    public string StatusCode { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime TriggeredAt { get; set; }
    public DateTime? ResolvedAt { get; set; }

    public AlertRule AlertRule { get; set; } = null!;
}
