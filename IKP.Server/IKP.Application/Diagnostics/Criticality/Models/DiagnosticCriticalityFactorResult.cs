namespace IKP.Application.Diagnostics.Criticality.Models
{
    /// <summary>
    /// Represents one calculated criticality factor.
    /// </summary>
    public class DiagnosticCriticalityFactorResult
    {
        /// <summary>
        /// Gets or sets the factor code.
        /// </summary>
        public string Code { get; set; } = null!;

        /// <summary>
        /// Gets or sets the normalized score from 0 to 100.
        /// </summary>
        public decimal Score { get; set; }

        /// <summary>
        /// Gets or sets the configured base weight.
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Gets or sets the source metric value.
        /// </summary>
        public decimal? Value { get; set; }

        /// <summary>
        /// Gets or sets whether the source metric is available for this calculation.
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
