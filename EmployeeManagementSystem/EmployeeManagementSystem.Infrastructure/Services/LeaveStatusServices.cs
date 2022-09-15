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
    public class LeaveStatusServices: ILeaveStatusService
    {
        private readonly ILeaveStatusRepository _leaveStatusRepository;
        public LeaveStatusServices(ILeaveStatusRepository leaveStatusRepository)
        {
            _leaveStatusRepository = leaveStatusRepository;
        }

        public Task<LeaveStatus> CreateAsync(LeaveStatus leaveStatus)
        {
            return _leaveStatusRepository.CreateAsync(leaveStatus);
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
