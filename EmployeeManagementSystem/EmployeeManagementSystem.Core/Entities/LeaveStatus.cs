using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Core.Entities
{
    public partial class LeaveStatus
    {
        public LeaveStatus()
        {
            LeaveApplications = new HashSet<LeaveApplication>();
        }

        public int StatusId { get; set; }
        public string Status { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
    }
}
