using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.App.HR.CompetencyMatrix
{
    public class CompetencyAssessmentItemValue : FullAudit
    {
        public decimal Value { get; set; }
        public string? Comment { get; set; }
    }
}
