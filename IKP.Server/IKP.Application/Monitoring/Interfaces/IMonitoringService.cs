using IKP.Domain.Entities.Monitoring;
namespace IKP.Application.Monitoring.Interfaces;
public interface IMonitoringService
{
    ValueTask<HealthSnapshot> CheckAsync(string component, CancellationToken cancellationToken = default);
    ValueTask<IReadOnlyList<HealthSnapshot>> GetRecentHealthAsync(int limit, CancellationToken cancellationToken = default);
    ValueTask<int> ApplyRetentionAsync(CancellationToken cancellationToken = default);
}
