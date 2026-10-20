using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;

namespace IKP.Domain.Entities.Updates;

public sealed class UpdateReleaseNote : Entity, IHasDescription, IHasOrder
{
    public Guid VersionId { get; set; }
    public int SortOrder { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsPlanned { get; set; }
    public bool IsHtmlDescription { get; set; } = false;

    public UpdateVersion Version { get; set; } = null!;
    public ICollection<UpdateReleaseNoteAttachment> Attachments { get; set; } = new List<UpdateReleaseNoteAttachment>();
}
