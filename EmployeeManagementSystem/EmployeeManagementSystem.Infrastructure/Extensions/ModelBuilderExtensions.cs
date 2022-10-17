using EmployeeManagementSystem.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Infrastructure.Extensions
{
    internal static class ModelBuilderExtensions
    {
        internal static void RegisterEntityConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AttendanceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LeaveApplicationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LeaveBalanceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LeaveEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LeaveStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());

        }
    }
}
