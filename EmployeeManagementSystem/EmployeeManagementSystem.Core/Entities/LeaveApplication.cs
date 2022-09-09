using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Core.Entities
{
    public partial class LeaveApplication
    {
        public LeaveApplication()
        {

           // Employees = new HashSet<Employee>();
           // Leave = new HashSet<Leave>();
        }


        public int LeaveApplicationId { get; set; }
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
        public int ApprovedBy { get; set; }


        public virtual Employee Employee { get; set; }
        public virtual Leave Leave { get; set; }
        public virtual LeaveStatus LeaveStatus { get; set; }
    }
}
