using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Core.Entities
{
    public partial class LeaveBalance
    {
        public int LeaveBalanceId { get; set; }
        public int? EmployeeId { get; set; }
        public int? LeaveTypeId { get; set; }
        public int Balance { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual Leave? LeaveType { get; set; }
    }
}
