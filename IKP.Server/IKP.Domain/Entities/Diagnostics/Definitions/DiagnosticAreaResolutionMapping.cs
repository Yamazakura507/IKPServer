using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.Diagnostics.Areas;

namespace IKP.Domain.Entities.Diagnostics.Definitions
{
    /// <summary>
    /// Defines a configurable mapping between a diagnostic area and a resolution rule.
    /// </summary>
    public class DiagnosticAreaResolutionMapping : Entity, IHasActiveState, IHasSystemState
    {
        /// <summary>
        /// Gets or sets the diagnostic area identifier.
        /// </summary>
        public Guid DiagnosticAreaId { get; set; }

        /// <summary>
        /// Gets or sets the resolution rule identifier.
        /// </summary>
        public Guid RuleId { get; set; }

        /// <summary>
        /// Gets or sets the regular expression pattern used to match a diagnostic value.
        /// </summary>
        public string Pattern { get; set; } = null!;

        /// <summary>
        /// Gets or sets the rule priority from 0 to 100.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the base confidence from 0 to 100.
        /// </summary>
        public decimal Confidence { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mapping is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mapping is system-defined.
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// Gets or sets the diagnostic area associated with the mapping.
        /// </summary>
        public DiagnosticArea DiagnosticArea { get; set; } = null!;

        /// <summary>
        /// Gets or sets the resolution rule associated with the mapping.
        /// </summary>
        public DiagnosticAreaResolutionRule Rule { get; set; } = null!;
    }
}
