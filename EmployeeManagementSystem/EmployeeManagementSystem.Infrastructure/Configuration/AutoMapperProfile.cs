using AutoMapper;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Configuration
{
    internal class AutoMapperProfile:Profile
    {
        internal AutoMapperProfile()
        {
            CreateMap<Department, Department>();
            //CreateMap<EmployeeVm, Employee>();
            //CreateMap<LeaveVm, Leave>();
            //CreateMap<LeaveApplicationVm, LeaveApplication>();
            //CreateMap<LeaveBalanceVm, LeaveBalance>();
            //CreateMap<LeaveStatusVm, LeaveStatus>();
            //CreateMap<RoleVm, Role>();


        }
    }
}
