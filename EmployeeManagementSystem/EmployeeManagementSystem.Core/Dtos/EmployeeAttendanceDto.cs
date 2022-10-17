namespace EmployeeManagementSystem.Core.Dtos
{
    public class EmployeeAttendanceDto
    {
        public DateTime DateOfLog { get; set; }
        public DateTime Timein { get; set; }
        public DateTime? TimeOut { get; set; }
        public string? EffectiveHours { get; set; }
    }
}
