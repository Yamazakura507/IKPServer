using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Diagnostics.Areas;

namespace IKP.Domain.Entities.Diagnostics.Definitions
{
    /// <summary>
    /// Defines a rule used to resolve a diagnostic area.
    /// </summary>
    public class DiagnosticAreaResolutionRule : FullLookupLocalized
    {
        /// <summary>
        /// Gets or sets mappings that use this resolution rule.
        /// </summary>
        public ICollection<DiagnosticAreaResolutionMapping> Mappings { get; set; } = new List<DiagnosticAreaResolutionMapping>();

        /// <summary>
        /// Gets or sets resolution evidence records that use this rule.
        /// </summary>
        public ICollection<DiagnosticAreaResolutionEvidence> Evidences { get; set; } = new List<DiagnosticAreaResolutionEvidence>();
    }
}
