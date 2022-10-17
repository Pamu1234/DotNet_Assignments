namespace EmployeeManagementSystemAPI.ViewModels
{
    public class ForgotAttendanceRegularizeVm
    {
        public int? EmployeeId { get; set; }
        public DateTime? Timein { get; set; }
        public DateTime? TimeOut { get; set; }
    }
}
