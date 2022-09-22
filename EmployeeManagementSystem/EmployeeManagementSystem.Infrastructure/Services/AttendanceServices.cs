using EmployeeManagementSystem.Core.Contracts.Infrastructure.Respositories;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.Services
{
    public class AttendanceServices : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        public AttendanceServices(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<Attendance> CreateAsync(Attendance attendance)
        {
            attendance.DateOfLog = DateTime.UtcNow;
            attendance.Timein = ClockIn(attendance);
            var result = await _attendanceRepository.CreateAsync(attendance);
            return result;
        }
        public DateTime ClockIn(Attendance attendance)
        {
            attendance.Timein = DateTime.UtcNow;
            return attendance.Timein;
        }

        public async Task<IEnumerable<AttendanceDto>> GetAttendanceAsync()
        {
            var result = await _attendanceRepository.GetAttendanceAsync();
            return result;
        }
        public async Task<AttendanceDto> GetAttendanceDataByIdAsync(int attendanceId)
        {
            var result = await _attendanceRepository.GetAttendanceDataByIdAsync(attendanceId);
            return result;
        }
        public async Task<AttendanceDto> GetEmployeeAttendanceById(int empId)
        {
            var result = await _attendanceRepository.GetEmployeeAttendanceById(empId);
            return result;
        }
        public async Task<IEnumerable<EmployeeAttendanceWithLeaves>> GetEmployeeAttendanceWithLeaves(int empId)
        {
            var result = await _attendanceRepository.GetEmployeeAttendanceWithLeaves(empId);
            return result;
        }
        
            
        public async Task<Attendance>UpdateAsync (int attendanceId, Attendance attendance)
        {
            var result = await _attendanceRepository.UpdateAsync(attendanceId, attendance);
            return result;
        }
        public async Task DeleteAttendanceRecord (int attendanceId)
        {
            await _attendanceRepository.DeleteAttendanceRecord(attendanceId);
           
        }

    }
}
