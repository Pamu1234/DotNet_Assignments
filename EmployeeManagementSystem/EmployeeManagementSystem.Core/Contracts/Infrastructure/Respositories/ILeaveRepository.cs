using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public interface ILeaveRepository
    {
        Task<Leaves> CreateAsync(Leaves leave);
        Task DeleteLeaveAsync(int leaveId);
        Task<Leaves> GetLeaveDataAsync(int leaveId);
        Task<IEnumerable<LeaveDto>> GetLeavesAsync();
        Task<Leaves> UpdateAsync(int leaveId, Leaves leave);
    }
}