using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories.EntityFramework;

namespace EmployeeManagementSystem.Infrastructure.Services
{
    public class DepartmentServices : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentServices(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<Department> CreateAsync(Department department)
        {
            return _departmentRepository.CreateAsync(department);
        }

        public Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
        {
            var result = _departmentRepository.GetDepartmentsAsync();
            return result;
        }

        public Task<DepartmentDto> GetDepartmentAsync(int departmentId)
        {
            var result = _departmentRepository.GetDepartmentAsync(departmentId);
            return result;
        }

        public Task<Department> UpdateAsync(int departmentId, Department department)
        {
            return _departmentRepository.UpdateAsync(departmentId, department);
        }

        public Task DeleteDepartmentAsync(int departmentId)
        {
            return _departmentRepository.DeleteDepartmentAsync(departmentId);
        }

 
    }
}
