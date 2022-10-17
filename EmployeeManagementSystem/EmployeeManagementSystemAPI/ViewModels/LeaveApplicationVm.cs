using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemAPI.ViewModels
{
    public class LeaveApplicationVm
    {
        [Required]
        public int? EmployeeId { get; set; }
        [Required]
        public int  LeaveTypeId { get; set; }
        [Required]
        public string Purpose { get; set; } = null!;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
  
    }
}
