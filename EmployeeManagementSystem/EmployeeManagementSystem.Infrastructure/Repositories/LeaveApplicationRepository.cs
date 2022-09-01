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
    public class LeaveApplicationRepository : ILeaveApplicationRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        public LeaveApplicationRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
        }

        public async Task<LeaveApplication> CreateAsync(LeaveApplication leaveApplication)
        {
            _employeeManagementDataDbContext.LeaveApplications.Add(leaveApplication);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return leaveApplication;

        }

        public async Task<IEnumerable<LeaveApplicationDto>> GetLeaveApplicationAsync()
        {
            var employeeLeaveList = await (from leaveApplication in _employeeManagementDataDbContext.LeaveApplications
                                      select new LeaveApplicationDto()
                                      {
                                          EmployeeId = leaveApplication.EmployeeId,
                                          LeaveTypeId = leaveApplication.LeaveTypeId,
                                          Purpose = leaveApplication.Purpose,
                                          NoOfDays = leaveApplication.NoOfDays,
                                          DateOfApplication = leaveApplication.DateOfApplication,
                                          DateOfApproval = leaveApplication.DateOfApproval,
                                          StatusId = leaveApplication.StatusId,
                                          CreatedDate = leaveApplication.CreatedDate,
                                          UpdatedDate = leaveApplication.UpdatedDate

                                      }).ToListAsync();
            return employeeLeaveList;
        }

        public async Task<LeaveApplication> GetLeaveDataByIdAsync(int leaveId)
        {
            return await _employeeManagementDataDbContext.LeaveApplications.FindAsync(leaveId);
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
