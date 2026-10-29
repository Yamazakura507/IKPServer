namespace IKP.Application.App.HR.Employees.Models
{
    public class EmployeeListItem
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string? FamilyName { get; init; } = null!;
        public string ShortName { get; init; } = null!;
        public string? NickName { get; init; }
        public DateOnly? DateOfBirth { get; init; }
        public DateOnly? HireDate { get; init; }
        public Guid? UserId { get; init; }
    }
}
