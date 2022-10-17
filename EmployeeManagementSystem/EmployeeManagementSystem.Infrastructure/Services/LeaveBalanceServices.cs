using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Infrastructure.Services
{
    public class LeaveBalanceServices : ILeaveBalanceService
    {
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;

        public LeaveBalanceServices(ILeaveBalanceRepository leaveBalanceRepository, EmployeemanagementDbContext employeemanagementDbContext)
        {
            _leaveBalanceRepository = leaveBalanceRepository;
            _employeeManagementDataDbContext = employeemanagementDbContext;
        }

        public Task<LeaveBalance> CreateRangeAsync(LeaveBalance leaveBalance)
        {
            return _leaveBalanceRepository.CreateAsync(leaveBalance);
        }

        public Task DeleteLeaveBalanceDataByIdAsync(int leaveBalanceId)
        {
            return _leaveBalanceRepository.DeleteLeaveBalanceDataByIdAsync(leaveBalanceId);
        }

        public async Task<IEnumerable<LeaveBalanceDto>> GetLeavesBalanceAsync()
        {

            var leaveBalance = await _leaveBalanceRepository.GetLeavesBalanceAsync();
            return leaveBalance;

        }
        public async Task<LeaveBalanceDto> GetLeaveBalanceDataByIdAsync(int leaveBalanceId)
        {
            return await _leaveBalanceRepository.GetLeaveBalanceDataByIdAsync(leaveBalanceId);
        }

        public async Task<LeaveBalance> GetRemainingLeavesByEmpId(int empId,int leaveTypeId )
        {
           return  await _leaveBalanceRepository.GetRemainingLeavesByEmpId(empId, leaveTypeId);
           
        }
         
        public async Task<IEnumerable<EmployeeAvailableLeavesDto>> GetRemainingLeaveBalanceOfEmployee(int empId)
        {
            var data = await _leaveBalanceRepository.GetRemainingLeaveBalanceOfEmployee(empId);
            return data;
        }
        public Task<LeaveBalance> UpdateAsync(int leaveBalanceId, LeaveBalance leaveBalance)
        {
            return _leaveBalanceRepository.UpdateAsync(leaveBalanceId, leaveBalance);
        }

        public async Task<LeaveBalanceDto> GetEmployeesRemainingleaves(int empId)
        {
            var leaveBalancesOfEmployee =  await (from emp in _employeeManagementDataDbContext.Employees
                                           join LeaveBalance in _employeeManagementDataDbContext.LeaveBalances
                                           on emp.EmployeeId equals LeaveBalance.EmployeeId
                                           where LeaveBalance.EmployeeId == empId
                                           select new LeaveBalanceDto
                                           {
                                               Balance = LeaveBalance.Balance,
                                               

                                           }).FirstOrDefaultAsync();
            return leaveBalancesOfEmployee;
        }

    }
}
