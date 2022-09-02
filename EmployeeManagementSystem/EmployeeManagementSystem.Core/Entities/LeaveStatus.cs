using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Core.Entities
{
    public partial class LeaveStatus
    {
        public int StatusId { get; set; }
        public string Status { get; set; } = null!;
        public string? Description { get; set; }

        public virtual LeaveApplication? LeaveApplication { get; set; }
    }
}
