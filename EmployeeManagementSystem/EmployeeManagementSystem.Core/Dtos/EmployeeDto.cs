using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Core.Dtos
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Salary { get; set; }
        public string? DepartmentName { get; set; }
        public string? RoleName { get; set; }

    }
}
