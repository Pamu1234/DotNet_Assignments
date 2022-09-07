using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Core.Dtos
{
    public class EmployeeDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? DepartmentName { get; set; }
        public string? Role { get; set; }
        public int EmpId { get; set; }

    }
}
