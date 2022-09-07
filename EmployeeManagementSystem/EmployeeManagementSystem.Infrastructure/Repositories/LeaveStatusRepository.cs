using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class LeaveStatusRepository : ILeaveStatusRepository
    {
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;

        public LeaveStatusRepository(EmployeemanagementDbContext employeeManagementDataDbContext, IDbConnection dbConnection)
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
            var leaveStatusDataByIdQuery = "select * from LeaveStatus where StatusId = @leaveStatusId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<LeaveStatus>(leaveStatusDataByIdQuery, new { leaveStatusId });
        }

        public async Task<LeaveStatus> UpdateAsync(int leaveStatusId, LeaveStatus leaveStatus)
        {
            var leaveStatusToBeUpdate = await GetLeaveStatusDataByIdAsync(leaveStatusId);
            leaveStatus.StatusId = leaveStatus.StatusId;
            leaveStatus.Description = leaveStatus.Description;
            leaveStatus.Status = leaveStatus.Status;
            _employeeManagementDataDbContext.LeaveStatuses.Update(leaveStatus);
            _employeeManagementDataDbContext.SaveChanges();
            return leaveStatus;
        }

        public async Task DeleteLeaveAsync(int leaveStatusId)
        {
            var leaveStatusToBeDeleted = await GetLeaveStatusDataByIdAsync(leaveStatusId);
            _employeeManagementDataDbContext.LeaveStatuses.Remove(leaveStatusToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }

    }
}
