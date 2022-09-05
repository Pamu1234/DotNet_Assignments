namespace EmployeeManagementSystemAPI.ViewModels
{
    public class LeaveVm
    {
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; } = null!;
        public string? Description { get; set; }

    }
}
