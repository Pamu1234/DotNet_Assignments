using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Core.Contracts.Infrastructure.Services
{
    public interface  ILeaveApplicationService
    {
        Task<LeaveApplication> CreateAsync(LeaveApplication leaveApplication);
        Task DeleteLeaveApplicationAsync(int leaveId);
        Task<IEnumerable<LeaveApplicationDto>> GetLeaveApplicationAsync();
        Task<LeaveApplication> GetLeaveDataByIdAsync(int leaveId);
        Task<LeaveApplication> UpdateAsync(int leaveId, LeaveApplication leaveApplication);
    }
}
