using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;

namespace IKP.Domain.Entities.Updates;

public sealed class UpdateDownloadLink : Entity, IHasActiveState
{
    public Guid VersionId { get; set; }
    public string Url { get; set; } = null!;
    public string? Name { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsActive { get; set; } = true;
    public UpdateVersion Version { get; set; } = null!;
}
