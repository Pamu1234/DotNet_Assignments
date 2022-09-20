using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
        Task<IEnumerable< EmployeeDto>> GetEmployeeAsync(int employeeId);
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<Employee> UpdateAsync(int employeeId, Employee employee);
        public DateTime EmployeeLogin(int empId);
    }
}