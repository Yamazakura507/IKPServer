namespace IKP.Application.Diagnostics.Criticality.Models
{
    /// <summary>
    /// Contains persisted diagnostic metrics used for criticality calculation.
    /// </summary>
    public class DiagnosticCriticalityCalculationInput
    {
        /// <summary>
        /// Gets or sets the total occurrence count during the latest hour.
        /// </summary>
        public long OccurrencesLastHour { get; set; }

        /// <summary>
        /// Gets or sets the number of affected users during the latest hour.
        /// </summary>
        public long UsersLastHour { get; set; }

        /// <summary>
        /// Gets or sets the affected-user ratio in percent.
        /// </summary>
        public decimal AffectedUserRatio { get; set; }

        /// <summary>
        /// Gets or sets the current growth rate in percent, when available.
        /// </summary>
        public decimal? GrowthRate { get; set; }

        /// <summary>
        /// Gets or sets the diagnostic area importance in percent.
        /// </summary>
        public decimal? FeatureImportance { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the statistics used for this calculation.
        /// </summary>
        public DateTime StatisticsCalculatedAt { get; set; }

        /// <summary>
        /// Gets or sets the existing manual criticality override.
        /// </summary>
        public decimal? ManualScore { get; set; }
    }
}
