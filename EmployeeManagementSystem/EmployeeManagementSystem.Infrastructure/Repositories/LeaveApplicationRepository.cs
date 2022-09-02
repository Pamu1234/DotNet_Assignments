using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class LeaveApplicationRepository : ILeaveApplicationRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;
        public LeaveApplicationRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext, IDbConnection dbConnection)
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
            leaveApplicationToBeUpdate.EmployeeId = leaveApplication.EmployeeId;
            leaveApplicationToBeUpdate.LeaveTypeId = leaveApplication.LeaveTypeId;
            leaveApplicationToBeUpdate.Purpose = leaveApplication.Purpose;
            leaveApplicationToBeUpdate.NoOfDays = leaveApplication.NoOfDays;
            leaveApplicationToBeUpdate.DateOfApproval = leaveApplication.DateOfApproval;
            leaveApplicationToBeUpdate.StatusId = leaveApplication.StatusId;
            leaveApplicationToBeUpdate.UpdatedDate = leaveApplication.UpdatedDate;
            leaveApplicationToBeUpdate = leaveApplication;
            _employeeManagementDataDbContext.LeaveApplications.Update(leaveApplicationToBeUpdate);
            _employeeManagementDataDbContext.SaveChanges();
            return leaveApplicationToBeUpdate;
        }

        public async Task DeleteLeaveApplicationAsync(int leaveId)
        {
            var leaveApplicationToBeDeleted = await GetLeaveDataByIdAsync(leaveId);
            _employeeManagementDataDbContext.LeaveApplications.Remove(leaveApplicationToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }
    }
}
