using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class LeaveBalanceRepository : ILeaveBalanceRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;
        public LeaveBalanceRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext,IDbConnection dbConnection)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
            _dapperConnection = dbConnection;
        }

        public async Task<LeaveBalance> CreateAsync(LeaveBalance leaveBalance)
        {
            _employeeManagementDataDbContext.LeaveBalances.Add(leaveBalance);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return leaveBalance;
        }

        public async Task<IEnumerable<LeaveBalanceDto>> GetLeavesBalanceAsync()
        {
            var getLeaveBalanceDataQuery = "select * from LeaveBalance";
            var result = await _dapperConnection.QueryAsync<LeaveBalanceDto>(getLeaveBalanceDataQuery);
            return result;
        }

        public async Task<LeaveBalance> GetLeaveBalanceDataByIdAsync(int leaveBalanceId)
        {
            var getLeaveBalanceDataByIdQuery = "select * from LeaveBalance where LeaveBalanceId = @leaveBalanceId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<LeaveBalance>(getLeaveBalanceDataByIdQuery, new { leaveBalanceId });
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
