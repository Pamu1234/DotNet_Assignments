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


        public async Task<Attendance> UpdateAsync(int attendanceId, Attendance attendance)
        {
            var result = await GetAttendanceDataByIdAsync(attendanceId);
            var effectiveHours = attendance.TimeOut - attendance.Timein;
            attendance.EffectiveHours = effectiveHours.HasValue ? effectiveHours.Value.ToString("hh\\:mm") : "";
            attendance.LeaveTypeId = attendance.LeaveTypeId;
             _employeeManagementDataDbContext.Attendances.Update(attendance);
           await  _employeeManagementDataDbContext.SaveChangesAsync();
            return attendance;
        }


        public async Task<IEnumerable<AttendanceDto>> GetAttendanceAsync()
        {
            var getAttendanceQuery = "select * from Attendance;";
            var result = await _dapperConnection.QueryAsync<AttendanceDto>(getAttendanceQuery);
            return result;
        }

        public async Task<AttendanceDto> GetAttendanceDataByIdAsync(int attendanceId)
        {
            var getAttendanceQueryById = "select * from Attendance where AttendanceId = @attendanceId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<AttendanceDto>(getAttendanceQueryById, new { attendanceId });
        }

        public async Task<IEnumerable<EmployeeAttendanceDto>> GetEmployeeAttendanceById(int empId)
        {
            var getEmployeeAttendance = "SELECT * FROM Attendance where  EmployeeId=@empId";
            var result = await _dapperConnection.QueryAsync<EmployeeAttendanceDto>(getEmployeeAttendance, new { empId });
            return result;
        }
        public async Task<IEnumerable<EmployeeAttendanceWithLeavesDto>> EmployeeAttendanceWithLeaves(int empId)
        {
            var totalData = await(from attendance in _employeeManagementDataDbContext.Attendances
                            join leave in _employeeManagementDataDbContext.Leaves
                            on attendance.LeaveTypeId equals leave.LeaveTypeId  into att
                            from atte in att.DefaultIfEmpty()
                            where attendance.EmployeeId == empId
                            select new EmployeeAttendanceWithLeavesDto
                            {
                                DateOfLog = attendance.DateOfLog,
                                EffectiveHours = attendance.EffectiveHours,
                                Timein = attendance.Timein,
                                TimeOut = attendance.TimeOut,
                                LeaveTypeName= atte.LeaveTypeName
                            }).Distinct().ToListAsync();
            return totalData;
        }

        public async Task CreateRangeAsync(IEnumerable<Attendance> attendances)
        {
            _employeeManagementDataDbContext.Attendances.AddRange(attendances);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }
        public async Task<Attendance> GetLastAttendance(int empId)
        {
            var getAttendanceQuery = "SELECT * FROM Attendance where EmployeeId = @empId order by DateOfLog desc";
            var result = await _dapperConnection.QueryFirstOrDefaultAsync<Attendance>(getAttendanceQuery, new {empId} );
            return result;

        }
        public async Task<List<DateTime>> GetDate(int empId)
        {
            var geDateQuery = "SELECT  CONVERT(Date,DateOfLog) FROM Attendance where EmployeeId = @empId order by DateOfLog desc";
            var result = (List <DateTime>)await _dapperConnection.QueryAsync<DateTime>(geDateQuery, new { empId });
            return result;
        }


        public async Task<Attendance> GetAttendanceDataByDate(string date, int empId)
        {
            var getAttendanceQueryById = "select * from Attendance where CONVERT(Date,Timein) = @date and EmployeeId=@empId";
            var result = await _dapperConnection.QueryFirstOrDefaultAsync<Attendance>(getAttendanceQueryById, new { date, empId });
            return result;
        }

        public async Task<TimeOnly> GetTime(int empId)
        {
            var geDateQuery = "SELECT  CONVERT(Time,TimeIn) FROM Attendance where EmployeeId = @empId order by DateOfLog desc";
            var result = await _dapperConnection.QueryFirstOrDefaultAsync<TimeOnly>(geDateQuery, new { empId });
            return result;
        }

    }
}
