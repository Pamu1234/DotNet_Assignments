using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemAPI.ViewModels
{
    public class LoginVm
    {
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
