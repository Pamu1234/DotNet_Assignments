using EmployeeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.EntityConfigurations
{
    internal class AttendanceEntityTypeConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("Attendance");

                builder.Property(e => e.DateOfLog).HasColumnType("datetime");

                builder.Property(e => e.LateTime).HasColumnType("datetime");

                builder.Property(e => e.TimeOut).HasColumnType("datetime");

                builder.Property(e => e.Timein).HasColumnType("datetime");

                builder.HasOne(d => d.Employee)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Attendanc__Emplo__6FE99F9F");

                builder.HasOne(d => d.LeaveType)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .HasConstraintName("FK__Attendanc__Leave__70DDC3D8");
        }
    }
}
