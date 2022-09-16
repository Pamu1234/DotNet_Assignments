using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Core.Contracts.Infrastructure.Respositories
{
    public interface  IAttendanceRepository
    {
        public Task<Attendance> CreateAsync(Attendance attendance);
        public  Task<IEnumerable<AttendanceDto>> GetAttendanceDetails();
        public Task<AttendanceDto> GetAttendanceDetailsById(int attendanceId);
        public Task<Attendance> UpdateAsync(int attendanceId, Attendance attendance);
        public  Task DeleteAttendanceRecord(int attendanceId);
    }
}
