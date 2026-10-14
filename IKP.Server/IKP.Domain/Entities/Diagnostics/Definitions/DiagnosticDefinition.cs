using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Diagnostics.Definitions
{
    /// <summary>
    /// Represents a diagnostic definition belonging to a definition group.
    /// </summary>
    public class DiagnosticDefinition : FullLookupLocalized
    {
        /// <summary>
        /// Gets or sets the identifier of the definition group.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Gets or sets the definition group.
        /// </summary>
        public DiagnosticDefinitionGroup Group { get; set; } = null!;
    }
}
