namespace EmployeeManagementSystemAPI.ViewModels
{
    public class AttendanceRegularizeVm
    {
        public int EmployeeId { get; set; }
        public DateTime TimeOut { get; set; }
        public String RegularizeDate { get; set; } = null!;


    }
}
