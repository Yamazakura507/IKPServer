using IKP.Domain.Common.Base;
using IKP.Domain.Entities.App.HR.Lookup;

namespace IKP.Domain.Entities.App.HR;

public class EmploymentHistory : ShortAudit
{
    public Guid EmployeeId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid PositionId { get; set; }
    public Guid? GradeId { get; set; }
    public Guid? ManagerEmployeeId { get; set; }
    public Guid? RecordTypeId { get; set; }
    
    public DateOnly ValidFrom { get; set; }
    public DateOnly? ValidTo { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }

    public Employee Employee { get; set; } = null!;
    public Department Department { get; set; } = null!;
    public Position Position { get; set; } = null!;
    public Grade? Grade { get; set; }
    public Employee? ManagerEmployee { get; set; }
    public EmploymentRecordType RecordType { get; set; } = null!;
}
