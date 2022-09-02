using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Data;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class LeaveStatusRepository : ILeaveStatusRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;

        public LeaveStatusRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext, IDbConnection dbConnection)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
            _dapperConnection = dbConnection;
        }
        public async Task<LeaveStatus> CreateAsync(LeaveStatus leaveStatus)
        {
            _employeeManagementDataDbContext.LeaveStatuses.Add(leaveStatus);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return leaveStatus;
        }

        public async Task<IEnumerable<LeaveStatusDto>> GetLeavesStatusAsync()
        {
            var leaveStatusDataQuery = "select * from LeaveStatus";
            var result = await _dapperConnection.QueryAsync<LeaveStatusDto>(leaveStatusDataQuery);
            return result;
        }

        public async Task<LeaveStatus> GetLeaveStatusDataByIdAsync(int leaveStatusId)
        {
            var leaveStatusDataByIdQuery = "select * from Leaves where LeaveStatus = @leaveStatusId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<LeaveStatus>(leaveStatusDataByIdQuery, new { leaveStatusId });
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
