using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.Services
{
    public class LeaveApplicationServices : ILeaveApplicationService
    {
        private readonly ILeaveApplicationRepository _leaveApplicationRepository;
        public LeaveApplicationServices(ILeaveApplicationRepository leaveApplicationRepository)
        {
            _leaveApplicationRepository = leaveApplicationRepository;
        }

        public Task<LeaveApplication> CreateAsync(LeaveApplication leaveApplication)
        {
            return _leaveApplicationRepository.CreateAsync(leaveApplication);
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
    }
}
