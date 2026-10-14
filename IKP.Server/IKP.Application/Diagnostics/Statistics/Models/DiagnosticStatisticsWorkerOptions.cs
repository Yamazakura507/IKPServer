namespace IKP.Application.Diagnostics.Statistics.Models
{
    /// <summary>
    /// Configures the diagnostic statistics background worker.
    /// </summary>
    public class DiagnosticStatisticsWorkerOptions
    {
        /// <summary>
        /// Gets or sets the maximum number of work items claimed in one batch.
        /// </summary>
        public int BatchSize { get; set; } = 32;

        /// <summary>
        /// Gets or sets the polling interval in milliseconds when no work is available.
        /// </summary>
        public int PollIntervalMilliseconds { get; set; } = 1000;

        /// <summary>
        /// Gets or sets the maximum processing time before a work item is considered stale.
        /// </summary>
        public int StaleAfterMinutes { get; set; } = 10;

        /// <summary>
        /// Gets or sets the initial retry delay in seconds.
        /// </summary>
        public int InitialRetryDelaySeconds { get; set; } = 5;

        /// <summary>
        /// Gets or sets the maximum retry delay in seconds.
        /// </summary>
        public int MaximumRetryDelaySeconds { get; set; } = 300;
    }
}
