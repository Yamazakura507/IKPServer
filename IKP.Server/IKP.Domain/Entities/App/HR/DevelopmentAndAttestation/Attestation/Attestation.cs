using IKP.Domain.Common.Base;
using IKP.Domain.Entities.App.HR.CompetencyMatrix;

namespace IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Attestation
{
    public class Attestation : FullAudit
    {
        public Guid EmployeeId { get; set; }
        public Guid? ManagerEmployeeId { get; set; }
        public Guid MatrixAssessmentId { get; set; }
        public string Title { get; set; } = null!; 
        public DateOnly? PlannedDate { get; set; }
        public DateOnly? CompletedDate { get; set; }
        public decimal? FinalScore { get; set; }
        public string? FinalComment { get; set; }

        public Employee Employee { get; set; } = null!; 
        public Employee? ManagerEmployee { get; set; }
        public CompetencyAssessment MatrixAssessment { get; set; } = null!; 
        public ICollection<AttestationSection> Sections { get; set; } = new List<AttestationSection>();
    }
}
