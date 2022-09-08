using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILeaveRepository _leaveRepository;
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;

        public EmployeeService(IEmployeeRepository employeeRepository, ILeaveRepository leaveRepository, ILeaveBalanceRepository leaveBalanceRepository, EmployeemanagementDbContext employeeManagementDataDbContext)
        {
            _employeeRepository = employeeRepository;
            _leaveRepository = leaveRepository;
            _leaveBalanceRepository = leaveBalanceRepository;
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
        }


        public async Task<Employee> CreateAsync(Employee employee)
        {
            var emplyeeData = await _employeeRepository.CreateAsync(employee);
            var leaves = await _leaveRepository.GetLeavesAsync();
  
            List<LeaveBalance> leaveBalances = new List<LeaveBalance>();
            foreach (var item in leaves)
            {
                LeaveBalance leaveBalance = new LeaveBalance();
                leaveBalance.EmployeeId = emplyeeData.EmployeeId;
                leaveBalance.LeaveTypeId = item.LeaveTypeId;
                leaveBalance.Balance = item.NoOfDays;
                leaveBalances.Add(leaveBalance);
            }
            await _leaveBalanceRepository.CreateRangeAsync(leaveBalances);
            return emplyeeData;

  
        }


        public Task DeleteEmployeeAsync(int employeeId)
        {
            return _employeeRepository.DeleteEmployeeAsync(employeeId);
        }

        public async Task<EmployeeDto> GetEmployeeAsync(int employeeId)
        {
            var remainingLeaves = await (from emp in _employeeManagementDataDbContext.Employees
                                         join leaveBalance in _employeeManagementDataDbContext.LeaveBalances
                                         on emp.EmployeeId equals leaveBalance.EmployeeId

                                         where emp.EmployeeId == employeeId
                                         select new EmployeeDto
                                         {
                                             EmpId = emp.EmployeeId,
                                             Role = emp.Role.RoleName,
                                             FirstName = emp.FirstName,
                                             LastName = emp.LastName,
                                             EmailId = emp.EmailId,
                                             Address = emp.Address,
                                             Contact = emp.Contact,
                                             DepartmentName = emp.Department.DepartmentName,
                                         }).FirstOrDefaultAsync();
            return remainingLeaves;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            var employeeData =await  (from department in _employeeManagementDataDbContext.Departments
                               join emp in _employeeManagementDataDbContext.Employees
                               on department.DepartmentId equals emp.DepartmentId
                               join role in _employeeManagementDataDbContext.Roles
                               on emp.RoleId equals role.RoleId
                               select new EmployeeDto
                               {
                                   FirstName = emp.FirstName,
                                   LastName= emp.LastName,
                                   EmailId= emp.EmailId,
                                   Contact = emp.Contact,
                                   Address = emp.Address,
                                   DepartmentName= department.DepartmentName,
                                   Role=role.RoleName,
                                   EmpId=emp.EmployeeId

                               }).ToListAsync();

            return employeeData;
            //return _employeeRepository.GetEmployeesAsync();

        }

        public Task<Employee> UpdateAsync(int employeeId, Employee employee)
        {
            return _employeeRepository.UpdateAsync(employeeId, employee);
        }


    }
}
