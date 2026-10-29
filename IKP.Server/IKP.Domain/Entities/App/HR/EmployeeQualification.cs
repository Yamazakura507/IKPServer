using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.Files;

namespace IKP.Domain.Entities.App.HR;

public class EmployeeQualification : FullAudit, IHasName, IHasFile
{
    public Guid EmployeeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Issuer { get; set; }
    public DateOnly? IssuedAt { get; set; }
    public DateOnly? ExpiresAt { get; set; }
    public string? Number { get; set; }
    public Guid? FileId { get; set; }

    public Employee Employee { get; set; } = null!;
    public StoredFile? File { get; set; }
}
