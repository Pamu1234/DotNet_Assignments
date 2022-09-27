﻿using EmployeeManagementSystem.Core.Contracts.Infrastructure.Respositories;
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
            attendance.DateOfLog = DateTime.UtcNow;
            attendance.Timein = ClockIn(attendance);
            var result = await _attendanceRepository.CreateAsync(attendance);
            return result;
        }
        public DateTime? ClockIn(Attendance attendance)
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
            var attendanceDataOfEmp= await _attendanceRepository.GetEmployeeAttendanceWithLeaves(empId);
            var leavesOfEmployee = await _leaveApplicationRepository.GetEmployeeLeavesData(empId);

            var attendanceWithLeaves =  (from att in attendanceDataOfEmp
                                       join leave in leavesOfEmployee
                                       on att.LeaveTypeId equals leave.LeaveTypeId into leaveType
                                       from leaveRecord in leaveType.DefaultIfEmpty()
                                       where att.EmployeeId == empId
                                       select new EmployeeAttendanceWithLeaves
                                       {
                                           LeaveTypeId = att.LeaveTypeId,
                                           LeaveTypeName = att.LeaveTypeName,
                                           NoOfDays= att.NoOfDays,
                                           Timein= att.Timein,
                                           DateOfLog = att.DateOfLog,
                                           EffectiveHours =  att.EffectiveHours,
                                           EmployeeId = empId,
                                           TimeOut= att.TimeOut
                                       }).ToList();

            return attendanceWithLeaves;
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
