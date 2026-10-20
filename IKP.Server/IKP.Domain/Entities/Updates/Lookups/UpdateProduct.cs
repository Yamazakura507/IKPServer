using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Updates.Lookups;

public sealed class UpdateProduct : FullLookupLocalized
{
    public Guid CategoryId { get; set; }
    public Guid? PlatformId { get; set; }

    public UpdateCategory Category { get; set; } = null!;
    public UpdatePlatform? Platform { get; set; }
}
