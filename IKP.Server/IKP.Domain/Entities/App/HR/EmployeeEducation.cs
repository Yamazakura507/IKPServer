using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.App.HR.Lookup;
using IKP.Domain.Entities.Files;

namespace IKP.Domain.Entities.App.HR;

public class EmployeeEducation : FullAudit, IHasFile
{
    public Guid EmployeeId { get; set; }
    public Guid EducationTypeId { get; set; }
    public string Organization { get; set; } = null!;
    public string? Faculty { get; set; }
    public string? Speciality { get; set; }
    public string? Qualification { get; set; }
    public int? GraduationYear { get; set; }
    public DateOnly? BeginOfDate { get; set; }
    public DateOnly? EndOfDate { get; set; }
    public string? ProofOfDocumentNumber { get; set; }
    public Guid? FileId { get; set; }

    public Employee Employee { get; set; } = null!;
    public EducationType EducationType { get; set; } = null!;
    public StoredFile? File { get; set; }
}
