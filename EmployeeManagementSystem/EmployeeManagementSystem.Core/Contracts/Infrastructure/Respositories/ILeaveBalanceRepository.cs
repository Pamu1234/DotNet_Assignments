using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public interface ILeaveBalanceRepository
    {
        Task<LeaveBalanceDto> GetRemainingLeavesByEmpId(int empId);
        Task<LeaveBalance> CreateAsync(LeaveBalance leaveBalance);
        Task CreateRangeAsync(IEnumerable<LeaveBalance> leaveBalances);
        Task DeleteLeaveBalanceDataByIdAsync(int leaveBalanceId);
        Task<LeaveBalanceDto> GetLeaveBalanceDataByIdAsync(int leaveBalanceId);
        Task<IEnumerable<LeaveBalanceDto>> GetLeavesBalanceAsync();
        Task<LeaveBalance> UpdateAsync(int leaveBalanceId, LeaveBalance leaveBalance);
        
    }
}