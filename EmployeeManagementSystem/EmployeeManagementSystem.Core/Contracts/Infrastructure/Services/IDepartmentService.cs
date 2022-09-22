using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Core.Contracts.Infrastructure.Services
{
    public interface  IDepartmentService
    {
        Task<Department> CreateAsync(Department department);
        Task DeleteDepartmentAsync(int departmentId);
        Task<DepartmentDto> GetDepartmentByNameAsync(string departmentName);
        Task<DepartmentDto> GetDepartmentAsync(int departmentId);
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
        DepartmentDto UpdateAsync(int departmentId, DepartmentDto department, Department departmentToBeUpdate);
        Task<IEnumerable<EmployeeDto>> GetEmpWorkingInDept(int deptId);
        Task<int> EmployeeInDepsrtmentCount(int deptId);
    }
}
