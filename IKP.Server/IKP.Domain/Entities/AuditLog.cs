using IKP.Domain.Interfaces;

namespace IKP.Domain.Entities
{
    public class AuditLog : IEntity
    {
        public Guid Id { get; set; }

        public string EntityName { get; set; } = null!;

        public Guid EntityId { get; set; }

        public string Action { get; set; } = null!;

        public Guid? UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? OldValues { get; set; }

        public string? NewValues { get; set; }
    }
}
