using IKP.Domain.Common.Interfaces.Audit;

namespace IKP.Domain.Common.Base
{
    public abstract class ShortAudit : Entity, IAuditable
    {
        /// <inheritdoc />
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc />
        public Guid? CreatedBy { get; set; }

        /// <inheritdoc />
        public DateTime? UpdatedAt { get; set; }

        /// <inheritdoc />
        public Guid? UpdatedBy { get; set; }
    }
}
