namespace IKP.Application.Diagnostics.Resolvers.Models
{
    /// <summary>
    /// Represents a resolution mapping loaded for automatic diagnostic area resolution.
    /// </summary>
    public class DiagnosticAreaResolutionMappingModel
    {
        /// <summary>
        /// Gets or sets the mapping identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the diagnostic area identifier.
        /// </summary>
        public Guid DiagnosticAreaId { get; set; }

        /// <summary>
        /// Gets or sets the resolution rule identifier.
        /// </summary>
        public Guid RuleId { get; set; }

        /// <summary>
        /// Gets or sets the resolution rule code.
        /// </summary>
        public string RuleCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the regular expression pattern.
        /// </summary>
        public string Pattern { get; set; } = null!;

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the base confidence.
        /// </summary>
        public decimal Confidence { get; set; }
    }
}
