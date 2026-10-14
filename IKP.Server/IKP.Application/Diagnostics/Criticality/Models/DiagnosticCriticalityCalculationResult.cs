namespace IKP.Application.Diagnostics.Criticality.Models
{
    /// <summary>
    /// Represents the result of an automatic diagnostic criticality calculation.
    /// </summary>
    public class DiagnosticCriticalityCalculationResult
    {
        /// <summary>
        /// Gets or sets the automatic score from 0 to 100.
        /// </summary>
        public decimal AutomaticScore { get; set; }

        /// <summary>
        /// Gets or sets the calculated factors.
        /// </summary>
        public IReadOnlyList<DiagnosticCriticalityFactorResult> Factors { get; set; } = Array.Empty<DiagnosticCriticalityFactorResult>();
    }
}
