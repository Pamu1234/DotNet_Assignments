using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Repositories.EntityFramework
{
    public interface IDepartmentRepository
    {
        Task<Department> CreateAsync(Department department);
        Task DeleteDepartmentAsync(int departmentId);
        Task<DepartmentDto> GetDepartmentAsync(int departmentId);
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
        Task<Department> UpdateAsync(int departmentId, Department department);
        public  Task<IEnumerable<EmployeeDto>> GetTotalNoOfEmpWorkingInEachDept(int deptId);
    }
}