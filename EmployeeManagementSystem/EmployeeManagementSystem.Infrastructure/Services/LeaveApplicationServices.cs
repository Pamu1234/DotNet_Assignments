using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Core.Enum;
using EmployeeManagementSystem.Infrastructure.Repositories;

namespace EmployeeManagementSystem.Infrastructure.Services
{
    public class LeaveApplicationServices : ILeaveApplicationService
    {
        private readonly ILeaveApplicationRepository _leaveApplicationRepository;
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;
        public LeaveApplicationServices(ILeaveApplicationRepository leaveApplicationRepository, ILeaveBalanceRepository leaveBalanceRepository)
        {
            _leaveApplicationRepository = leaveApplicationRepository;
            _leaveBalanceRepository = leaveBalanceRepository;
        }

        public async Task<LeaveApplication> CreateAsync(LeaveApplication leaveApplication)
        {
            var totalLeaveDays = (leaveApplication.EndDate - leaveApplication.StartDate).TotalDays;
            leaveApplication.NoOfDays = (int)totalLeaveDays;
            leaveApplication.DateOfApplication = DateTime.UtcNow;
            leaveApplication.StatusId = (int)LeaveApprovalStatus.Pending;
            return await _leaveApplicationRepository.CreateAsync(leaveApplication);

        }

        public Task DeleteLeaveApplicationAsync(int leaveId)
        {
            return _leaveApplicationRepository.DeleteLeaveApplicationAsync(leaveId);
        }

        public Task<IEnumerable<LeaveApplicationDto>> GetLeaveApplicationAsync()
        {
            return _leaveApplicationRepository.GetLeaveApplicationAsync();
        }

        public Task<LeaveApplication> GetLeaveDataByIdAsync(int leaveId)
        {
            return _leaveApplicationRepository.GetLeaveDataByIdAsync(leaveId);
        }

        public Task<LeaveApplication> UpdateAsync(int leaveId, LeaveApplication leaveApplication)
        {
            return _leaveApplicationRepository.UpdateAsync(leaveId, leaveApplication);
        }

        public Task<IEnumerable<LeaveApplicationDto>> GetEmployeeLeaveRequest (int empId)
        {
            return _leaveApplicationRepository.GetEmployeeLeaveRequest(empId);
        }
    }
}
