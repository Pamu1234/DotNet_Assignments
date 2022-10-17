using EmployeeManagementSystem.Core.Contracts.Infrastructure.Respositories;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace EmployeeManagementSystem.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        
        private readonly ILeaveRepository _leaveRepository;
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IConfiguration _configuration;
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;


        public EmployeeService(IEmployeeRepository employeeRepository,IConfiguration iconfiguratoin, IAttendanceRepository attendanceRepository, ILeaveRepository leaveRepository, ILeaveBalanceRepository leaveBalanceRepository, EmployeemanagementDbContext employeeManagementDataDbContext)
        {
            _employeeRepository = employeeRepository;
            _configuration = iconfiguratoin;
            _leaveRepository = leaveRepository;
            _attendanceRepository = attendanceRepository;
            _leaveBalanceRepository = leaveBalanceRepository;
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
        }


        public async Task<Employee> CreateAsync(Employee employee)
        {
            var employeeData = new Employee();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetSection("AuthWebAPI").GetSection("BaseUrl").Value);
                var content = new StringContent(JsonSerializer.Serialize(employee), System.Text.Encoding.UTF8, "application/json");
                var result = await client.PostAsync(_configuration.GetSection("AuthWebAPI").GetSection("RegisterUrl").Value,content);
                string resultContent = await result.Content.ReadAsStringAsync();
                employeeData = JsonSerializer.Deserialize<Employee>(resultContent,new JsonSerializerOptions() { PropertyNameCaseInsensitive =true });

                if (employeeData is null)
                    return null;
            }

            var leaves = await _leaveRepository.GetLeavesAsync();

            List<LeaveBalance> leaveBalances = new List<LeaveBalance>();
            foreach (var item in leaves)
            {
                LeaveBalance leaveBalance = new LeaveBalance();
                leaveBalance.EmployeeId = employeeData.EmployeeId;
                leaveBalance.LeaveTypeId = item.LeaveTypeId;
                leaveBalance.Balance = item.NoOfDays;
                leaveBalances.Add(leaveBalance);
            }
            await _leaveBalanceRepository.CreateRangeAsync(leaveBalances);
            return employeeData;

        }

        public async Task< EmployeeDto> GetEmployeeAsync(int employeeId)
        {
            var remainingLeaves = await _employeeRepository.GetEmployeeAsync(employeeId);
            return remainingLeaves;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            var employeeData = await _employeeRepository.GetEmployeesAsync();
            return employeeData;

        }

        public Task DeleteEmployeeAsync(int employeeId)
        {
            return(_employeeRepository.DeleteEmployeeAsync(employeeId));
        }


        public async Task<EmployeeDto> UpdateAsync(int employeeId, Employee employee)
        {
            var employeeData = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            employeeData.FirstName = employee.FirstName;
            employeeData.LastName = employee.LastName;  
            employeeData.EmailId = employee.EmailId;
            employeeData.Contact = employee.Contact;
            employeeData.Address = employee.Address;
            employeeData.Salary = employee.Salary;
            employeeData.DepartmentId = employee.DepartmentId;
            employeeData.RoleId= employee.RoleId;

            var data = await _employeeRepository.UpdateAsync(employeeData);
            if(data != null)
            {
                var result = await _employeeRepository.GetEmployeeAsync(employeeId);
                return result;
            }
            return null;
        }

        public async Task<Attendance> EmployeeLogin(int empId)
        {
            var attendancedata = await _attendanceRepository.GetDate(empId);
            bool valid = true;
            var todayDate = DateTime.Now.ToString("yyyy-MM-dd");
            for( int i =0;i< attendancedata.Count();i++)
            {
              var  dateString = Convert.ToDateTime(attendancedata[i]).ToString("yyyy-MM-dd");
                if(dateString == todayDate)
                {
                    valid = false;
                    break;
                }
            }
            if (valid)
            {
                Attendance attendance = new()
                {
                    EmployeeId = empId,
                    DateOfLog = DateTime.UtcNow,
                    Timein = DateTime.UtcNow
                };

                var result = await _attendanceRepository.CreateAsync(attendance);

                return attendance;
            }
            return null;
        }

        public async Task<Attendance> EmployeeLogout(int empId)
        {
            var employee = await _attendanceRepository.GetLastAttendance(empId);
            if ( employee.Timein != null && employee.TimeOut == null && employee.LeaveTypeId == null) 
            { 
                employee.TimeOut = DateTime.UtcNow;
                var logOut = await _attendanceRepository.UpdateAsync(employee.AttendanceId,employee);
                return logOut;
            }
            return null;

        }


    }
}
