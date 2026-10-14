using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Domain.Entities.Diagnostics.Errors;

namespace IKP.Domain.Entities.Diagnostics.Areas
{
    /// <summary>
    /// Describes how a diagnostic area was resolved.
    /// </summary>
    public class DiagnosticAreaResolution : Entity
    {
        /// <summary>
        /// Gets or sets the identifier of the resolved diagnostic area.
        /// </summary>
        public Guid? DiagnosticAreaId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the resolution source.
        /// </summary>
        public Guid? SourceId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the resolution rule.
        /// </summary>
        public Guid? RuleId { get; set; }

        /// <summary>
        /// Gets or sets the confidence of the resolution.
        /// </summary>
        public decimal Confidence { get; set; }

        /// <summary>
        /// Gets or sets the version of the resolution algorithm.
        /// </summary>
        public int Version { get; set; }


        public DiagnosticArea? DiagnosticArea { get; set; }

        /// <summary>
        /// Gets or sets the resolution source.
        /// </summary>
        public DiagnosticAreaResolutionSource? Source { get; set; }

        /// <summary>
        /// Gets or sets the resolution rule.
        /// </summary>
        public DiagnosticAreaResolutionRule? Rule { get; set; }


        /// <summary>
        /// Gets or sets error groups referencing this resolution.
        /// </summary>
        public ICollection<ErrorGroup> ErrorGroups { get; set; } = new List<ErrorGroup>();

        /// <summary>
        /// Gets or sets error occurrences referencing this resolution.
        /// </summary>
        public ICollection<ErrorOccurrence> ErrorOccurrences { get; set; } = new List<ErrorOccurrence>();

        /// <summary>
        /// Gets or sets the evidence supporting this resolution.
        /// </summary>
        public ICollection<DiagnosticAreaResolutionEvidence> Evidences { get; set; } = new List<DiagnosticAreaResolutionEvidence>();
    }
}
