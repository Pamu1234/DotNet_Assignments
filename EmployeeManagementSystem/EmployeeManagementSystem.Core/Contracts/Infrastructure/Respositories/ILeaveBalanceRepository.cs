using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public interface ILeaveBalanceRepository
    {
        //Task<IEnumerable< LeaveBalanceDto>> GetRemainingLeavesByEmpId(int empId);
        Task<LeaveBalance> GetRemainingLeavesByEmpId(int empId,int leaveTypeId);
        Task<LeaveBalance> CreateAsync(LeaveBalance leaveBalance);
        Task CreateRangeAsync(IEnumerable<LeaveBalance> leaveBalances);
        Task DeleteLeaveBalanceDataByIdAsync(int leaveBalanceId);
        Task<LeaveBalanceDto> GetLeaveBalanceDataByIdAsync(int leaveBalanceId);
        Task<IEnumerable<LeaveBalanceDto>> GetLeavesBalanceAsync();
        Task<LeaveBalance> UpdateAsync(int leaveBalanceId, LeaveBalance leaveBalance);
        //Task<LeaveBalance> GetCheckLeaveBalanceAsync(LeaveApplication leaveApplication);


    }
}