using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Core.Contracts.Infrastructure.Respositories
{
    public interface IAttendanceRepository
    {
         Task<Attendance> CreateAsync(Attendance attendance);
         Task<IEnumerable<AttendanceDto>> GetAttendanceAsync();
         Task<Attendance> GetAttendanceDataByDate(string date, int empId);
        Task<TimeOnly> GetTime(int empId);
         Task<IEnumerable<EmployeeAttendanceDto>> GetEmployeeAttendanceById(int empId);
        Task<Attendance> UpdateAsync(int attendanceId, Attendance attendance);
        Task<Attendance> GetLastAttendance(int empId);
        Task<IEnumerable<EmployeeAttendanceWithLeavesDto>> EmployeeAttendanceWithLeaves(int empId);
        Task CreateRangeAsync(IEnumerable<Attendance> attendances);
        Task<List<DateTime>> GetDate(int empId);

    }
}
