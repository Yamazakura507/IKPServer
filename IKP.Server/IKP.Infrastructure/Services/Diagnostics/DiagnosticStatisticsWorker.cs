using IKP.Application.Diagnostics.Interfaces;
using IKP.Application.Diagnostics.Statistics.Interfaces;
using IKP.Application.Diagnostics.Statistics.Models;
using IKP.Domain.Entities.Diagnostics.Statistics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IKP.Infrastructure.Services.Diagnostics
{
    /// <summary>
    /// Processes durable diagnostic statistics work items outside the API request path.
    /// </summary>
    public class DiagnosticStatisticsWorker : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly DiagnosticStatisticsWorkerOptions options;
        private readonly ILogger<DiagnosticStatisticsWorker> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticStatisticsWorker"/> class.
        /// </summary>
        /// <param name="serviceScopeFactory">Service scope factory.</param>
        /// <param name="options">Worker options.</param>
        /// <param name="logger">Worker logger.</param>
        public DiagnosticStatisticsWorker(IServiceScopeFactory serviceScopeFactory, IOptions<DiagnosticStatisticsWorkerOptions> options, ILogger<DiagnosticStatisticsWorker> logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.options = options.Value;
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
                    logger.LogError(exception, "Diagnostic statistics worker iteration failed.");

                    await Task.Delay(TimeSpan.FromMilliseconds(options.PollIntervalMilliseconds), stoppingToken);
                }
            }
        }

        /// <summary>
        /// Processes one claimed batch of diagnostic statistics work.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The number of claimed work items.</returns>
        private async ValueTask<int> ProcessBatchAsync(CancellationToken cancellationToken)
        {
            using IServiceScope scope = serviceScopeFactory.CreateScope();
            IDiagnosticRepository diagnosticRepository = scope.ServiceProvider.GetRequiredService<IDiagnosticRepository>();

            DateTime now = DateTime.UtcNow;
            IReadOnlyList<DiagnosticStatisticsWorkItem> workItems = 
                await diagnosticRepository.ClaimStatisticsWorkItemsAsync(options.BatchSize, now, TimeSpan.FromMinutes(options.StaleAfterMinutes), cancellationToken);

            foreach (DiagnosticStatisticsWorkItem workItem in workItems)
            {
                await ProcessWorkItemAsync(workItem, scope.ServiceProvider, cancellationToken);
            }

            return workItems.Count;
        }

        /// <summary>
        /// Processes one statistics work item.
        /// </summary>
        /// <param name="workItem">Claimed work item.</param>
        /// <param name="serviceProvider">Scoped service provider.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async ValueTask ProcessWorkItemAsync(DiagnosticStatisticsWorkItem workItem, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            IDiagnosticRepository diagnosticRepository = serviceProvider.GetRequiredService<IDiagnosticRepository>();
            IDiagnosticStatisticsService statisticsService = serviceProvider.GetRequiredService<IDiagnosticStatisticsService>();
            DateTime claimedRequestedAt = workItem.RequestedAt;

            try
            {
                await statisticsService.RecalculateAsync(workItem.ErrorGroupId, DateTime.UtcNow, cancellationToken);
                await diagnosticRepository.CompleteStatisticsWorkItemAsync(workItem.Id, claimedRequestedAt, DateTime.UtcNow, cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception exception)
            {
                TimeSpan retryDelay = CalculateRetryDelay(workItem.AttemptCount);
                DateTime availableAt = DateTime.UtcNow.Add(retryDelay);

                logger.LogError(
                    exception,
                    "Diagnostic statistics recalculation failed for error group {ErrorGroupId}. Retry scheduled for {AvailableAt}.",
                    workItem.ErrorGroupId,
                    availableAt);

                await diagnosticRepository.FailStatisticsWorkItemAsync(workItem.Id, claimedRequestedAt, exception.ToString(), availableAt, cancellationToken);
            }
        }

        /// <summary>
        /// Calculates an exponential retry delay with an upper bound.
        /// </summary>
        /// <param name="attemptCount">Current attempt number.</param>
        /// <returns>Retry delay.</returns>
        private TimeSpan CalculateRetryDelay(int attemptCount)
        {
            double multiplier = Math.Pow(2, Math.Max(0, attemptCount - 1));
            double seconds = Math.Min(options.MaximumRetryDelaySeconds, options.InitialRetryDelaySeconds * multiplier);

            return TimeSpan.FromSeconds(seconds);
        }
    }
}
