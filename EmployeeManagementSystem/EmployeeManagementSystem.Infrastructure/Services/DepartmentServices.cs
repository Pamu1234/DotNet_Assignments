using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories;
using EmployeeManagementSystem.Infrastructure.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Infrastructure.Services
{
    public class DepartmentServices : IDepartmentService
    {
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILeaveApplicationRepository _leaveApplicationRepository;
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;

        public DepartmentServices(EmployeemanagementDbContext employeeManagementDataDbContext, IDepartmentRepository departmentRepository, ILeaveBalanceRepository leaveBalanceRepository, IEmployeeRepository employeeRepository,ILeaveApplicationRepository leaveApplicationRepository)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _leaveApplicationRepository = leaveApplicationRepository;
            _leaveBalanceRepository = leaveBalanceRepository;
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
