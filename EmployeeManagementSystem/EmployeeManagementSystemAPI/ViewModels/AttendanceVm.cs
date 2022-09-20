namespace EmployeeManagementSystemAPI.ViewModels
{
    public class AttendanceVm
    {
        public int? EmployeeId { get; set; }
        public DateTime DateOfLog { get; set; }
        public DateTime Timein { get; set; }
        public DateTime TimeOut { get; set; }
    }
}
