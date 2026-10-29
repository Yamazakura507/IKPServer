using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.App.HR.Lookup;

namespace IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Attestation
{
    public class AttestationSection : FullAudit, IHasOrder
    {
        public Guid AttestationId { get; set; }
        public Guid AttestationSectionTypeId { get; set; }
        public string Title { get; set; } = null!;
        public int SortOrder { get; set; }

        public Attestation Attestation { get; set; } = null!;
        public AttestationSectionType SectionType { get; set; } = null!;
        public ICollection<AttestationQuestion> Questions { get; set; } = new List<AttestationQuestion>();
    }
}
