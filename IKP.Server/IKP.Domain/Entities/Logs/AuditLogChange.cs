using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Logs
{
    public class AuditLogChange : Entity
    {
        public Guid AuditLogId { get; set; }

        public AuditLog AuditLog { get; set; } = null!;

        public string PropertyName { get; set; } = null!;

        public string PropertyType { get; set; } = null!;

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }
    }
}
