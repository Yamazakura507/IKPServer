using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;

namespace IKP.Domain.Entities.App.HR.Lookup
{
    public class DevelopmentNeeds : FullAudit, IHasName
    {
        /// <inheritdoc />
        public string Name { get; set; } = null!;

        public Guid? TypeId { get; set; }

        public DevelopmentItemType? DevelopmentItemType { get; set; }
    }
}
