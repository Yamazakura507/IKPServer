using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Diagnostics.Definitions;

namespace IKP.Domain.Entities.Diagnostics.Areas
{
    /// <summary>
    /// Represents a functional area of the diagnostic subsystem.
    /// </summary>
    public class DiagnosticArea : FullLookupLocalized
    {
        /// <summary>
        /// Gets or sets the base importance score of the diagnostic area.
        /// </summary>
        public int ImportanceScore { get; set; }

        /// <summary>
        /// Gets or sets resolutions associated with this diagnostic area.
        /// </summary>
        public ICollection<DiagnosticAreaResolution> Resolutions { get; set; } = new List<DiagnosticAreaResolution>();

        /// <summary>
        /// Gets or sets configurable resolution mappings targeting this diagnostic area.
        /// </summary>
        public ICollection<DiagnosticAreaResolutionMapping> ResolutionMappings { get; set; } = new List<DiagnosticAreaResolutionMapping>();
    }
}
