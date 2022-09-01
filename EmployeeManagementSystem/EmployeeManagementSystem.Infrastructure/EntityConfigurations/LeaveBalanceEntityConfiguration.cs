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
    internal class LeaveBalanceEntityConfiguration:  IEntityTypeConfiguration<LeaveBalance>
    {
        public void Configure(EntityTypeBuilder<LeaveBalance> builder)
        {
            builder.ToTable("LeaveBalance");

            builder.HasIndex(e => e.LeaveTypeId, "UQ__LeaveBal__43BE8F15AD7316DB")
                .IsUnique();

            builder.HasIndex(e => e.EmployeeId, "UQ__LeaveBal__7AD04F104C4CD26D")
                .IsUnique();

            builder.HasOne(d => d.Employee)
                .WithOne(p => p.LeaveBalance)
                .HasForeignKey<LeaveBalance>(d => d.EmployeeId)
                .HasConstraintName("FK__LeaveBala__Emplo__5629CD9C");

            builder.HasOne(d => d.Leave)
                .WithOne(p => p.LeaveBalance)
                .HasForeignKey<LeaveBalance>(d => d.LeaveTypeId)
                .HasConstraintName("FK__LeaveBala__Leave__571DF1D5");
        }
    }
}
