using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Files;

namespace IKP.Domain.Entities.Updates;

public sealed class UpdateReleaseNoteAttachment : Entity
{
    public Guid ReleaseNoteId { get; set; }
    public Guid FileId { get; set; }
    public int SortOrder { get; set; }
    public UpdateReleaseNote ReleaseNote { get; set; } = null!;
    public StoredFile File { get; set; } = null!;
}
