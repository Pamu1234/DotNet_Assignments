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
    internal class LeaveStatusEntityTypeConfiguration : IEntityTypeConfiguration<LeaveStatus>
    {
        public void Configure(EntityTypeBuilder<LeaveStatus> builder)
        {
            builder.HasKey(e => e.StatusId)
                    .HasName("PK__LeaveSta__C8EE20634A50E4AB");

            builder.ToTable("LeaveStatus");

            builder.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
