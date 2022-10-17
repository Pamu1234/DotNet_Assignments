using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Core.Contracts.Infrastructure.Services
{
    public interface IAttendanceService
    {
         Task<Attendance> CreateAsync(Attendance attendance);
         Task<IEnumerable<AttendanceDto>> GetAttendanceAsync();
         Task<IEnumerable<EmployeeAttendanceDto>> GetEmployeeAttendanceById(int empId);
        Task<bool> GetAttendanceDataByDate(string date, int empId,DateTime regulizetime);
        //Task<TimeOnly> GetTime(int empId);
        Task<Attendance> UpdateAsync(int attendanceId, Attendance attendance);
      //  Task<AttendanceDto> GetAttendanceDataByIdAsync(int attendanceId);
        Task<IEnumerable<EmployeeAttendanceWithLeavesDto>> EmployeeAttendanceWithLeaves(int empId);
        Task<bool> RegularizeForgotMethod(Attendance attendance);
    }
}
