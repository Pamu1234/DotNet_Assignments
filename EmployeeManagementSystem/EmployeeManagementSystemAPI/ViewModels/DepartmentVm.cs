namespace EmployeeManagementSystemAPI.ViewModels
{
    public class DepartmentVm
    {
        public string DepartmentName { get; set; } = null!;
        public string? Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
