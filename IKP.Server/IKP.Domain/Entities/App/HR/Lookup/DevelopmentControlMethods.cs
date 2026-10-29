using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;

namespace IKP.Domain.Entities.App.HR.Lookup
{
    public class DevelopmentControlMethods : FullAudit, IHasName
    {
        public string Name { get; set; } = null!;
    }
}
