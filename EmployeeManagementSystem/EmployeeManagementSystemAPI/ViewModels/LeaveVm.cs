namespace EmployeeManagementSystemAPI.ViewModels
{
    public class LeaveVm
    {
        public string LeaveTypeName { get; set; } = null!;
        public string? Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NoOfDays { get; set; }
        

    }
}
