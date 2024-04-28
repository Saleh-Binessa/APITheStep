using APITheStep.Models.DB;
using APITheStep.Models.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APITheStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly BankContext _context;

        public EmployeeController(BankContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<EmployeeResponse>> GetAllEmployees()
        {
            var employees = _context.Employees.Select(e => new EmployeeResponse
            {
                Id = e.Id,
                Name = e.Name,
                CivilId = e.CivilId,
                Position = e.Position,
                WorkplaceId = e.WorkplaceId 
            }).ToList();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<EmployeeResponse> GetEmployeeDetails(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            var response = new EmployeeResponse
            {
                Id = id,
                Name = employee.Name,
                CivilId = employee.CivilId,
                Position = employee.Position,
                WorkplaceId = employee.WorkplaceId 
            };
            return Ok(response);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddEmployee(AddEmployeeRequest request)
        {
            var employee = new Employee()
            {
                Name = request.Name,
                CivilId = request.CivilId,
                Position = request.Position,
                WorkplaceId = request.WorkplaceId
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetEmployeeDetails), new { id = employee.Id });
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult EditEmployee(int id, AddEmployeeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Name = request.Name;
            employee.CivilId = request.CivilId;
            employee.Position = request.Position;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return NoContent(); 
        }
    }
}

