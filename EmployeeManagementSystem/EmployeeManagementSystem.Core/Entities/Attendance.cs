namespace EmployeeManagementSystem.Core.Entities
{

    public partial  class Attendance
    {

        public int AttendanceId { get; set; }
        public int? EmployeeId { get; set; }
        public int? LeaveTypeId { get; set; }
        public DateTime DateOfLog { get; set; }
        public DateTime Timein { get; set; }
        public DateTime? TimeOut { get; set; }
        public DateTime? LateTime { get; set; }
        public int? EffectiveHours { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual Leaves? LeaveType { get; set; }
    }
}
