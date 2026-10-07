using IKP.Domain.Interfaces;

namespace IKP.Domain.Entities
{
    public class Employee : IEntity, IAuditableEntity, ISoftDeleteEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
