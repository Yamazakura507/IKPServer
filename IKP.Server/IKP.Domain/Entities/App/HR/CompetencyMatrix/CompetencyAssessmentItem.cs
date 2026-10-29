using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.App.HR.CompetencyMatrix
{
    public class CompetencyAssessmentItem : FullAudit
    {
        public Guid AssessmentId { get; set; }
        public Guid MatrixItemId { get; set; }
        public Guid? ReferenceAssessmentValueId { get; set; }
        public Guid? ManagerAssessmentValueId { get; set; }
        public Guid? EmployeeAssessmentValueId { get; set; }

        public CompetencyAssessment Assessment { get; set; } = null!; 
        public CompetencyMatrixItem MatrixItem { get; set; } = null!;
        public CompetencyAssessmentItemValue? ReferenceAssessmentValue { get; set; }
        public CompetencyAssessmentItemValue? ManagerAssessmentValue { get; set; }
        public CompetencyAssessmentItemValue? EmployeeAssessmentValue { get; set; }
    }
}
