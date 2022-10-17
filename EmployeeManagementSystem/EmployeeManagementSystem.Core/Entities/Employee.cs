namespace EmployeeManagementSystem.Core.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            Attendances = new HashSet<Attendance>();
            LeaveApplications = new HashSet<LeaveApplication>();
            LeaveBalances = new HashSet<LeaveBalance>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }
        public int? RoleId { get; set; }
        public string Password { get; set; } = null!;
        public string? PasswordSalt { get; set; } = null!;

        public virtual Department? Department { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
        public virtual ICollection<LeaveBalance> LeaveBalances { get; set; }
    }
}
