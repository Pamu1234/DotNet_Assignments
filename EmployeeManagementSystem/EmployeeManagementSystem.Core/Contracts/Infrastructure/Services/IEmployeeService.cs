using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Core.Contracts.Infrastructure.Services
{
    public interface IEmployeeService
    {
        Task<Employee> CreateAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
        Task<IEnumerable< EmployeeDto>> GetEmployeeAsync(int employeeId);
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<Employee> UpdateAsync(int employeeId, Employee employee);
        Task<Attendance> EmployeeLogin(int empId);
        //Task<Attendance> EmployeeLogout(int empId);
        Task<Attendance?> EmployeeLogout(int empId);


    }
}
