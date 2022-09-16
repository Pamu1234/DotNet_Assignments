using AutoMapper;
using Dapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Respositories;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<AttendanceDto>> GetAttendanceDetails()
        {
            var getAttendanceQuery = "SELECT * FROM Attendance";
            var result = await _dapperConnection.QueryAsync<AttendanceDto>(getAttendanceQuery);
            return result;
        }

        public async Task<AttendanceDto> GetAttendanceDetailsById(int attendanceId)
        {
            var getAttendanceQueryById = "select * from Attendance where AttendanceId = @attendanceId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<AttendanceDto>(getAttendanceQueryById, new { attendanceId });
        }

        public async Task DeleteAttendanceRecord(int attendanceId)
        {
            var attendanceToBeDelete = await _employeeManagementDataDbContext.Attendances.FirstOrDefaultAsync(a => a.AttendanceId == attendanceId);
            var dataToBeDelete= _employeeManagementDataDbContext.Attendances.Remove(attendanceToBeDelete);
             await _employeeManagementDataDbContext.SaveChangesAsync();

        }

        public async Task<Attendance> UpdateAsync(int attendanceId, Attendance attendance)
        {
            var result = await GetAttendanceDetailsById(attendanceId);
            attendance.EffectiveHours = (TimeSpan)(result.Timein + result.TimeOut);

            //var effectiveHours = result.TimeOut - result.Timein;
            //var effectiveMinutes = result.TimeOut - result.Timein;
           _employeeManagementDataDbContext.Attendances.Update(attendance);
            _employeeManagementDataDbContext.SaveChanges();
            return attendance;


        }

    }
}
