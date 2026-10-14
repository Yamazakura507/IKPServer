using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Diagnostics.Definitions
{
    /// <summary>
    /// Represents a group of diagnostic definitions.
    /// </summary>
    public class DiagnosticDefinitionGroup : FullLookupLocalized
    {
        /// <summary>
        /// Gets or sets the definitions belonging to this group.
        /// </summary>
        public ICollection<DiagnosticDefinition> Definitions { get; set; } = new List<DiagnosticDefinition>();
    }
}
