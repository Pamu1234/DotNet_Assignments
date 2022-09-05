namespace EmployeeManagementSystemAPI.ViewModels
{
    public class LeaveApplicationVm
    {
        public int? EmployeeId { get; set; }
        public int? LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDatew { get; set; }
        public string Purpose { get; set; } = null!;
        public int NoOfDays { get; set; }

    }
}
