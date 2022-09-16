using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Core.Entities
{
    public partial class Leaves
    {
        public Leaves()
        {
            LeaveApplications = new HashSet<LeaveApplication>();
            LeaveBalances = new HashSet<LeaveBalance>();
        }

        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; } = null!;
        public string? Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int NoOfDays { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
        public virtual ICollection<LeaveBalance> LeaveBalances { get; set; }
    }
}
