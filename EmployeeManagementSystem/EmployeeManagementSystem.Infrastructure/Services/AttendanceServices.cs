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
            var result = await  _attendanceRepository.CreateAsync(attendance);
            return result;
        }
        public async Task<IEnumerable<AttendanceDto>>GetAttendanceAsync()
        {
            var result = await _attendanceRepository.GetAttendanceAsync();
            return result;
        }
        public async Task<AttendanceDto> GetAttendanceDataByIdAsync(int attendanceId)
        {
            var result = await _attendanceRepository.GetAttendanceDataByIdAsync(attendanceId);
            return result;
        }

        public async Task<Attendance>UpdateAsync (int attendanceId, Attendance attendance)
        {
            var result = await _attendanceRepository.UpdateAsync(attendanceId, attendance);
            return result;
        }
        public Task DeleteAttendanceRecord (int attendanceId)
        {
            var result = _attendanceRepository.DeleteAttendanceRecord(attendanceId);
            return result;
        }

    }
}
