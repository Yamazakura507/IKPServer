using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.App.HR.Lookup;

namespace IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Attestation
{
    public class AttestationQuestion : FullAudit, IHasOrder
    {
        public Guid SectionId { get; set; }
        public Guid QuestionTypeId { get; set; }
        public int SortOrder { get; set; }
        public string Text { get; set; } = null!; 
        public bool IsAiDraft { get; set; }
        public bool IsApproved { get; set; }
        public bool IsOralAnswer { get; set; }
        public bool IsSelfWriteAnswer { get; set; }
        public string? AiPromptContext { get; set; }
        public decimal? MaxScore { get; set; }

        public AttestationQuestionType QuestionType { get; set; } = null!;
        public AttestationSection Section { get; set; } = null!; 
        public ICollection<AttestationAnswerOption> Options { get; set; } = new List<AttestationAnswerOption>();
    }
}
