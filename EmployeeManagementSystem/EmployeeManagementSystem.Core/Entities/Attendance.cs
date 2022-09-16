using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Core.Entities
{
    public partial  class Attendance
    {
        public int AttendanceId { get; set; }
        public int? EmployeeId { get; set; }
        public int? LeaveTypeId { get; set; }
        public DateTime DateOfLog { get; set; }
        public TimeSpan Timein { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public TimeSpan? LateTime { get; set; }
        public TimeSpan EffectiveHours { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual Leaves? LeaveType { get; set; }
    }
}
