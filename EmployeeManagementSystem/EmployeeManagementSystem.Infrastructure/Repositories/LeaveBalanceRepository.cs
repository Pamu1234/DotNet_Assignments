using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<LeaveBalanceDto> GetLeaveBalanceDataByIdAsync(int leaveBalanceId)
        {
            var getLeaveBalanceDataByIdQuery = "select * from LeaveBalance where LeaveBalanceId = @leaveBalanceId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<LeaveBalanceDto>(getLeaveBalanceDataByIdQuery, new { leaveBalanceId });
        }

        public async Task<IEnumerable< LeaveBalanceDto>> GetRemainingLeavesByEmpId(int empId)
        {

            var remainingLeavesOfEmployee = await (from employee in _employeeManagementDataDbContext.Employees
                                                   join leaveBlance in _employeeManagementDataDbContext.LeaveBalances
                                                   on employee.EmployeeId equals leaveBlance.EmployeeId
                                                   join leaveType in _employeeManagementDataDbContext.Leaves
                                                   on leaveBlance.LeaveTypeId equals leaveType.LeaveTypeId
                                                   where employee.EmployeeId == empId
                                                   select new LeaveBalanceDto
                                                   {
                                                       FirstName = employee.FirstName,
                                                       LastName = employee.LastName,
                                                       LeaveTypeName = leaveType.LeaveTypeName,
                                                       Balance = leaveBlance.Balance
                                                   }).ToListAsync();
            return remainingLeavesOfEmployee;
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
           //_employeeManagementDataDbContext.LeaveBalances.Remove(leaveBalanceToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }

    }
}
