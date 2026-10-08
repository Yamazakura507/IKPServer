namespace IKP.Domain.Common.Interfaces.Audit
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedAt { get; set; }

        Guid? DeletedBy { get; set; }
    }
}
