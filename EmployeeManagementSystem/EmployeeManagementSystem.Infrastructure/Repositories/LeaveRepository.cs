using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        public LeaveRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
        }

        public async Task<Leave> CreateAsync(Leave leave)
        {
            _employeeManagementDataDbContext.Leaves.Add(leave);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return leave;
        }

        public async Task<IEnumerable<LeaveDto>> GetLeavesAsync()
        {
            var leavesData = await (from leave in _employeeManagementDataDbContext.Leaves
                                   select new LeaveDto()
                                   {
                                      LeaveTypeName = leave.LeaveTypeName,
                                       Description = leave.Description,

                                   }).ToListAsync();
            return leavesData;
        }

        public async Task<Leave> GetLeaveDataAsync(int leaveId)
        {
            return await _employeeManagementDataDbContext.Leaves.FindAsync(leaveId);
        }

        public async Task<Leave> UpdateAsync(int leaveId, Leave leave)
        {
            var leaveToBeUpdate = await GetLeaveDataAsync(leaveId);
            leaveToBeUpdate.LeaveTypeId = leave.LeaveTypeId;
            leaveToBeUpdate.LeaveTypeName = leave.LeaveTypeName;
            leaveToBeUpdate.Description = leave.Description;
            leaveToBeUpdate.CreatedBy = leave.CreatedBy;
            leaveToBeUpdate.CreatedDate = leave.CreatedDate;
            leaveToBeUpdate.UpdatedBy = leave.UpdatedBy;
            leaveToBeUpdate.UpdatedDate = leave.UpdatedDate;
            _employeeManagementDataDbContext.Leaves.Update(leaveToBeUpdate);
            _employeeManagementDataDbContext.SaveChanges();
            return leaveToBeUpdate;
        }

        public async Task DeleteLeaveAsync(int leaveId)
        {
            var leaveToBeDeleted = await GetLeaveDataAsync(leaveId);
            _employeeManagementDataDbContext.Leaves.Remove(leaveToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }
    }
}
