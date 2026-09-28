using Employee_API.DTOs;
using Employee_API.Interface;
using Employee_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Employee_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: /Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployee()
        {
            var employees = await _employeeRepository.GetEmployee();

            var result = employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Age = e.Age
            });

            return Ok(result);
        }

        // GET: /Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            var emp = await _employeeRepository.GetEmployeeById(id);
            if (emp == null)
                return NotFound(new { message = "Employee not found" });

            return Ok(new EmployeeDto { Id = emp.Id, Name = emp.Name, Age = emp.Age });
        }

        // POST: /Employee
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(CreateEmployeeDto dto)
        {
            var emp = new Employee { Name = dto.Name, Age = dto.Age };

            var newId = await _employeeRepository.CreateEmployee(emp);

            var created = new EmployeeDto { Id = newId, Name = emp.Name, Age = emp.Age };
            return CreatedAtAction(nameof(GetEmployeeById), new { id = newId }, created);
        }

        // PUT: /Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDto dto)
        {
            var emp = new Employee { Id = id, Name = dto.Name, Age = dto.Age };

            var result = await _employeeRepository.UpdateEmployee(id, emp);
            return result > 0
                ? NoContent()
                : NotFound(new { message = "Employee not found" });
        }

        // DELETE: /Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeRepository.DeleteEmployee(id);
            return result > 0
                ? NoContent()
                : NotFound(new { message = "Employee not found" });
        }
    }
}