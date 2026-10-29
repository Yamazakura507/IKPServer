using IKP.Application.App.HR.Employees.Models;

namespace IKP.Application.App.HR.Employees.Interfaces;

public interface IEmployeeService
{
    ValueTask<EmployeeDetailsResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<EmployeeDetailsResponse> CreateAsync(CreateEmployeeRequest request, CancellationToken cancellationToken = default);
    ValueTask<EmployeeDetailsResponse> UpdateAsync(Guid id, UpdateEmployeeRequest request, CancellationToken cancellationToken = default);
    ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask RestoreAsync(Guid id, CancellationToken cancellationToken = default);
}
