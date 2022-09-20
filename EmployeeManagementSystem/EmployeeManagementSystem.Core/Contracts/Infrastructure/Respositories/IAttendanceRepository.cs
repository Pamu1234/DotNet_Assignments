using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Core.Contracts.Infrastructure.Respositories
{
    public interface IAttendanceRepository
    {
        public Task<Attendance> CreateAsync(Attendance attendance);
        public Task<IEnumerable<AttendanceDto>> GetAttendanceAsync();
        public Task<AttendanceDto> GetAttendanceDataByIdAsync(int attendanceId);
        public Task<AttendanceDto> GetEmployeeAttendanceById(int empId);
        public Task<Attendance> UpdateAsync(int attendanceId, Attendance attendance);
        public Task DeleteAttendanceRecord(int attendanceId);
        Task<Attendance> GetLastAttendance(int empId);
    }
}
