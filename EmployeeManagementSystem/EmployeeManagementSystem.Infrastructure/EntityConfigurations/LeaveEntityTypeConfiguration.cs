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
    internal class LeaveEntityTypeConfiguration : IEntityTypeConfiguration<Leaves>
    {
        public void Configure(EntityTypeBuilder<Leaves> builder)
        {
            builder.HasKey(e => e.LeaveTypeId)
                    .HasName("PK__Leaves__43BE8F14865D89E6");

            builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LeaveTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
           // builder.Property(e => e.NoOfDays).IsRequired();

            builder.Property(e => e.UpdatedDate).HasColumnType("date");
        }
    }
}
