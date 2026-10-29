using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Common.Interfaces.Lookup;

namespace IKP.Domain.Common.Base
{
    public abstract class BaseAppLookupWithAudit : FullAudit, IShortLookup, IHasOrder, IHasActiveState
    {
        /// <inheritdoc />
        public string Name { get; set; } = null!;

        /// <inheritdoc />
        public string? Description { get; set; }

        /// <inheritdoc />
        public int SortOrder { get; set; }

        /// <inheritdoc />
        public bool IsActive { get; set; }
    }
}
