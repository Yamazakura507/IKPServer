using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.Files;

namespace IKP.Domain.Entities.App.HR.Lookup
{
    public class EmploymentRecordType : BaseAppLookupWithAudit, IHasIcon
    {
        /// <inheritdoc />
        public Guid? FileId { get; set; }

        /// <inheritdoc />
        public StoredFile? File { get; set; }
    }
}
