namespace EmployeeManagementSystem.Core.Dtos
{
    public class DepartmentToBeUpdatedDto
    {
        public string DepartmentName { get; set; } = null!;
        public string? Description { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; } 
    }
}
