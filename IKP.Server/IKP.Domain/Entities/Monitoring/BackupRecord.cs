using IKP.Domain.Common.Base;
namespace IKP.Domain.Entities.Monitoring;
public sealed class BackupRecord : Entity
{
    public string BackupType { get; set; } = null!;
    public string StatusCode { get; set; } = null!;
    public string? StorageKey { get; set; }
    public long? Size { get; set; }
    public string? ContentHash { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? ErrorMessage { get; set; }
}
