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
    public class LeaveStatusServices: ILeaveStatusRepository
    {
        private readonly LeaveStatusRepository _leaveStatusRepository;
        public LeaveStatusServices(LeaveStatusRepository leaveStatusRepository)
        {
            _leaveStatusRepository = leaveStatusRepository;
        }

        public Task<LeaveStatus> CreateAsync(LeaveStatus leaveStatus)
        {
            return _leaveStatusRepository.CreateAsync(leaveStatus);
        }

        public Task DeleteLeaveAsync(int leaveStatusId)
        {
            return _leaveStatusRepository.DeleteLeaveAsync(leaveStatusId);
        }

        public Task<IEnumerable<LeaveStatusDto>> GetLeavesStatusAsync()
        {
            return _leaveStatusRepository.GetLeavesStatusAsync();
        }

        public Task<LeaveStatus> GetLeaveStatusDataByIdAsync(int leaveStatusId)
        {
            return _leaveStatusRepository.GetLeaveStatusDataByIdAsync(leaveStatusId);
        }

        public Task<LeaveStatus> UpdateAsync(int leaveStatusId, LeaveStatus leaveStatus)
        {
            return _leaveStatusRepository.UpdateAsync(leaveStatusId, leaveStatus);
        }
    }
}
