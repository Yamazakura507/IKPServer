using IKP.Domain.Common.Interfaces.Audit;

namespace IKP.Domain.Common.Base
{
    public abstract class FullAudit : ShortAudit, ISoftDelete
    {
        /// <inheritdoc />
        public bool IsDeleted { get; set; }

        /// <inheritdoc />
        public DateTime? DeletedAt { get; set; }

        /// <inheritdoc />
        public Guid? DeletedBy { get; set; }
    }
}
