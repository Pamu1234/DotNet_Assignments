namespace EmployeeManagementSystem.Core.Dtos
{
    public class EmployeeAttendanceWithLeavesDto
    {
        
        public DateTime DateOfLog { get; set; }
        public DateTime? Timein { get; set; }
        public DateTime? TimeOut { get; set; }
        public string? EffectiveHours { get; set; }
        public string LeaveTypeName { get; set; } = null!;
        public DateTime? StartDate { get; set; } =null!;

        
    }
}
