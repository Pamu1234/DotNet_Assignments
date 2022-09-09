using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class LeaveApplicationRepository : ILeaveApplicationRepository
    {
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;

        public LeaveApplicationRepository(EmployeemanagementDbContext employeeManagementDataDbContext, IDbConnection dbConnection)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
            _dapperConnection = dbConnection;
        }

        public async Task<LeaveApplication> CreateAsync(LeaveApplication leaveApplication)
        {
            _employeeManagementDataDbContext.LeaveApplications.Add(leaveApplication);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return leaveApplication;

        }

        public async Task<IEnumerable<LeaveApplicationDto>> GetLeaveApplicationAsync()
        {
            var getLeaveApplicationDataQuery = "select * from LeaveApplication";
            var result = await _dapperConnection.QueryAsync<LeaveApplicationDto>(getLeaveApplicationDataQuery);
            return result;
        }

        public async Task<LeaveApplication> GetLeaveDataByIdAsync(int leaveId)
        {
            var getEmployeeByIdQuery = "select * from LeaveApplication where EmployeeId = @leaveId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<LeaveApplication>(getEmployeeByIdQuery, new { leaveId });
        }

        public async Task<LeaveApplication> UpdateAsync(int leaveId, LeaveApplication leaveApplication)
        {
            var leaveApplicationToBeUpdate = await GetLeaveDataByIdAsync(leaveId);
            leaveApplication.EmployeeId = leaveApplication.EmployeeId;
            leaveApplication.LeaveTypeId = leaveApplication.LeaveTypeId;
            leaveApplication.DateOfApproval = DateTime.UtcNow;
            leaveApplication.StartDate = DateTime.UtcNow;
            leaveApplication.EndDate = DateTime.UtcNow;
            leaveApplication.StatusId = leaveApplication.StatusId;
            _employeeManagementDataDbContext.LeaveApplications.Update(leaveApplication);
            _employeeManagementDataDbContext.SaveChanges();
            return leaveApplication;
        }

        public async Task DeleteLeaveApplicationAsync(int leaveId)
        {
            var leaveApplicationToBeDeleted = await GetLeaveDataByIdAsync(leaveId);
            _employeeManagementDataDbContext.LeaveApplications.Remove(leaveApplicationToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }
    }
}
