using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public interface ILeaveRepository
    {
        Task<Leave> CreateAsync(Leave leave);
        Task DeleteLeaveAsync(int leaveId);
        Task<Leave> GetLeaveDataAsync(int leaveId);
        Task<IEnumerable<LeaveDto>> GetLeavesAsync();
        Task<Leave> UpdateAsync(int leaveId, Leave leave);
    }
}