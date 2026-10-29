using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;

namespace IKP.Domain.Entities.App.HR.Lookup
{
    public class DevelopmentMethods : FullAudit, IHasName
    {
        /// <inheritdoc />
        public string Name { get; set; } = null!;

        public bool IsControl { get; set; }
    }
}
