using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Core.Contracts.Infrastructure.Services
{
    public interface  IDepartmentService
    {
        //Task CreateRangeAsync(IEnumerable<Department> departments);
        Task<Department> CreateAsync(Department department);
        Task DeleteDepartmentAsync(int departmentId);
        Task<DepartmentDto> GetDepartmentAsync(int departmentId);
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
        Task<bool> ApproveLeaveApplication(int empId, int leaveId);
        Task<Department> UpdateAsync(int departmentId, Department department);
    }
}
