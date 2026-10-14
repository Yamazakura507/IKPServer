namespace IKP.Application.Diagnostics.Resolvers.Models
{
    /// <summary>
    /// Represents a matched rule contribution to a diagnostic area candidate.
    /// </summary>
    public class DiagnosticAreaResolutionEvidenceModel
    {
        /// <summary>
        /// Gets or sets the mapping identifier.
        /// </summary>
        public Guid MappingId { get; set; }

        /// <summary>
        /// Gets or sets the rule identifier.
        /// </summary>
        public Guid RuleId { get; set; }

        /// <summary>
        /// Gets or sets the pattern snapshot.
        /// </summary>
        public string Pattern { get; set; } = null!;

        /// <summary>
        /// Gets or sets the matched value snapshot.
        /// </summary>
        public string MatchedValue { get; set; } = null!;

        /// <summary>
        /// Gets or sets the priority snapshot.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the base confidence snapshot.
        /// </summary>
        public decimal Confidence { get; set; }

        /// <summary>
        /// Gets or sets the effective contribution used by the aggregator.
        /// </summary>
        public decimal Contribution { get; set; }
    }
}
