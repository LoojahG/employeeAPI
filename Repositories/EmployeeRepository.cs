using Employee_API.Data;
using Employee_API.Interface;
using Employee_API.Model;
using Dapper;

namespace Employee_API.Repositories
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly DapperContext _context;
        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            var sql = "SELECT * FROM Employee";
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee>(sql);
                return employees;
            }
        }
        public async Task<Employee> GetEmployeeById(int id)
        {
            var sql = "SELECT * FROM Employee where Id=@id";
            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QuerySingleOrDefaultAsync<Employee>(sql, new { Id = id });
                
                return employee;
            }
        }
        public async Task<int> CreateEmployee(Employee emp)
        {
            var sql = "INSERT INTO Employee (Id, Name, Age) VALUES (@Id,@Name, @Age)";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { emp.Id, emp.Name, emp.Age });
                return result;
            }
        }
        public async Task<int> UpdateEmployee(int id, Employee emp)
        {
            var sql = "UPDATE Employee SET Name=@Name, Age=@Age WHERE Id=@Id";
            using (var connection= _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { emp.Id, emp.Name, emp.Age });
                return result;
            }
        }
        public async Task<int> DeleteEmployee(int id)
        {
            var sql = "DELETE FROM Employee WHERE Id=@Id";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
    }
}
