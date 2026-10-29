using IKP.Domain.Common.Base;
using IKP.Domain.Entities.App.HR.Lookup;

namespace IKP.Domain.Entities.App.HR;

public class EmployeeContact : FullAudit
{
    public Guid EmployeeId { get; set; }
    public Guid EmployeeContactTypeId { get; set; }
    public string Value { get; set; } = null!;
    public string? Label { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsVerified { get; set; }
    public DateTime? VerifiedAt { get; set; }

    public Employee Employee { get; set; } = null!;
    public EmployeeContactType? Type { get; set; }
}
