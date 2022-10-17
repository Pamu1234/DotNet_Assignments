using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemAPI.ViewModels
{
    public class EmployeeVm
    {
        [StringLength(20), Required]
        public string FirstName { get; set; } = null!;
        [StringLength(15), Required]
        public string LastName { get; set; } = null!;
        [StringLength(35), Required]
        public string EmailId { get; set; } = null!;
        [StringLength(10), Required]
        public string Contact { get; set; } = null!;
        [StringLength(20), Required]
        public string Address { get; set; } = null!;
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public int? DepartmentId { get; set; }
        [Required]
        public int? RoleId { get; set; }
    }
}
