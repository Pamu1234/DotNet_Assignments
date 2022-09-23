using AutoMapper;
using Dapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Respositories;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;
        private readonly IMapper _mapper;

        public AttendanceRepository(EmployeemanagementDbContext employeemanagementDbContext, IDbConnection dbConnection , IMapper mapper)
        {
            _employeeManagementDataDbContext = employeemanagementDbContext;
            _dapperConnection = dbConnection;
            _mapper = mapper;
        }

        public async Task<Attendance> CreateAsync(Attendance attendance)
        {
            _employeeManagementDataDbContext.Attendances.Add(attendance);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return attendance;
        }


        public async Task DeleteAttendanceRecord(int attendanceId)
        {
            var attendanceToBeDelete = await _employeeManagementDataDbContext.Attendances.FirstAsync(a => a.AttendanceId == attendanceId);
              _employeeManagementDataDbContext.Attendances.Remove(attendanceToBeDelete);
             await _employeeManagementDataDbContext.SaveChangesAsync();

        }

        public async Task<Attendance> UpdateAsync(int attendanceId, Attendance attendance)
        {
            var result = await GetAttendanceDataByIdAsync(attendanceId);
            var effectiveHours = attendance.TimeOut - attendance.Timein;
            attendance.EffectiveHours = Convert.ToString(effectiveHours.Value.Hours);
            //attendance.LeaveTypeId = leaves.LeaveTypeId;
            _employeeManagementDataDbContext.Attendances.Update(attendance);
           await  _employeeManagementDataDbContext.SaveChangesAsync();
            return attendance;
        }

        public async Task<IEnumerable<AttendanceDto>> GetAttendanceAsync()
        {
            var getAttendanceQuery = "SELECT * FROM Attendance";
            var result = await _dapperConnection.QueryAsync<AttendanceDto>(getAttendanceQuery);
            return result;
        }

        public async Task<AttendanceDto> GetAttendanceDataByIdAsync(int attendanceId)
        {
            var getAttendanceQueryById = "select * from Attendance where AttendanceId = @attendanceId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<AttendanceDto>(getAttendanceQueryById, new { attendanceId });
        }

        public async Task<AttendanceDto> GetEmployeeAttendanceById(int empId)
        {
            var getEmployeeAttendance = "execute spEmployeeAttendance @empId";
            var result = await _dapperConnection.QueryFirstOrDefaultAsync<AttendanceDto>(getEmployeeAttendance, new { empId });
            return result;
        }
        public async Task<IEnumerable<EmployeeAttendanceWithLeaves>> GetEmployeeAttendanceWithLeaves(int empId)
        {
           
            var data = await (from Employee in _employeeManagementDataDbContext.Employees
                              join Attendance in _employeeManagementDataDbContext.Attendances
                              on Employee.EmployeeId equals Attendance.EmployeeId
                              join LeaveApplication in _employeeManagementDataDbContext.LeaveApplications
                              on Employee.EmployeeId equals LeaveApplication.EmployeeId
                              where Attendance.EmployeeId == empId
                              select new EmployeeAttendanceWithLeaves
                              {
                                  EmployeeId = empId,
                                  DateOfLog = Attendance.DateOfLog,
                                  Timein = Attendance.Timein,
                                  TimeOut = Attendance.TimeOut,
                                  EffectiveHours = Attendance.EffectiveHours,
                                  LeaveTypeName = LeaveApplication.LeaveType.LeaveTypeName,
                                  NoOfDays = LeaveApplication.NoOfDays
                              }).ToListAsync();
            return data;
                
        }

        public async Task CreateRangeAsync(IEnumerable<Attendance> attendances)
        {
            _employeeManagementDataDbContext.Attendances.AddRange(attendances);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }
        public async Task<Attendance> GetLastAttendance(int empId)
        {
            var getAttendanceQuery = "SELECT * FROM Attendance where EmployeeId = @empId order by AttendanceId desc";
            var result = await _dapperConnection.QueryFirstOrDefaultAsync<Attendance>(getAttendanceQuery, new {empId} );
            return result;

        }

    }
}
