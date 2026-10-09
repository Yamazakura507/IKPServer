using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Common.Interfaces.Lookup;

namespace IKP.Domain.Entities.Diagnostics.Definitions
{
    /// <summary>
    /// Represents a group of diagnostic definitions.
    /// </summary>
    public class DiagnosticDefinitionGroup : Entity, IHasCode, ILocalizedLookup, IHasOrder, IHasActiveState
    {
        /// <summary>
        /// Gets or sets the stable system code of the definition group.
        /// </summary>
        public string Code { get; set; } = null!;

        /// <summary>
        /// Gets or sets the localization key for the group name.
        /// </summary>
        public string NameKey { get; set; } = null!;

        /// <summary>
        /// Gets or sets the localization key for the group description.
        /// </summary>
        public string? DescriptionKey { get; set; }

        /// <summary>
        /// Gets or sets the optional file identifier used for the group icon.
        /// </summary>
        public Guid? FileId { get; set; }

        /// <summary>
        /// Gets or sets the display order of the group.
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the group is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the group is a system definition.
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// Gets or sets the definitions belonging to this group.
        /// </summary>
        public ICollection<DiagnosticDefinition> Definitions { get; set; } = new List<DiagnosticDefinition>();
    }
}
