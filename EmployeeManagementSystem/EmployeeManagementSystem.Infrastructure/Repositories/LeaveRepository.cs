using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;

        public LeaveRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext, IDbConnection dbConnection)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
            _dapperConnection = dbConnection;
        }

        public async Task<Leave> CreateAsync(Leave leave)
        {
            _employeeManagementDataDbContext.Leaves.Add(leave);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return leave;
        }

        public async Task<IEnumerable<LeaveDto>> GetLeavesAsync()
        {
            var getLeavesDataQuery = "select * from Leaves";
            var result = await _dapperConnection.QueryAsync<LeaveDto>(getLeavesDataQuery);
            return result;
        }

        public async Task<Leave> GetLeaveDataAsync(int leaveId)
        {
            var getLeavesDataByIdQuery = "select * from Leaves where LeaveTypeId = @leaveId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<Leave>(getLeavesDataByIdQuery, new { leaveId });
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
