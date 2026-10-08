using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Common.Interfaces.Lookup;

namespace IKP.Domain.Entities.Logs
{
    public class AuditActionDefinition : Entity, ILocalizedLookup, IHasCode, IHasOrder, IHasActiveState
    {
        public string Code { get; set; } = null!;

        public string NameKey { get; set; } = null!;

        public string? DescriptionKey { get; set; }

        public bool IsSystem { get; set; }

        public bool IsActive { get; set; }

        public int SortOrder { get; set; }

        public Guid? FileId { get; set; }
    }
}
