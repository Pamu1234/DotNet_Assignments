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
            // Vm to Entities
            CreateMap<AttendanceVm, Attendance>();
            CreateMap<DepartmentVm, Department>();
            CreateMap<EmployeeVm, Employee>();
            CreateMap<Employee, EmployeeVm>();
            CreateMap<LeaveVm, Leaves>();
            CreateMap<LeaveApplicationVm, LeaveApplication>();
            CreateMap<LeaveBalanceVm, LeaveBalance>();
            CreateMap<LeaveApplication, LeaveApplicationVm>();
            CreateMap<LeaveStatusVm, LeaveStatus>();
            CreateMap<RoleVm, Role>();
            CreateMap<Employee, EmloyeeCreateDto>();

            CreateMap<Employee, EmployeeDto>();
            CreateMap<LeaveBalance, LeaveBalanceDto>();

            CreateMap<DepartmentToBeUpdatedDto, Department>();
            CreateMap<EmloyeeCreateDto, Employee>();
            CreateMap<Attendance, EmployeeClockedInDto>();
            CreateMap<Attendance, EmployeeClockedOutDto>();
            CreateMap<LoginVm, Employee>();

            CreateMap<ForgotAttendanceRegularizeVm, Attendance>();


        }
    }
}
