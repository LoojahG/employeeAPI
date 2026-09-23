using Microsoft.AspNetCore.Mvc;
using Dapper;
using Employee_API.Data;
using Employee_API.Model;

namespace Employee_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly DapperContext _context;
        public EmployeeController(DapperContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            var sql = "SELECT * FROM Employee";
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync(sql);
                if (!employees.Any())
                {
                    return NoContent();
                }
                return Ok(employees);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var sql = "SELECT * FROM Employee where Id=@id";
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync(sql, new { Id = id });
                if (!employees.Any())
                {
                    return NoContent();
                }
                return Ok(employees);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee emp)
        {
            var sql = "INSERT INTO Employee (Id, Name, Age) VALUES (@Id,@Name, @Age)";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { emp.Id, emp.Name, emp.Age });
                if (result > 0)
                {
                    return Ok(new { message = "Employee created successfully" });
                }
                return BadRequest(new { message = "Failed to create employee" });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee emp)
        {
            var sql = "UPDATE Employee SET Name=@Name, Age=@Age WHERE Id=@Id";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { emp.Id, emp.Name, emp.Age });
                if (result > 0)
                {
                    return Ok(new { message = "Employee updated successfully" });
                }
                return BadRequest(new { message = "Failed to update employee" });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var sql = "DELETE FROM Employee WHERE Id=@Id";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                if (result > 0)
                {
                    return Ok(new { message = "Employee deleted successfully" });
                }
                return BadRequest(new { message = "Failed to delete employee" });
            }
        }
    }
}
