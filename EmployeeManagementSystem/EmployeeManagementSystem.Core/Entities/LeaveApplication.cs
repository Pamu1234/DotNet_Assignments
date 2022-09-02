using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Core.Entities
{
    public partial class LeaveApplication
    {
        public int? EmployeeId { get; set; }
        public int? LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Purpose { get; set; } = null!;
        public int NoOfDays { get; set; }
        public DateTime DateOfApplication { get; set; }
        public DateTime DateOfApproval { get; set; }
        public int? StatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int LeaveApplicationId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual Leave? LeaveType { get; set; }
        public virtual LeaveStatus? Status { get; set; }
    }
}
