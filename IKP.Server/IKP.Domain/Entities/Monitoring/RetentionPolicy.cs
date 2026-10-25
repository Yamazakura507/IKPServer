using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Audit;
using IKP.Domain.Common.Interfaces.Information;
namespace IKP.Domain.Entities.Monitoring;
public sealed class RetentionPolicy : Entity, IHasActiveState, IAuditable
{
    public string AreaCode { get; set; } = null!;
    public int RetentionDays { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastRunAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}
