namespace IKP.Application.Diagnostics.Criticality.Models
{
    /// <summary>
    /// Configures the diagnostic criticality worker.
    /// </summary>
    public class DiagnosticCriticalityWorkerOptions
    {
        /// <summary>Gets or sets the maximum number of work items per batch.</summary>
        public int BatchSize { get; set; } = 32;
        /// <summary>Gets or sets the polling interval in milliseconds.</summary>
        public int PollIntervalMilliseconds { get; set; } = 1000;
        /// <summary>Gets or sets the stale processing threshold in minutes.</summary>
        public int StaleAfterMinutes { get; set; } = 10;
        /// <summary>Gets or sets the initial retry delay in seconds.</summary>
        public int InitialRetryDelaySeconds { get; set; } = 5;
        /// <summary>Gets or sets the maximum retry delay in seconds.</summary>
        public int MaximumRetryDelaySeconds { get; set; } = 300;
    }
}
