using Employee_API.Model;

namespace Employee_API.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployee();
        Task<Employee> GetEmployeeById(int id);
        Task<int> CreateEmployee(Employee emp);
        Task<int> UpdateEmployee(int id, Employee emp);
        Task<int> DeleteEmployee(int id);
    }
}
