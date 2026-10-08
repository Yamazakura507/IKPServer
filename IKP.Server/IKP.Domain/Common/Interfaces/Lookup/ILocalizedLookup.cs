using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Common.Interfaces.Localized;

namespace IKP.Domain.Common.Interfaces.Lookup
{
    public interface ILocalizedLookup : IHasLocalizedName, IHasLocalizedDescription, IHasIcon
    {
    }
}
