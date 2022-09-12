using AutoMapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.ViewModels;

namespace EmployeeManagementSystemAPI.Configurations
{
    internal class AutoMapperProfile:Profile
    {
        internal AutoMapperProfile()
        {
            CreateMap<DepartmentVm, Department>();
            CreateMap<EmployeeVm, Employee>();
            CreateMap<LeaveVm, Leaves>();
            CreateMap<LeaveApplicationVm, LeaveApplication>();
            CreateMap<LeaveBalanceVm, LeaveBalance>();
            CreateMap<LeaveStatusVm, LeaveStatus>();
            CreateMap<RoleVm, Role>();

            CreateMap<Employee, EmployeeDto>();


        }
    }
}
