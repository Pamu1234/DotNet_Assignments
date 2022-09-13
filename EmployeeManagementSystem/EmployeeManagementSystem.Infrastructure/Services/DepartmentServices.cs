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

        public async Task<bool> ApproveLeaveApplication(int empId,int leaveId)
        {
            var isEmployeeHR = await IsEmployeeIsHr(empId);
            if (isEmployeeHR == true)
            {
                var leaveApplication = await _leaveApplicationRepository.GetLeaveDataByIdAsync(leaveId);
                leaveApplication.ApprovedBy = 1;
                if (leaveApplication != null)
                {

                    var data = await (from leave in _employeeManagementDataDbContext.Leaves
                                      join leaveBlance in _employeeManagementDataDbContext.LeaveBalances
                                      on leave.LeaveTypeId equals leaveBlance.LeaveTypeId
                                      where leave.LeaveTypeId == leaveApplication.LeaveTypeId && leaveBlance.EmployeeId == leaveApplication.EmployeeId

                                      select leaveBlance).FirstAsync();

                    data.Balance = data.Balance - leaveApplication.NoOfDays;

                    await _leaveBalanceRepository.UpdateAsync(data.LeaveBalanceId, data);
                }
                return true;
            }
            return false;
        }
        public async Task<bool> IsEmployeeIsHr(int empId)
        {
            var departments = await _departmentRepository.GetDepartmentsAsync();
            var employees = await _employeeRepository.GetEmployeesAsync();
            var employee =  from emp in employees
                            join dept in departments
                            on emp.DepartmentName equals dept.DepartmentName
                            where emp.EmployeeId == empId
                            select emp;
            if (employee != null)
                return true;
            return false;
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
