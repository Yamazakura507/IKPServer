using IKP.Application.App.HR.Employees.Interfaces;
using IKP.Application.App.HR.Employees.Models;
using IKP.Domain.Entities.App.HR;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.App.HR.Employees;

public sealed class EmployeeService(AppDbContext db) : IEmployeeService
{
    public async ValueTask<EmployeeDetailsResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default) =>
        await db.Employees.AsNoTracking().Where(x => x.Id == id && !x.IsDeleted).Select(x => new EmployeeDetailsResponse
        {
            Id = x.Id, 
            FirstName = x.FirstName, 
            LastName = x.LastName, 
            FamilyName = x.FamilyName,
            NickName = x.NickName,
            ShortName = x.ShortName,
            DateOfBirth = x.DateOfBirth, 
            HireDate = x.HireDate, 
            UserId = x.UserId, 
            PhotoFileId = x.FileId
        }).SingleOrDefaultAsync(cancellationToken);

    public async ValueTask<EmployeeDetailsResponse> CreateAsync(CreateEmployeeRequest request, CancellationToken cancellationToken = default)
    {
        Employee employee = new ()
        {
            FirstName = Required(request.FirstName, nameof(request.FirstName)), 
            LastName = Required(request.LastName, nameof(request.LastName)), 
            FamilyName = request.FamilyName?.Trim(),
            ShortName = FormatToInitials(request.LastName, request.FirstName, request.FamilyName),
            NickName = request.NickName,
            DateOfBirth = request.DateOfBirth, 
            HireDate = request.HireDate, 
            UserId = request.UserId, 
            FileId = request.PhotoFileId
        };

        db.Employees.Add(employee); 
        await db.SaveChangesAsync(cancellationToken); 

        return (await GetAsync(employee.Id, cancellationToken))!;
    }

    public async ValueTask<EmployeeDetailsResponse> UpdateAsync(Guid id, UpdateEmployeeRequest request, CancellationToken cancellationToken = default)
    {
        Employee employee = await db.Employees.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken) ?? throw new KeyNotFoundException();

        employee.FirstName = Required(request.FirstName, nameof(request.FirstName)); 
        employee.LastName = Required(request.LastName, nameof(request.LastName)); 
        employee.FamilyName = request.FamilyName?.Trim();
        employee.ShortName = FormatToInitials(request.LastName, request.FirstName, request.FamilyName);
        employee.NickName = request.NickName;
        employee.DateOfBirth = request.DateOfBirth; 
        employee.HireDate = request.HireDate; 
        employee.UserId = request.UserId; 
        employee.FileId = request.PhotoFileId;

        await db.SaveChangesAsync(cancellationToken); 

        return (await GetAsync(id, cancellationToken))!;
    }

    public async ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Employee employee = await db.Employees.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken) ?? throw new KeyNotFoundException(); 

        employee.IsDeleted = true;
        employee.DeletedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask RestoreAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Employee employee = await db.Employees.SingleOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException();

        if (!employee.IsDeleted) throw new InvalidOperationException("Employee is not deleted."); 
        
        employee.IsDeleted = false; 
        employee.DeletedAt = null; 
        employee.DeletedBy = null; 
        
        await db.SaveChangesAsync(cancellationToken);
    }

    private static string Required(string value, string name) => string.IsNullOrWhiteSpace(value) ? throw new ArgumentException($"{name} is required.") : value.Trim();

    private static string FormatToInitials(string lastName, string firstName, string? familyName)
    {
        lastName = lastName?.Trim() ?? string.Empty;

        string firstInitials = GetInitialsForCompoundName(firstName);
        string familyInitials = GetInitialsForCompoundName(familyName);
        string result = lastName;

        if (!string.IsNullOrEmpty(firstInitials)) result += " " + firstInitials;
        if (!string.IsNullOrEmpty(familyInitials)) result += " " + familyInitials;

        return result;
    }

    private static string GetInitialsForCompoundName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return string.Empty;

        string[] parts = name.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        IEnumerable<string> initials = parts.Select(p => $"{char.ToUpper(p[0])}.");

        return string.Join("-", initials);
    }
}
