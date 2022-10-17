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

        public async Task<Department> CreateAsync(Department department)
        {
             var result =  await _departmentRepository.CreateAsync(department);
            return result;
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

        public async Task<DepartmentDto> UpdateAsync(int deptId, DepartmentToBeUpdatedDto department)
        {
            var departmentData = await _departmentRepository.GetDepartmentById(deptId);
            if(departmentData != null)
            {
                departmentData.DepartmentName = department.DepartmentName;
                departmentData.Description = department.Description;
                departmentData.UpdatedBy = department.UpdatedBy;
                departmentData.UpdatedDate = DateTime.UtcNow;
                
                var data = await _departmentRepository.UpdateAsync(departmentData);

                if(data !=null)
                {
                    var result = await _departmentRepository.GetDepartmentAsync(deptId);
                    return result;
                }

            }
            return null;

        }

        public Task DeleteDepartmentAsync(int departmentId)
        {
            return _departmentRepository.DeleteDepartmentAsync(departmentId);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmpWorkingInDept(int deptId)
        {
            var result =  await _departmentRepository.GetListOfEmpWorkingInEachDept(deptId);
            return result;
        }

        public async Task<int> EmployeeInDepsrtmentCount(int deptId)
        {
            var employees = await _departmentRepository.GetListOfEmpWorkingInEachDept(deptId);
            return employees.Count();
        }

        public async Task<DepartmentDto> GetDepartmentByNameAsync(string departmentName)
        {

            var result = await _departmentRepository.GetDepartmentByNameAsync(departmentName);
            return result;
        }
    }
}
