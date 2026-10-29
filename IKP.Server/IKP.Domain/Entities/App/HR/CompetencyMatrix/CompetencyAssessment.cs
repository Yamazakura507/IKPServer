using IKP.Domain.Common.Base;
using IKP.Domain.Entities.App.HR.Lookup;

namespace IKP.Domain.Entities.App.HR.CompetencyMatrix
{
    public class CompetencyAssessment : FullAudit
    {
        public Guid EmployeeId { get; set; }
        public Guid MatrixVersionId { get; set; }
        public DateTime AssessmentDate { get; set; }
        public Guid AssessorEmployeeId { get; set; }
        public Guid AssessmentTypeId { get; set; }
        public string? Notes { get; set; }

        public Employee Employee { get; set; } = null!; 
        public Employee AssessorEmployee { get; set; } = null!;
        public CompetencyAssessmentType AssessmentType { get; set; } = null!;
        public CompetencyMatrixVersion MatrixVersion { get; set; } = null!; 
        public ICollection<CompetencyAssessmentItem> Items { get; set; } = new List<CompetencyAssessmentItem>();
    }
}
