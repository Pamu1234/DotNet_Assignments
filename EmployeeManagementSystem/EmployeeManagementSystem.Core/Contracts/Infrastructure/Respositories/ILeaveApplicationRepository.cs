using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public interface ILeaveApplicationRepository
    {
        Task<LeaveApplication> CreateAsync(LeaveApplication leaveApplication);
        Task DeleteLeaveApplicationAsync(int leaveId);
        Task<IEnumerable<LeaveApplicationDto>> GetLeaveApplicationAsync();
        Task<LeaveApplication> GetLeaveDataByIdAsync(int leaveId);
        Task<LeaveApplication> UpdateAsync(int leaveId, LeaveApplication leaveApplication);
    }
}