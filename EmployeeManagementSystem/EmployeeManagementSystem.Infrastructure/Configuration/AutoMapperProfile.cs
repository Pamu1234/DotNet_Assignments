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
            //CreateMap<Department, DepartmentToBeUpdatedDto>();
            CreateMap<DepartmentToBeUpdatedDto, Department>();
            CreateMap<EmloyeeCreateDto, Employee>();
            CreateMap<Employee, EmloyeeCreateDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Attendance, EmployeeAttendanceDto>();

            // CreateMap<AttendanceVm , Attendance>();

            CreateMap<Leaves, LeaveDto>();
            CreateMap<LeaveApplication, LeaveApplicationDto>();
            CreateMap<LeaveBalance, LeaveBalanceDto>();
            CreateMap<LeaveStatus, LeaveStatusDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<Attendance, EmployeeClockedInDto > ();
            CreateMap<Attendance, EmployeeClockedOutDto>();




        }
    }
}
