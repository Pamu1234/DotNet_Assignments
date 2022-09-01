using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class LeaveBalanceRepository : ILeaveBalanceRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        public LeaveBalanceRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
        }

        public async Task<LeaveBalance> CreateAsync(LeaveBalance leaveBalance)
        {
            _employeeManagementDataDbContext.LeaveBalances.Add(leaveBalance);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return leaveBalance;
        }

        public async Task<IEnumerable<LeaveBalanceDto>> GetLeavesBalanceAsync()
        {
            var leaveBalanceData = await (from leaveBalance in _employeeManagementDataDbContext.LeaveBalances
                                          select new LeaveBalanceDto()
                                          {
                                              LeaveBalanceId = leaveBalance.LeaveBalanceId,
                                              EmployeeId = leaveBalance.EmployeeId,
                                              LeaveTypeId = leaveBalance.LeaveTypeId,
                                              Balance = leaveBalance.Balance,

                                          }).ToListAsync();
            return leaveBalanceData;
        }

        public async Task<LeaveBalance> GetLeaveBalanceDataByIdAsync(int leaveBalanceId)
        {
            return await _employeeManagementDataDbContext.LeaveBalances.FindAsync(leaveBalanceId);
        }

        public async Task<LeaveBalance> UpdateAsync(int leaveBalanceId, LeaveBalance leaveBalance)
        {
            var leaveBalanceToBeUpdate = await GetLeaveBalanceDataByIdAsync(leaveBalanceId);
            leaveBalanceToBeUpdate.LeaveBalanceId = leaveBalance.LeaveBalanceId;
            leaveBalanceToBeUpdate.EmployeeId = leaveBalance.EmployeeId;
            leaveBalanceToBeUpdate.LeaveTypeId = leaveBalance.LeaveTypeId;
            leaveBalanceToBeUpdate.Balance = leaveBalance.Balance;
            _employeeManagementDataDbContext.LeaveBalances.Update(leaveBalanceToBeUpdate);
            _employeeManagementDataDbContext.SaveChanges();
            return leaveBalanceToBeUpdate;
        }

        public async Task DeleteLeaveBalanceDataByIdAsync(int leaveBalanceId)
        {
            var leaveBalanceToBeDeleted = await GetLeaveBalanceDataByIdAsync(leaveBalanceId);
            _employeeManagementDataDbContext.LeaveBalances.Remove(leaveBalanceToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }
    }
}
