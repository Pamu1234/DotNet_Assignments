namespace EmployeeManagementSystem.Core.Dtos
{
    public class EmployeeClockedInDto
    {
        public int AttendanceId { get; set; }
        public DateTime DateOfLog { get; set; }
        public DateTime Timein { get; set; }
    }
}
