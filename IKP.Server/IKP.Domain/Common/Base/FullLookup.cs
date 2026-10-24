using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Common.Interfaces.Lookup;
using IKP.Domain.Entities.Files;

namespace IKP.Domain.Common.Base
{
    public abstract class FullLookup : Entity, IHasCode, ILookup, IHasOrder, IHasActiveState, IHasSystemState
    {
        /// <inheritdoc />
        public string Code { get; set; } = null!;

        /// <inheritdoc />
        public string Name { get; set; } = null!;

        /// <inheritdoc />
        public string? Description { get; set; }

        /// <inheritdoc />
        public Guid? FileId { get; set; }

        /// <inheritdoc />
        public int SortOrder { get; set; }

        /// <inheritdoc />
        public bool IsActive { get; set; }

        /// <inheritdoc />
        public bool IsSystem { get; set; }

        /// <inheritdoc />
        public StoredFile? File { get; set; }
    }
}
