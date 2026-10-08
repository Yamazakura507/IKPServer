using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Audit;

namespace IKP.Domain.Entities.Employees
{
    public class Employee : Entity, IAuditable, ISoftDelete
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string FamilyName { get; set; } = null!;

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
