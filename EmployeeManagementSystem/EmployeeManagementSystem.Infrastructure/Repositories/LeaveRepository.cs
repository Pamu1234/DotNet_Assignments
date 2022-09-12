using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;

        public LeaveRepository(EmployeemanagementDbContext employeeManagementDataDbContext, IDbConnection dbConnection)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
            _dapperConnection = dbConnection;
        }

        public async Task<Leaves> CreateAsync(Leaves leave)
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

        public async Task<Leaves> GetLeaveDataAsync(int leaveId)
        {
            var getLeavesDataByIdQuery = "select * from Leaves where LeaveTypeId = @leaveId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<Leaves>(getLeavesDataByIdQuery, new { leaveId });
        }

        public async Task<Leaves> UpdateAsync(int leaveId, Leaves leave)
        {
            var leaveToBeUpdate = await GetLeaveDataAsync(leaveId);
            leave.LeaveTypeId = leave.LeaveTypeId;
            leave.LeaveTypeName = leave.LeaveTypeName;
            leave.Description = leave.Description;
            leave.CreatedBy = leave.CreatedBy;
            leave.CreatedDate = leave.CreatedDate;
            leave.UpdatedBy = leave.UpdatedBy;
            leave.UpdatedDate = leave.UpdatedDate;
            _employeeManagementDataDbContext.Leaves.Update(leave);
            _employeeManagementDataDbContext.SaveChanges();
            return leave;
        }

        public async Task DeleteLeaveAsync(int leaveId)
        {
            var leaveToBeDeleted = await GetLeaveDataAsync(leaveId);
            _employeeManagementDataDbContext.Leaves.Remove(leaveToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }
    }
}
