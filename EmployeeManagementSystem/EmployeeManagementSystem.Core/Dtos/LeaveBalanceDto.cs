using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Core.Dtos
{
    public class LeaveBalanceDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LeaveTypeName { get; set; }
        //public int EmployeeId { get; set; }
        public int Balance { get; set; }
    }
}
