using EmployeeManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Core.Dtos
{
    public class LeaveStatusDto
    {
        public string Status { get; set; } = null!;
        public string? Description { get; set; }
    }
}
