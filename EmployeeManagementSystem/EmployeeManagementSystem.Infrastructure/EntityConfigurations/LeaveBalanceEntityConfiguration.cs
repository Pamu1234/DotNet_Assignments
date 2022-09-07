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

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.LeaveBalances)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__LeaveBala__Emplo__403A8C7D");

            builder.HasOne(d => d.LeaveType)
                .WithMany(p => p.LeaveBalances)
                .HasForeignKey(d => d.LeaveTypeId)
                .HasConstraintName("FK__LeaveBala__Leave__412EB0B6");
        }
    }
}
