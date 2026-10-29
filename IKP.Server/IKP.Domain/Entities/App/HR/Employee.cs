using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.Auth.Users;
using IKP.Domain.Entities.Files;

namespace IKP.Domain.Entities.App.HR;

/// <summary>
/// HR person record. Authentication identity is deliberately separate.
/// </summary>
public class Employee : FullAudit, IHasIcon
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? FamilyName { get; set; } = null!;
    public string ShortName { get; set; } = null!;
    public string? NickName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public DateOnly? HireDate { get; set; }
    public Guid? UserId { get; set; }
    public Guid? FileId { get; set; }

    public AuthUser? User { get; set; }
    public StoredFile? File { get; set; }
    public ICollection<EmploymentHistory> EmploymentHistory { get; set; } = new List<EmploymentHistory>();
    public ICollection<EmployeeContact> Contacts { get; set; } = new List<EmployeeContact>();
    public ICollection<EmployeeEducation> Education { get; set; } = new List<EmployeeEducation>();
    public ICollection<EmployeeQualification> Qualifications { get; set; } = new List<EmployeeQualification>();
}
