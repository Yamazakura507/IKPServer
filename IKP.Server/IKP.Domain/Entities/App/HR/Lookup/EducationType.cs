using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Common.Interfaces.Lookup;
using IKP.Domain.Entities.Files;

namespace IKP.Domain.Entities.App.HR.Lookup
{
    public class EducationType : FullAudit, IShortLookup, IHasIcon
    {
        /// <inheritdoc />
        public string Name { get; set; } = null!;

        /// <inheritdoc />
        public string? Description { get; set; }

        /// <inheritdoc />
        public Guid? FileId { get; set; }

        /// <inheritdoc />
        public StoredFile? File { get; set; }
    }
}
