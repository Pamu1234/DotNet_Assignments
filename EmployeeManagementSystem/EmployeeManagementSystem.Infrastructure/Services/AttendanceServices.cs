using EmployeeManagementSystem.Core.Contracts.Infrastructure.Respositories;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories;

namespace EmployeeManagementSystem.Infrastructure.Services
{
    public class AttendanceServices : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ILeaveApplicationRepository _leaveApplicationRepository;
        private readonly ILeaveRepository _leaveRepository;

        public AttendanceServices(IAttendanceRepository attendanceRepository, ILeaveApplicationRepository leaveApplicationRepository, ILeaveRepository leaveRepository)
        {
            _attendanceRepository = attendanceRepository;
            _leaveApplicationRepository = leaveApplicationRepository;
            _leaveRepository = leaveRepository;
        }

        public async Task<Attendance> CreateAsync(Attendance attendance)
        {
            //var data = await _attendanceRepository.GetAttendanceAsync();

            attendance.DateOfLog = DateTime.UtcNow;
            attendance.Timein = DateTime.UtcNow; 
            var result = await _attendanceRepository.CreateAsync(attendance);
            return result;
        }
        //public DateTime? ClockIn(Attendance attendance)
        //{
        //    attendance.Timein = DateTime.UtcNow;
        //    return attendance.Timein;
        //}

        public async Task<IEnumerable<AttendanceDto>> GetAttendanceAsync()
        {
            var result = await _attendanceRepository.GetAttendanceAsync();
            return result;
        }

        public async Task<IEnumerable<EmployeeAttendanceDto>> GetEmployeeAttendanceById(int empId)
        {
            var result = await _attendanceRepository.GetEmployeeAttendanceById(empId);
            return result;
        }

        public async Task<IEnumerable<EmployeeAttendanceWithLeavesDto>> EmployeeAttendanceWithLeaves(int empId)
        {
            var result = await _attendanceRepository.EmployeeAttendanceWithLeaves(empId);
            return result;
        }

        public async Task<Attendance>UpdateAsync (int attendanceId, Attendance attendance)
        {
            var result = await _attendanceRepository.UpdateAsync(attendanceId, attendance);
            return result;
        }

        public async Task<bool> GetAttendanceDataByDate(string date,  int empId, DateTime regulizetime)
        {
            var result = await _attendanceRepository.GetAttendanceDataByDate(date, empId);
            if(result==null)
                return false;

            if(result.Timein !=null)
            {
                result.TimeOut = regulizetime;

                await _attendanceRepository.UpdateAsync(result.AttendanceId,result);

                return true;
            }
            return false;
        }

        public async Task<bool> RegularizeForgotMethod(Attendance attendance )
        {
            attendance.DateOfLog = DateTime.UtcNow;
            var effectiveHours = attendance.TimeOut - attendance.Timein;
            attendance.EffectiveHours = effectiveHours.HasValue ? effectiveHours.Value.ToString("hh\\:mm") : "";

            var attendanceResult = await _attendanceRepository.CreateAsync(attendance);
            if (attendanceResult !=null)
            {
                return true;
            }
            return false;
        }

    }
}
