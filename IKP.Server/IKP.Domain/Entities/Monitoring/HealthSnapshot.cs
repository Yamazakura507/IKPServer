using IKP.Domain.Common.Base;
namespace IKP.Domain.Entities.Monitoring;
public sealed class HealthSnapshot : Entity
{
    public string Component { get; set; } = null!;
    public string StatusCode { get; set; } = null!;
    public double? DurationMilliseconds { get; set; }
    public string? Details { get; set; }
    public DateTime CheckedAt { get; set; }
}
