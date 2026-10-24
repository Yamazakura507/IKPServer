using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Audit;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.Files;
using IKP.Domain.Entities.Updates.Lookups;

namespace IKP.Domain.Entities.Updates;

public sealed class UpdateVersion : Entity, IHasFile, IAuditable
{
    public Guid ProductId { get; set; }
    public Guid? FileId { get; set; }
    public string Version { get; set; } = null!;
    public string? ContentHash { get; set; }
    public long? Size { get; set; }
    public bool IsPrerelease { get; set; }
    public bool IsPublished { get; set; }
    public bool IsRequired { get; set; }
    public DateTime? ReleaseAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }

    public UpdateProduct Product { get; set; } = null!;
    public StoredFile? File { get; set; } = null;
}
