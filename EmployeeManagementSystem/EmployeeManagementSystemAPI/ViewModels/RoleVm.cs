namespace EmployeeManagementSystemAPI.ViewModels
{
    public class RoleVm
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
