using AutoMapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Configuration
{
    internal class AutoMapperProfile:Profile
    {
        internal AutoMapperProfile()
        {
            CreateMap<Department, DepartmentDto>();

            CreateMap<EmployeeDto, Employee>();

            CreateMap<Leave, LeaveDto>();
            CreateMap<LeaveApplication, LeaveApplicationDto>();
            CreateMap<LeaveBalance, LeaveBalanceDto>();
            CreateMap<LeaveStatus, LeaveStatusDto>();
            CreateMap<Role, RoleDto>();


        }
    }
}
