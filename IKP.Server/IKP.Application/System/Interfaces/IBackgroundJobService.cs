using IKP.Application.System.Models;

namespace IKP.Application.System.Interfaces;

/// <summary>
/// Provides durable background job creation and lifecycle operations.
/// </summary>
public interface IBackgroundJobService
{
    ValueTask<BackgroundJobResponse> EnqueueAsync(string jobType, string? payload, DateTime? scheduledAt, Guid? userId, CancellationToken cancellationToken = default);
    ValueTask<BackgroundJobResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}
