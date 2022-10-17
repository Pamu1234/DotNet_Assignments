using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee>DeleteEmployeeAsync(int employeeId);
        Task< EmployeeDto> GetEmployeeAsync(int employeeId);
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<Employee> UpdateAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(int empId);
        public DateTime EmployeeLogin(int empId);
        Task<Employee> GetUserDetails(string email);
    }
}