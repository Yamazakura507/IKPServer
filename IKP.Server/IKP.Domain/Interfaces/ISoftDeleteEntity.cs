namespace IKP.Domain.Interfaces
{
    public interface ISoftDeleteEntity
    {
        bool IsDeleted { get; set; }
    }
}
