using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class LeaveStatusRepository : ILeaveStatusRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        public LeaveStatusRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
        }
        public async Task<LeaveStatus> CreateAsync(LeaveStatus leaveStatus)
        {
            _employeeManagementDataDbContext.LeaveStatuses.Add(leaveStatus);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return leaveStatus;
        }

        public async Task<IEnumerable<LeaveStatusDto>> GetLeavesStatusAsync()
        {
            var leavesStatusData = await (from leaveStatus in _employeeManagementDataDbContext.LeaveStatuses
                                          select new LeaveStatusDto()
                                          {
                                              StatusId = leaveStatus.StatusId,
                                              Description = leaveStatus.Description,
                                              Status = leaveStatus.Status,
                                          }).ToListAsync();
            return leavesStatusData;
        }

        public async Task<LeaveStatus> GetLeaveStatusDataByIdAsync(int leaveStatusId)
        {
            return await _employeeManagementDataDbContext.LeaveStatuses.FindAsync(leaveStatusId);
        }

        public async Task<LeaveStatus> UpdateAsync(int leaveStatusId, LeaveStatus leaveStatus)
        {
            var leaveStatusToBeUpdate = await GetLeaveStatusDataByIdAsync(leaveStatusId);
            leaveStatusToBeUpdate.StatusId = leaveStatus.StatusId;
            leaveStatusToBeUpdate.Description = leaveStatus.Description;
            leaveStatusToBeUpdate.Status = leaveStatus.Status;
            _employeeManagementDataDbContext.LeaveStatuses.Update(leaveStatusToBeUpdate);
            _employeeManagementDataDbContext.SaveChanges();
            return leaveStatusToBeUpdate;
        }

        public async Task DeleteLeaveAsync(int leaveStatusId)
        {
            var leaveStatusToBeDeleted = await GetLeaveStatusDataByIdAsync(leaveStatusId);
            _employeeManagementDataDbContext.LeaveStatuses.Remove(leaveStatusToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }

    }
}
