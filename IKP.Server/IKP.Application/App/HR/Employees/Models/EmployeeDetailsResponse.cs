namespace IKP.Application.App.HR.Employees.Models
{
    public sealed class EmployeeDetailsResponse : EmployeeListItem
    {
        public Guid? PhotoFileId { get; init; }
    }
}
