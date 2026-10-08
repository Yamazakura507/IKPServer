using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Logs
{
    public class AuditLog : Entity
    {
        public string EntitySchema { get; set; } = null!;

        public string EntityType { get; set; } = null!;

        public Guid EntityId { get; set; }

        public Guid ActionId { get; set; }

        public Guid? UserId { get; set; }

        public DateTime CreatedAt { get; set; }


        public AuditActionDefinition Action { get; set; } = null!;

        public ICollection<AuditLogChange> Changes { get; set; } = new List<AuditLogChange>();
    }
}
