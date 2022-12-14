using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class LeaveApplicationRepository : ILeaveApplicationRepository
    {
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;

        public LeaveApplicationRepository(EmployeemanagementDbContext employeeManagementDataDbContext, IDbConnection dbConnection, ILeaveBalanceRepository leaveBalanceRepository)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
            _dapperConnection = dbConnection;
            _leaveBalanceRepository = leaveBalanceRepository;
        }

        public async Task<LeaveApplication> CreateAsync(LeaveApplication leaveApplication)
        {
            _employeeManagementDataDbContext.LeaveApplications.Add(leaveApplication);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            if (leaveApplication != null)
            {

                var data = await (from leave in _employeeManagementDataDbContext.Leaves
                                  join leaveBlance in _employeeManagementDataDbContext.LeaveBalances
                                  on leave.LeaveTypeId equals leaveBlance.LeaveTypeId
                                  where leave.LeaveTypeId == leaveApplication.LeaveTypeId && leaveBlance.EmployeeId == leaveApplication.EmployeeId

                                  select leaveBlance).FirstOrDefaultAsync();

                data.Balance = data.Balance - leaveApplication.NoOfDays;
                await _leaveBalanceRepository.UpdateAsync(data.LeaveBalanceId, data);
            }
            return leaveApplication;

        }

        public async Task<IEnumerable<LeaveApplicationDto>> GetLeaveApplicationAsync()
        {
            var getLeaveApplicationDataQuery = await (from LeaveApplication in _employeeManagementDataDbContext.LeaveApplications
                                                      join Employee in _employeeManagementDataDbContext.Employees
                                                      on LeaveApplication.EmployeeId equals Employee.EmployeeId
                                                      join Leaves in _employeeManagementDataDbContext.Leaves
                                                      on LeaveApplication.LeaveTypeId equals Leaves.LeaveTypeId
                                                      select new LeaveApplicationDto
                                                      {
                                                          Firstname = Employee.FirstName,
                                                          Lastname = Employee.LastName,
                                                          LeaveTypeName = Leaves.LeaveTypeName,
                                                          DateOfApplication = LeaveApplication.DateOfApplication,
                                                          Purpose = LeaveApplication.Purpose,
                                                          NoOfDays = LeaveApplication.NoOfDays,
                                                          StatusId = LeaveApplication.StatusId,

                                                      }).ToListAsync();
            return getLeaveApplicationDataQuery;
        }

        public async Task<LeaveApplication> GetLeaveDataByIdAsync(int leaveId)
        {
            var getEmployeeByIdQuery = "select * from LeaveApplication where LeaveApplicationId = @leaveId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<LeaveApplication>(getEmployeeByIdQuery, new { leaveId });
        }

        public async Task<IEnumerable<LeaveApplicationDto >> GetEmployeeLeaveRequest(int empId)
        {
            var employeeLeaveRequest = await (from LeaveApplication in _employeeManagementDataDbContext.LeaveApplications
                                              join Employee in _employeeManagementDataDbContext.Employees
                                              on LeaveApplication.EmployeeId equals Employee.EmployeeId
                                              join leaves in _employeeManagementDataDbContext.Leaves
                                              on LeaveApplication.LeaveTypeId equals leaves.LeaveTypeId
                                              where Employee.EmployeeId== empId
                                              select new LeaveApplicationDto
                                              {
                                                  Firstname = Employee.FirstName,
                                                  Lastname = Employee.LastName,
                                                  LeaveTypeName = leaves.LeaveTypeName,
                                                  Purpose = LeaveApplication.Purpose,
                                                  NoOfDays = LeaveApplication.NoOfDays,
                                                  StatusId = LeaveApplication.StatusId,
                                                  DateOfApplication = LeaveApplication.DateOfApplication,

                                              }).ToListAsync();
            return employeeLeaveRequest;
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
