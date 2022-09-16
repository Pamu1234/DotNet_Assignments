namespace EmployeeManagementSystemAPI.ViewModels
{
    public class AttendanceVm
    {
        public int? EmployeeId { get; set; }
        public DateTime DateOfLog { get; set; }
        public TimeSpan Timein { get; set; }
        public TimeSpan TimeOut { get; set; }
    }
}
