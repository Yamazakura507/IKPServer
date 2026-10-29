using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.Files;

namespace IKP.Domain.Entities.App.HR.Lookup
{
    public class CompetencyAssessmentType : FullAudit, IHasName, IHasIcon
    {
        /// <inheritdoc />
        public string Name { get; set; } = null!;

        /// <inheritdoc />
        public Guid? FileId { get; set; }

        /// <inheritdoc />
        public StoredFile? File { get; set; }
    }
}
