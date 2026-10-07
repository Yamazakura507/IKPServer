namespace IKP.Domain.Interfaces
{
    public interface IUserAuditEntity
    {
        Guid? CreatedBy { get; set; }

        Guid? UpdatedBy { get; set; }
    }
}
