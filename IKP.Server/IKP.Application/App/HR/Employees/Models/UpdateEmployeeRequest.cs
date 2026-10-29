namespace IKP.Application.App.HR.Employees.Models
{
    public sealed class UpdateEmployeeRequest
    {
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string? FamilyName { get; init; } = null!;
        public string? NickName { get; init; }
        public DateOnly? DateOfBirth { get; init; }
        public DateOnly? HireDate { get; init; }
        public Guid? UserId { get; init; }
        public Guid? PhotoFileId { get; init; }
    }
}
