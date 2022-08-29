using System;
using System.Collections.Generic;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmployeeManagementSystem.Infrastructure.Models
{
    public partial class EmployeeManagementDataDbContext : DbContext
    {
        public EmployeeManagementDataDbContext()
        {
        }

        public EmployeeManagementDataDbContext(DbContextOptions<EmployeeManagementDataDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Leave> Leaves { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RegisterEntityConfigurations();
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
