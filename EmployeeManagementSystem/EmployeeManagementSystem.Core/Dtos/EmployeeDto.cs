using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int? DepartmentName { get; set; }
        public int? RoleId { get; set; }
    }
}
