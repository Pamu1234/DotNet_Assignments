using EmployeeManagementSystem.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Core.Entities
{
    public partial class EmployeemanagementDbContext : DbContext
    {
        public EmployeemanagementDbContext()
        {
        }

        public EmployeemanagementDbContext(DbContextOptions<EmployeemanagementDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Leaves> Leaves { get; set; } = null!;
        public virtual DbSet<LeaveApplication> LeaveApplications { get; set; } = null!;
        public virtual DbSet<LeaveBalance> LeaveBalances { get; set; } = null!;
        public virtual DbSet<LeaveStatus> LeaveStatuses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RegisterEntityConfigurations();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
