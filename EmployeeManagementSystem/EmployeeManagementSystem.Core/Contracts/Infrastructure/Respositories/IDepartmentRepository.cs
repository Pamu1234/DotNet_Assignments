using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Repositories.EntityFramework
{
    public interface IDepartmentRepository
    {
        Task<Department> CreateAsync(Department department);
        Task DeleteDepartmentAsync(int departmentId);
        Task<DepartmentDto> GetDepartmentByNameAsync(string departmentName);
        Task<DepartmentDto> GetDepartmentAsync(int departmentId);
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
        Task<Department> UpdateAsync(DepartmentDto updatedDepartment);
        Task<IEnumerable<EmployeeDto>> GetTotalNoOfEmpWorkingInEachDept(int deptId);
        
    }
}