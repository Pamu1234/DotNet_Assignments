using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemAPI.ViewModels
{
    public class LeaveBalanceVm
    {
        [Required]
        public int? EmployeeId { get; set; }
        [Required]
        public int? LeaveTypeId { get; set; }
        [Required]
        public int Balance { get; set; }
    }
}
