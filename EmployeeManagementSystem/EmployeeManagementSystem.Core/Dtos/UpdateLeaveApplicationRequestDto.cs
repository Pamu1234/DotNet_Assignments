using EmployeeManagementSystem.Core.Enum;

namespace EmployeeManagementSystem.Core.Dtos
{
    public class UpdateLeaveApplicationRequestDto
    {
        public LeaveApprovalStatus Status { get; set; }
        public int ApprovedBy { get; set; }
    }
}
