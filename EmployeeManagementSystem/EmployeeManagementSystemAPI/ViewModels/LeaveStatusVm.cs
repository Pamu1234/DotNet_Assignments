namespace EmployeeManagementSystemAPI.ViewModels
{
    public class LeaveStatusVm
    {
        public int StatusId { get; set; }
        public string Status { get; set; } = null!;
        public string? Description { get; set; }
    }
}
