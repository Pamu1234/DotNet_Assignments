using EmployeeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.Infrastructure.EntityConfigurations
{
    internal class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Address)
                     .HasMaxLength(200)
                     .IsUnicode(false);

            builder.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.PasswordSalt)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.Salary).HasColumnType("decimal(8, 3)");

            builder.HasOne(d => d.Department)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employees__Depar__3A81B327");

            builder.HasOne(d => d.Role)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Employees__RoleI__3B75D760");
        }
    }
}
