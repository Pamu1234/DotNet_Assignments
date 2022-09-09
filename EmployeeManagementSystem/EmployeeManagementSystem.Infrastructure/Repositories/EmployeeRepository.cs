using AutoMapper;
using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;
        private readonly IMapper _mapper;
        public EmployeeRepository(EmployeemanagementDbContext employeeManagementDataDbContext,IDbConnection dbConnection,IMapper mapper)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
            _dapperConnection = dbConnection;
            _mapper = mapper;
        }


        public async Task<Employee> CreateAsync(Employee employee)
        {
            _employeeManagementDataDbContext.Employees.Add(employee);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return employee;

        }
        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            var getEmployeesQuery = "execute GetEmployeesDataList";
            var result = await _dapperConnection.QueryAsync<EmployeeDto>(getEmployeesQuery);
            return result;

        }
        public async Task<EmployeeDto> GetEmployeeAsync(int employeeId)
        {
            var getEmployeeByIdQuery = "execute GetEmployeesDataById @employeeId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<EmployeeDto>(getEmployeeByIdQuery, new { employeeId });
        }

        public async Task<Employee> UpdateAsync(int employeeId, Employee employee)
        {
            var employeToBeUpdate = await GetEmployeeAsync(employeeId);
            employee.EmployeeId = employeeId;
            employee.FirstName = employee.FirstName;
            employee.LastName = employee.LastName;
            employee.EmailId = employee.EmailId;
            employee.Contact = employee.Contact;
            employee.Address = employee.Address;
            employee.Salary = employee.Salary;
            _employeeManagementDataDbContext.Employees.Update(employee);
            _employeeManagementDataDbContext.SaveChanges();
            return employee;
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _employeeManagementDataDbContext.Employees.FirstOrDefaultAsync(s => s.EmployeeId == employeeId);
            _employeeManagementDataDbContext.Employees.Remove(employee);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }

        
    }
}
