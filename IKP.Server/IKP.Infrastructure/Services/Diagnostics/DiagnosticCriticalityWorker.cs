using IKP.Application.Diagnostics.Criticality.Interfaces;
using IKP.Application.Diagnostics.Criticality.Models;
using IKP.Application.Diagnostics.Interfaces;
using IKP.Infrastructure.Services.Redis.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IKP.Infrastructure.Services.Diagnostics
{
    /// <summary>
    /// Recalculates criticality asynchronously after statistics have been persisted.
    /// </summary>
    public class DiagnosticCriticalityWorker : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly DiagnosticCriticalityWorkerOptions options;
        private readonly IRedisDistributedLockService distributedLockService;
        private readonly ILogger<DiagnosticCriticalityWorker> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticCriticalityWorker"/> class.
        /// </summary>
        public DiagnosticCriticalityWorker(IServiceScopeFactory serviceScopeFactory, IOptions<DiagnosticCriticalityWorkerOptions> options, IRedisDistributedLockService distributedLockService, ILogger<DiagnosticCriticalityWorker> logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.options = options.Value;
            this.distributedLockService = distributedLockService;
            this.logger = logger;
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    int processedCount = await ProcessBatchAsync(stoppingToken);

                    if (processedCount == 0) await Task.Delay(TimeSpan.FromMilliseconds(options.PollIntervalMilliseconds), stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    return;
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Diagnostic criticality worker iteration failed.");
                    await Task.Delay(TimeSpan.FromMilliseconds(options.PollIntervalMilliseconds), stoppingToken);
                }
            }
        }

        private async ValueTask<int> ProcessBatchAsync(CancellationToken cancellationToken)
        {
            using IServiceScope scope = serviceScopeFactory.CreateScope();
            IDiagnosticRepository repository = scope.ServiceProvider.GetRequiredService<IDiagnosticRepository>();
            IDiagnosticCriticalityService service = scope.ServiceProvider.GetRequiredService<IDiagnosticCriticalityService>();
            IReadOnlyList<Guid> candidates = await repository.GetCriticalityRefreshCandidatesAsync(options.BatchSize, cancellationToken);
            int processed = 0;

            foreach (Guid errorGroupId in candidates)
            {
                string lockKey = $"ikp:diagnostics:criticality:{errorGroupId}";
                await using IRedisDistributedLockHandle? distributedLock = await distributedLockService.TryAcquireAsync(lockKey, TimeSpan.FromMinutes(options.StaleAfterMinutes), cancellationToken);
                
                if (distributedLock is null) continue;

                try
                {
                    await service.RecalculateAsync(errorGroupId, DateTime.UtcNow, cancellationToken);
                    processed++;
                }
                catch (Exception exception) when (exception is not OperationCanceledException)
                {
                    logger.LogError(exception, "Diagnostic criticality recalculation failed for error group {ErrorGroupId}.", errorGroupId);
                }
            }

            return processed;
        }
    }
}
