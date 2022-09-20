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
       // public int? LeaveTypeId { get; set; }
        public DateTime DateOfLog { get; set; }
        public DateTime Timein { get; set; }
        public DateTime? TimeOut { get; set; }
        public int? EffectiveHours { get; set; }
    }
}
