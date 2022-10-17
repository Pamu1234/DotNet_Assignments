using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemAPI.ViewModels
{
    public class LeaveApplicationApproveVm
    {
        [Required]
        public DateTime? DateOfApproval { get; set; }
        [Required]
        public int StatusId { get; set; }
    }
}
