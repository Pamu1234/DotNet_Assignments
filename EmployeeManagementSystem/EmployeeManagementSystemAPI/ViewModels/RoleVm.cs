using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemAPI.ViewModels
{
    public class RoleVm
    {
        [Required]
        public string RoleName { get; set; } = null!;
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

    }
}
