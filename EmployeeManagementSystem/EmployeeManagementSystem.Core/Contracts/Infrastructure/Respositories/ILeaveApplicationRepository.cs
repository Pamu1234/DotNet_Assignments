using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public interface ILeaveApplicationRepository
    {
        Task<LeaveApplication> CreateAsync(LeaveApplication leaveApplication);
        Task DeleteLeaveApplicationAsync(int leaveId);
        Task<IEnumerable<LeaveApplicationDto>> GetEmployeeLeaveRequest(int empId);
        Task<IEnumerable<TotalLeavesOfEmployeeDto>> GetEmployeeLeavesData(int empId);
        Task<IEnumerable<LeaveApplicationDto>> GetLeaveApplicationAsync(int? LeaveApplicationId = null, int? EmployeeId = null);
        Task<LeaveApplication> GetLeaveDataByIdAsync(int leaveId);
        Task<LeaveApplication> UpdateAsync(int leaveId, UpdateLeaveApplicationRequestDto updateLeaveApplicationRequestDto,LeaveApplication leaveApplication);
    }
}