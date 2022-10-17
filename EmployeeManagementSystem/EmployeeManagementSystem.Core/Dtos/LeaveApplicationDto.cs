namespace EmployeeManagementSystem.Core.Dtos
{
    public class LeaveApplicationDto
    {
        public int LeaveApplicationId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string LeaveTypeName { get; set; }
       // public int LeaveTypeId { get; set; }
        public string Purpose { get; set; } = null!;
        public int NoOfDays { get; set; }
        public DateTime DateOfApplication { get; set; }
        public int? StatusId { get; set; }

    }
}
