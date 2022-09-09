namespace EmployeeManagementSystemAPI.ViewModels
{
    public class LeaveApplicationVm
    {
        public int? EmployeeId { get; set; }
        public string Purpose { get; set; } = null!;
        public DateTime DateOfApplication { get; set; }
        public int? StatusId { get; set; }
    }
}
