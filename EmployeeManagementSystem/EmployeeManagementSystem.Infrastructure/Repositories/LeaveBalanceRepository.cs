using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class LeaveBalanceRepository : ILeaveBalanceRepository
    {
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;
        public LeaveBalanceRepository(EmployeemanagementDbContext employeeManagementDataDbContext,IDbConnection dbConnection)
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

        public async Task<IEnumerable<LeaveBalance>> GetRemainingLeavesByEmpId(int empId)
        {
            var remainingLeaves = "select * from LeaveBalance where EmployeeId=@empId";
            return await _dapperConnection.QueryAsync<LeaveBalance>(remainingLeaves, new { empId });
        }

        public async Task<LeaveBalance> UpdateAsync(int leaveBalanceId, LeaveBalance leaveBalance)
        {
            var leaveBalanceToBeUpdate = await GetLeaveBalanceDataByIdAsync(leaveBalanceId);
            leaveBalance.LeaveBalanceId = leaveBalance.LeaveBalanceId;
            leaveBalance.EmployeeId = leaveBalance.EmployeeId;
            leaveBalance.LeaveTypeId = leaveBalance.LeaveTypeId;
            leaveBalance.Balance = leaveBalance.Balance;
            _employeeManagementDataDbContext.LeaveBalances.Update(leaveBalance);
            _employeeManagementDataDbContext.SaveChanges();
            return leaveBalance;
        }

        public async Task DeleteLeaveBalanceDataByIdAsync(int leaveBalanceId)
        {
            var leaveBalanceToBeDeleted = await GetLeaveBalanceDataByIdAsync(leaveBalanceId);
            _employeeManagementDataDbContext.LeaveBalances.Remove(leaveBalanceToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }
    }
}
