using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Core.Dtos
{
    public class LeaveDto
    {
        public string LeaveTypeName { get; set; } = null!;
        public string? Description { get; set; }

    }
}
