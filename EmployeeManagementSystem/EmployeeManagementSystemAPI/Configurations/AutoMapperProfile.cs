﻿using AutoMapper;
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
            CreateMap<LeaveVm, Leave>();
            CreateMap<LeaveApplicationVm, LeaveApplication>();
            CreateMap<LeaveBalanceVm, LeaveBalance>();
            CreateMap<LeaveStatusVm, LeaveStatus>();
            CreateMap<RoleVm, Role>();


        }
    }
}