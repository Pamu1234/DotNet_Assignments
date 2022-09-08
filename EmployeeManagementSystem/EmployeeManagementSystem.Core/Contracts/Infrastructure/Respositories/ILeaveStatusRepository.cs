using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public interface ILeaveStatusRepository
    {
        Task<LeaveStatus> CreateAsync(LeaveStatus leaveStatus);
       // Task DeleteLeaveAsync(int leaveStatusId);
        Task<IEnumerable<LeaveStatusDto>> GetLeavesStatusAsync();
        Task<LeaveStatus> GetLeaveStatusDataByIdAsync(int leaveStatusId);
        Task<LeaveStatus> UpdateAsync(int leaveStatusId, LeaveStatus leaveStatus);
    }
}