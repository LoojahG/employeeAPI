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
        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            var result= await _employeeRepository.GetEmployee();
            return Ok(result);
        }
        // GET: api/Employee/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await _employeeRepository.GetEmployeeById(id);
            if(result == null)
            {
                return NotFound(new { message = "Employee not found" });
            }
            return Ok(result);
        }
        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee emp)
        {
            var result = await _employeeRepository.CreateEmployee(emp);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = emp.Id }, emp);
        }
        // PUT: api/Employee/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee emp)
        {
            var result = await _employeeRepository.UpdateEmployee(id,emp);
            return result>0
                ? Ok(new { message = "Employee Updated successfully" })
                : BadRequest(new { message = "Failed to update employee." });
        }
        // DELETE: api/Employee/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result=await _employeeRepository.DeleteEmployee(id);
            return result > 0
                ? Ok(new { message = "Employee Deleted successfully" })
                : BadRequest(new { message = "Failed to delete employee or Employee doesn\'t exist." });
        }
    }
}
