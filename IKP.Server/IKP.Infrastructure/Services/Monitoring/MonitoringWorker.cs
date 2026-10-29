using IKP.Application.Monitoring.Interfaces;
using IKP.Domain.Common.Codes.Monitoring;
using IKP.Domain.Entities.Monitoring;
using IKP.Infrastructure.Persistence;
using IKP.Infrastructure.Services.Redis.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IKP.Infrastructure.Services.Monitoring;

/// <summary>
/// Periodically captures database health, applies retention, and evaluates registered health alert rules.
/// The worker uses a distributed lock so multiple API replicas do not perform the same maintenance cycle.
/// </summary>
public sealed class MonitoringWorker(IServiceScopeFactory scopeFactory, IRedisDistributedLockService distributedLockService, ILogger<MonitoringWorker> logger) : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromMinutes(1);
    private static readonly TimeSpan LockLifetime = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Run once shortly after startup, then on a fixed delay.
    /// </summary>
    /// <param name="stoppingToken">token</param>
    /// <returns>Task</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await RunCycleAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Monitoring maintenance cycle failed.");
            }

            try
            {
                await Task.Delay(Interval, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
        }
    }

    private async Task RunCycleAsync(CancellationToken cancellationToken)
    {
        await using IRedisDistributedLockHandle? distributedLock = await distributedLockService.TryAcquireAsync("ikp:monitoring:maintenance", LockLifetime, cancellationToken);

        if (distributedLock is null)
        {
            logger.LogDebug("Monitoring maintenance cycle skipped because another worker owns the lock.");

            return;
        }

        using IServiceScope scope = scopeFactory.CreateScope();
        IMonitoringService monitoring = scope.ServiceProvider.GetRequiredService<IMonitoringService>();
        AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        HealthSnapshot snapshot = await monitoring.CheckAsync("database", cancellationToken);

        await EvaluateHealthAlertsAsync(db, snapshot, cancellationToken);

        int removed = await monitoring.ApplyRetentionAsync(cancellationToken);

        logger.LogInformation("Monitoring cycle completed. Database status: {StatusCode}; expired health snapshots removed: {RemovedCount}.", snapshot.StatusCode, removed);
    }

    private static async Task EvaluateHealthAlertsAsync(AppDbContext db, HealthSnapshot snapshot, CancellationToken cancellationToken)
    {
        List<AlertRule> rules = await db.AlertRules.Where(x => x.IsActive).ToListAsync(cancellationToken);

        DateTime now = DateTime.UtcNow;
        bool unhealthy = string.Equals(snapshot.StatusCode, MonitoringCodes.Unhealthy, StringComparison.OrdinalIgnoreCase)
                            || string.Equals(snapshot.StatusCode, MonitoringCodes.Degraded, StringComparison.OrdinalIgnoreCase);

        foreach (AlertRule rule in rules)
        {
            bool applies = rule.Condition is "DATABASE_UNHEALTHY" or "HEALTH_UNHEALTHY";

            if (!applies) continue;

            Alert? activeAlert = await db.Alerts
                .Where(x => x.AlertRuleId == rule.Id && x.ResolvedAt == null)
                .OrderByDescending(x => x.TriggeredAt)
                .FirstOrDefaultAsync(cancellationToken);

            if (unhealthy && activeAlert is null)
            {
                db.Alerts.Add(new Alert
                {
                    AlertRuleId = rule.Id,
                    StatusCode = rule.SeverityCode,
                    Message = $"Health check for component '{snapshot.Component}' returned {snapshot.StatusCode}.",
                    TriggeredAt = now
                });
            }
            else if (!unhealthy && activeAlert is not null)
            {
                activeAlert.ResolvedAt = now;
            }
        }

        await db.SaveChangesAsync(cancellationToken);
    }
}
