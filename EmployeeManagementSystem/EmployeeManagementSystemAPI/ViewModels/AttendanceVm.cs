using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemAPI.ViewModels
{
    public class AttendanceVm
    {
        [Required]
        public int? EmployeeId { get; set; }
        [Required]
        public DateTime DateOfLog { get; set; }
        [Required]
        public DateTime Timein { get; set; }
        [Required]
        public DateTime TimeOut { get; set; }
    }
}
