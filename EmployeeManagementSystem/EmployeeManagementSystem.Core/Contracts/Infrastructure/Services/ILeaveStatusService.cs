using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Core.Contracts.Infrastructure.Services
{
    public interface ILeaveStatusService
    {
        Task<LeaveStatus> CreateAsync(LeaveStatus leaveStatus);
        //Task DeleteLeaveAsync(int leaveStatusId);
        Task<IEnumerable<LeaveStatusDto>> GetLeavesStatusAsync();
        Task<LeaveStatus> GetLeaveStatusDataByIdAsync(int leaveStatusId);
       Task<LeaveStatus> UpdateAsync(int leaveStatusId, LeaveStatus leaveStatus);
    }
}
