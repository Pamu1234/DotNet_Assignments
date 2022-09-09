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

            builder.Property(e => e.DateOfApplication).HasColumnType("datetime");

            builder.Property(e => e.DateOfApproval).HasColumnType("datetime");

            builder.Property(e => e.EndDate).HasColumnType("datetime");

            builder.Property(e => e.Purpose)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.StartDate).HasColumnType("datetime");


            builder.HasOne(d => d.Employee)
                .WithMany(p => p.LeaveApplications)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__LeaveAppl__Emplo__45F365D3");

            builder.HasOne(d => d.Leave)
                .WithMany(p => p.LeaveApplications)
                .HasForeignKey(d => d.LeaveTypeId)
                .HasConstraintName("FK__LeaveAppl__Leave__46E78A0C");

            builder.HasOne(d => d.LeaveStatus)
               .WithMany(p => p.LeaveApplications)
               .HasForeignKey(d => d.StatusId)
               .HasConstraintName("FK__LeaveAppl__Leave__46E78A0C");


        }
    }
}
