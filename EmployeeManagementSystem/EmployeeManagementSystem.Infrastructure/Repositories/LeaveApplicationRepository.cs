using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Core.Enum;
using EmployeeManagementSystem.Infrastructure.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class LeaveApplicationRepository : ILeaveApplicationRepository
    {
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;
        
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public LeaveApplicationRepository(EmployeemanagementDbContext employeeManagementDataDbContext, IDbConnection dbConnection, ILeaveBalanceRepository leaveBalanceRepository,  IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
            _dapperConnection = dbConnection;
            _leaveBalanceRepository = leaveBalanceRepository;
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<LeaveApplication> CreateAsync(LeaveApplication leaveApplication)
        {
            _employeeManagementDataDbContext.LeaveApplications.Add(leaveApplication);
            await _employeeManagementDataDbContext.SaveChangesAsync();
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
        public async Task<IEnumerable<TotalLeavesOfEmployeeDto>> GetEmployeeLeavesData(int empId)
        {
            var employeeLeaves = await( from LeaveApplication in _employeeManagementDataDbContext.LeaveApplications
                                        join Employee in _employeeManagementDataDbContext.Employees
                                        on LeaveApplication.EmployeeId equals Employee.EmployeeId
                                        join Leaves in _employeeManagementDataDbContext.Leaves 
                                        on LeaveApplication.LeaveTypeId equals Leaves.LeaveTypeId
                                        where Employee.EmployeeId == empId
                                        select new TotalLeavesOfEmployeeDto
                                        { 
                                            DateOfApplication= LeaveApplication.DateOfApplication,
                                            LeaveTypeName= Leaves.LeaveTypeName,
                                            NoOfDays= LeaveApplication.NoOfDays,
                                            LeaveTypeId = LeaveApplication.LeaveTypeId
                                            
                                            
                                        }).ToListAsync();
            return employeeLeaves;


        }



        public async Task<LeaveApplication> UpdateAsync(int leaveId, UpdateLeaveApplicationRequestDto updateLeaveApplicationRequestDto , LeaveApplication leaveApplication )
        {
            var leaveApplicationToBeUpdate = await GetLeaveDataByIdAsync(leaveId);
            leaveApplication.EmployeeId = leaveApplication.EmployeeId;
            leaveApplication.LeaveTypeId = leaveApplication.LeaveTypeId;
            leaveApplication.DateOfApproval = DateTime.UtcNow;
            leaveApplication.StatusId = (int)updateLeaveApplicationRequestDto.Status;
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
