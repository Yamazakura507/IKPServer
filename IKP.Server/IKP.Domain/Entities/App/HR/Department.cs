using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.Files;

namespace IKP.Domain.Entities.App.HR;

public class Department : BaseAppLookupWithAudit, IHasIcon
{
    public Guid? ParentDepartmentId { get; set; }
    public Guid? HeadEmployeeId { get; set; }

    /// <inheritdoc />
    public Guid? FileId { get; set; }

    /// <inheritdoc />
    public StoredFile? File { get; set; }

    public Department? ParentDepartment { get; set; }
    public Employee? HeadEmployee { get; set; }
    public ICollection<Department> Children { get; set; } = new List<Department>();
}
