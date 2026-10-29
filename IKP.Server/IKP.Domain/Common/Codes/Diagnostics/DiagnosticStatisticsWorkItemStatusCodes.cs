namespace IKP.Domain.Common.Codes.Diagnostics
{
    /// <summary>
    /// Contains codes for diagnostic statistics work item statuses.
    /// </summary>
    public static class DiagnosticStatisticsWorkItemStatusCodes
    {
        /// <summary>
        /// Indicates that the work item is waiting to be processed.
        /// </summary>
        public const string Pending = "PENDING";

        /// <summary>
        /// Indicates that the work item is currently being processed.
        /// </summary>
        public const string Processing = "PROCESSING";

        /// <summary>
        /// Indicates that the latest requested calculation has been completed.
        /// </summary>
        public const string Completed = "COMPLETED";

        /// <summary>
        /// Indicates that the latest calculation failed and is waiting for retry.
        /// </summary>
        public const string Failed = "FAILED";
    }
}
