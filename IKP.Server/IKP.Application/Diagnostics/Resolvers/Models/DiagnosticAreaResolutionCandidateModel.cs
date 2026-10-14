namespace IKP.Application.Diagnostics.Resolvers.Models
{
    /// <summary>
    /// Represents an automatically resolved diagnostic area candidate.
    /// </summary>
    public class DiagnosticAreaResolutionCandidateModel
    {
        /// <summary>
        /// Gets or sets the diagnostic area identifier.
        /// </summary>
        public Guid DiagnosticAreaId { get; set; }

        /// <summary>
        /// Gets or sets the aggregate confidence of the candidate.
        /// </summary>
        public decimal Confidence { get; set; }

        /// <summary>
        /// Gets or sets the primary rule identifier used for the resolution summary.
        /// </summary>
        public Guid PrimaryRuleId { get; set; }

        /// <summary>
        /// Gets or sets the evidence supporting the candidate.
        /// </summary>
        public List<DiagnosticAreaResolutionEvidenceModel> Evidences { get; set; } = new();
    }
}
