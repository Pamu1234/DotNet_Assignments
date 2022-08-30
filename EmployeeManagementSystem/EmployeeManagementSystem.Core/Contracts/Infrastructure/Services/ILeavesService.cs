using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Core.Contracts.Infrastructure.Services
{
    public interface ILeavesService
    {
        Task<Leave> CreateAsync(Leave leave);
        Task DeleteLeaveAsync(int leaveId);
        Task<Leave> GetLeaveDataAsync(int leaveId);
        Task<IEnumerable<LeaveDto>> GetLeavesAsync();
        Task<Leave> UpdateAsync(int leaveId, Leave leave);
    }
}
