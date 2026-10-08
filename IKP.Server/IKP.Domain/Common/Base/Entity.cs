using IKP.Domain.Common.Interfaces;

namespace IKP.Domain.Common.Base
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
