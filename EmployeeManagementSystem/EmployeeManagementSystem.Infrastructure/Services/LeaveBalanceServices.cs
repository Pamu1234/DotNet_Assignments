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
    public class LeaveBalanceServices : ILeaveBalanceService
    {
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;
        public LeaveBalanceServices(ILeaveBalanceRepository leaveBalanceRepository)
        {
            _leaveBalanceRepository = leaveBalanceRepository;
        }

        public Task<LeaveBalance> CreateAsync(LeaveBalance leaveBalance)
        {
            return _leaveBalanceRepository.CreateAsync(leaveBalance);
        }

        public Task DeleteLeaveBalanceDataByIdAsync(int leaveBalanceId)
        {
            return _leaveBalanceRepository.DeleteLeaveBalanceDataByIdAsync(leaveBalanceId);
        }

        public Task<LeaveBalance> GetLeaveBalanceDataByIdAsync(int leaveBalanceId)
        {
            return _leaveBalanceRepository.GetLeaveBalanceDataByIdAsync(leaveBalanceId);
        }

        public Task<IEnumerable<LeaveBalanceDto>> GetLeavesBalanceAsync()
        {
            return _leaveBalanceRepository.GetLeavesBalanceAsync();
        }

        public Task<LeaveBalance> UpdateAsync(int leaveBalanceId, LeaveBalance leaveBalance)
        {
            return _leaveBalanceRepository.UpdateAsync(leaveBalanceId, leaveBalance);
        }
    }
}
