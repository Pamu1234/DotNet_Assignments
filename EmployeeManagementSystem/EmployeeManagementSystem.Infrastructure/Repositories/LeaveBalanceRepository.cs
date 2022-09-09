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
        private readonly ILeaveRepository _leaveRepository;

        public LeaveBalanceRepository(EmployeemanagementDbContext employeeManagementDataDbContext,IDbConnection dbConnection,ILeaveRepository leaveRepository)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
            _dapperConnection = dbConnection;
            _leaveRepository = leaveRepository;
        }

        public async Task<LeaveBalance> CreateAsync(LeaveBalance leaveBalance)
        {
            _employeeManagementDataDbContext.LeaveBalances.Add(leaveBalance);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return leaveBalance;
        }

        public async Task CreateRangeAsync(IEnumerable<LeaveBalance> leaveBalances)
        {
            _employeeManagementDataDbContext.LeaveBalances.AddRange(leaveBalances);
            await _employeeManagementDataDbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<LeaveBalanceDto>> GetLeavesBalanceAsync()
        {
            var getLeaveBalanceDataQuery = "Execute GetLeaveBalanceData ";
            var result = await _dapperConnection.QueryAsync<LeaveBalanceDto>(getLeaveBalanceDataQuery);
            return result;
        }

        public async Task<LeaveBalance> GetLeaveBalanceDataByIdAsync(int leaveBalanceId)
        {
            var getLeaveBalanceDataByIdQuery = "select * from LeaveBalance where LeaveBalanceId = @leaveBalanceId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<LeaveBalance>(getLeaveBalanceDataByIdQuery, new { leaveBalanceId });
        }
        //public async Task<IEnumerable<LeaveBalanceDto>> GetLeavesDetails()
        //{
        //    var leaveData = from leaveBalance in _employeeManagementDataDbContext.LeaveBalances
        //                    join leave in _employeeManagementDataDbContext.Leaves
        //                    on leaveBalance.LeaveTypeId equals leave.LeaveTypeId
        //                    join Employee in _employeeManagementDataDbContext.Employees
        //                    on 
        //                    select new LeaveBalanceDto()
        //                    {
        //                        Balance = leaveBalance.Balance,
        //                        EmployeeId = leaveBalance.Employee.EmployeeId,
        //                          LeaveType = leave.LeaveTypeName,
        //                    };
        //}
        public async Task<IEnumerable<LeaveBalance>> GetRemainingLeavesByEmpId(int empId)
        {
            var remainingLeaves = "execute GetLeaveBalanceData EmployeeId=@empId";
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
