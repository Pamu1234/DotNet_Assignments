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
    internal class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.UpdatedDate).HasColumnType("date");
        }
    }
}
