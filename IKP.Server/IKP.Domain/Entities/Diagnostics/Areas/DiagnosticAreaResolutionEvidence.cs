using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Diagnostics.Definitions;

namespace IKP.Domain.Entities.Diagnostics.Areas
{
    /// <summary>
    /// Stores an immutable piece of evidence that contributed to a diagnostic area resolution.
    /// </summary>
    public class DiagnosticAreaResolutionEvidence : Entity
    {
        /// <summary>
        /// Gets or sets the diagnostic area resolution identifier.
        /// </summary>
        public Guid DiagnosticAreaResolutionId { get; set; }

        /// <summary>
        /// Gets or sets the source mapping identifier.
        /// </summary>
        public Guid? MappingId { get; set; }

        /// <summary>
        /// Gets or sets the resolution rule identifier used by the evidence.
        /// </summary>
        public Guid RuleId { get; set; }

        /// <summary>
        /// Gets or sets the pattern snapshot used by the evidence.
        /// </summary>
        public string Pattern { get; set; } = null!;

        /// <summary>
        /// Gets or sets the matched diagnostic value snapshot.
        /// </summary>
        public string MatchedValue { get; set; } = null!;

        /// <summary>
        /// Gets or sets the priority snapshot used by the evidence.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the base confidence snapshot used by the evidence.
        /// </summary>
        public decimal Confidence { get; set; }

        /// <summary>
        /// Gets or sets the effective confidence contribution used in aggregation.
        /// </summary>
        public decimal Contribution { get; set; }

        /// <summary>
        /// Gets or sets the diagnostic area resolution.
        /// </summary>
        public DiagnosticAreaResolution DiagnosticAreaResolution { get; set; } = null!;

        /// <summary>
        /// Gets or sets the resolution rule.
        /// </summary>
        public DiagnosticAreaResolutionRule Rule { get; set; } = null!;

        /// <summary>
        /// Gets or sets the source mapping.
        /// </summary>
        public DiagnosticAreaResolutionMapping? Mapping { get; set; }
    }
}
