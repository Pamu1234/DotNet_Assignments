using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public interface ILeaveBalanceRepository
    {
        Task<IEnumerable<LeaveBalance>> GetRemainingLeavesByEmpId(int empId);
        Task<LeaveBalance> CreateAsync(LeaveBalance leaveBalance);
        Task DeleteLeaveBalanceDataByIdAsync(int leaveBalanceId);
        Task<LeaveBalance> GetLeaveBalanceDataByIdAsync(int leaveBalanceId);
        Task<IEnumerable<LeaveBalanceDto>> GetLeavesBalanceAsync();
        Task<LeaveBalance> UpdateAsync(int leaveBalanceId, LeaveBalance leaveBalance);
    }
}