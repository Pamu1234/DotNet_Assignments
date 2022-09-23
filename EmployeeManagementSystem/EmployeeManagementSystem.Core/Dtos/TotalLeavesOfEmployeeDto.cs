using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Core.Dtos
{
    public class TotalLeavesOfEmployeeDto
    {
        public string LeaveTypeName { get; set; }
        public int NoOfDays { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateOfApplication { get; set; }
    }
}
