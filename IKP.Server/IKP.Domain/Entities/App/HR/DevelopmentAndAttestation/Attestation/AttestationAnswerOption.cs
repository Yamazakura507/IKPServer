using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;

namespace IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Attestation
{
    public class AttestationAnswerOption : FullAudit, IHasOrder
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = null!; 
        public bool IsCorrect { get; set; }
        public int SortOrder { get; set; }
        public AttestationQuestion Question { get; set; } = null!;
    }
}
