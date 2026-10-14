using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Diagnostics.Errors;

namespace IKP.Domain.Entities.Diagnostics.Statistics
{
    /// <summary>
    /// Stores durable diagnostic statistics recalculation work.
    /// </summary>
    public class DiagnosticStatisticsWorkItem : Entity
    {
        /// <summary>
        /// Gets or sets the error group that requires recalculation.
        /// </summary>
        public Guid ErrorGroupId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the newest calculation request.
        /// </summary>
        public DateTime RequestedAt { get; set; }

        /// <summary>
        /// Gets or sets the earliest time at which processing may start.
        /// </summary>
        public DateTime AvailableAt { get; set; }

        /// <summary>
        /// Gets or sets the timestamp at which the current attempt started.
        /// </summary>
        public DateTime? StartedAt { get; set; }

        /// <summary>
        /// Gets or sets the timestamp at which the latest completed attempt finished.
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Gets or sets the number of processing attempts for the current pending request.
        /// </summary>
        public int AttemptCount { get; set; }

        /// <summary>
        /// Gets or sets the current processing status identifier.
        /// </summary>
        public Guid StatusId { get; set; }

        /// <summary>
        /// Gets or sets the current processing status.
        /// </summary>
        public DiagnosticStatisticsWorkItemStatus Status { get; set; } = null!;

        /// <summary>
        /// Gets or sets the last processing error.
        /// </summary>
        public string? LastError { get; set; }

        /// <summary>
        /// Gets or sets the associated error group.
        /// </summary>
        public ErrorGroup ErrorGroup { get; set; } = null!;
    }
}
