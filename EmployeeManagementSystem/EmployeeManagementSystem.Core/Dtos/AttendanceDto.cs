using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Core.Dtos
{
    public class AttendanceDto
    {
        public int AttendanceId { get; set; }
        public int? EmployeeId { get; set; }
        public int? LeaveTypeId { get; set; }
        public DateTime DateOfLog { get; set; }
        public TimeSpan Timein { get; set; }
        public TimeSpan? TimeOut { get; set; }
    }
}
