namespace EmployeeManagementSystemAPI.ViewModels
{
    public class LeaveApplicationVm
    {
        public int? EmployeeId { get; set; }
        public int  LeaveTypeId { get; set; }
        public string LeaveName { get; set; }
        public string Purpose { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
