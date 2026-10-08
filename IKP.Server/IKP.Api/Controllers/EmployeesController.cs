using IKP.Domain.Entities.Employees;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext db;

        public EmployeesController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<Employee> employees = await db.Employees.AsNoTracking().OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToListAsync(cancellationToken);

            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            Employee? employee = await db.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return employee is null ? NotFound() : Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            Employee employee = new ()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                FamilyName = request.FamilyName
            };

            db.Employees.Add(employee);

            await db.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            Employee? employee = await db.Employees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (employee is null) return NotFound();

            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.FamilyName = request.FamilyName;

            await db.SaveChangesAsync(cancellationToken);

            return Ok(employee);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            Employee? employee = await db.Employees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (employee is null) return NotFound();

            db.Employees.Remove(employee);

            await db.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpPost("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
        {
            Employee? employee = await db.Employees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (employee is null) return NotFound();
            if (!employee.IsDeleted) return BadRequest("Employee is not deleted.");

            employee.IsDeleted = false;
            employee.DeletedAt = null;
            employee.DeletedBy = null;

            await db.SaveChangesAsync(cancellationToken);

            return Ok(employee);
        }
    }

    public sealed class CreateEmployeeRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FamilyName { get; set; } = null!;
    }

    public sealed class UpdateEmployeeRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FamilyName { get; set; } = null!;
    }
}
