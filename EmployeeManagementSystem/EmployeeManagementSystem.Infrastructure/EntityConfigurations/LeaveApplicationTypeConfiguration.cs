using EmployeeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.Infrastructure.EntityConfigurations
{
    internal class LeaveApplicationTypeConfiguration: IEntityTypeConfiguration<LeaveApplication>
    {
        public void Configure(EntityTypeBuilder<LeaveApplication> builder)
        {
            builder.ToTable("LeaveApplication");

            builder.HasIndex(e => e.LeaveTypeId, "UQ__LeaveApp__43BE8F15440C8D64")
                .IsUnique();

            builder.HasIndex(e => e.EmployeeId, "UQ__LeaveApp__7AD04F10772CBCE1")
                .IsUnique();

            builder.HasIndex(e => e.StatusId, "UQ__LeaveApp__C8EE206278AAB7DD")
                .IsUnique();

            builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.Property(e => e.DateOfApplication).HasColumnType("datetime");

            builder.Property(e => e.DateOfApproval).HasColumnType("datetime");

            builder.Property(e => e.EndDate).HasColumnType("datetime");

            builder.Property(e => e.Purpose)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.StartDate).HasColumnType("datetime");

            builder.Property(e => e.UpdatedDate).HasColumnType("date");

            builder.HasOne(d => d.Employee)
                .WithOne(p => p.LeaveApplication)
                .HasForeignKey<LeaveApplication>(d => d.EmployeeId)
                .HasConstraintName("FK__LeaveAppl__Emplo__5DCAEF64");

            builder.HasOne(d => d.LeaveType)
                .WithOne(p => p.LeaveApplication)
                .HasForeignKey<LeaveApplication>(d => d.LeaveTypeId)
                .HasConstraintName("FK__LeaveAppl__Leave__5EBF139D");

            builder.HasOne(d => d.Status)
                .WithOne(p => p.LeaveApplication)
                .HasForeignKey<LeaveApplication>(d => d.StatusId)
                .HasConstraintName("FK__LeaveAppl__Statu__5FB337D6");
        }
    }
}
