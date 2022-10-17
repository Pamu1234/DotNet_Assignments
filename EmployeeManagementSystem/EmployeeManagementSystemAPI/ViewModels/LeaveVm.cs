using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemAPI.ViewModels
{
    public class LeaveVm
    {
        [Required]
        public string LeaveTypeName { get; set; } = null!;
        [Required]
        public string? Description { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public int NoOfDays { get; set; }
        

    }
}
