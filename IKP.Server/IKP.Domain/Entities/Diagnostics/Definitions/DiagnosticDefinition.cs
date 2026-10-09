using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Common.Interfaces.Lookup;

namespace IKP.Domain.Entities.Diagnostics.Definitions
{
    /// <summary>
    /// Represents a diagnostic definition belonging to a definition group.
    /// </summary>
    public class DiagnosticDefinition : Entity, IHasCode, ILocalizedLookup, IHasOrder, IHasActiveState
    {
        /// <summary>
        /// Gets or sets the identifier of the definition group.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Gets or sets the definition group.
        /// </summary>
        public DiagnosticDefinitionGroup Group { get; set; } = null!;

        /// <summary>
        /// Gets or sets the stable system code of the definition.
        /// </summary>
        public string Code { get; set; } = null!;

        /// <summary>
        /// Gets or sets the localization key for the definition name.
        /// </summary>
        public string NameKey { get; set; } = null!;

        /// <summary>
        /// Gets or sets the localization key for the definition description.
        /// </summary>
        public string? DescriptionKey { get; set; }

        /// <summary>
        /// Gets or sets the optional file identifier used for the definition icon.
        /// </summary>
        public Guid? FileId { get; set; }

        /// <summary>
        /// Gets or sets the display order of the definition.
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the definition is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the definition is a system definition.
        /// </summary>
        public bool IsSystem { get; set; }
    }
}
