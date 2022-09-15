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
            return(_employeeRepository.DeleteEmployeeAsync(employeeId));
        }

        public async Task<IEnumerable< EmployeeDto>> GetEmployeeAsync(int employeeId)
        {
            var remainingLeaves = await _employeeRepository.GetEmployeeAsync(employeeId);
            return remainingLeaves;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            var employeeData = await _employeeRepository.GetEmployeesAsync();
            return employeeData;

        }

        public Task<Employee> UpdateAsync(int employeeId, Employee employee)
        {
            return _employeeRepository.UpdateAsync(employeeId, employee);
        }


    }
}
