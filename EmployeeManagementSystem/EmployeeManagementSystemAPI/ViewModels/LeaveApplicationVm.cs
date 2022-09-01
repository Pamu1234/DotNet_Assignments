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
        public DateTime DateOfApplication { get; set; }
        public DateTime DateOfApproval { get; set; }
        public int? StatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
