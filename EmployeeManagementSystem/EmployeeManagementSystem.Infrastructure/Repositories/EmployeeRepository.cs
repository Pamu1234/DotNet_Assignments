using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;
        public EmployeeRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext,IDbConnection dbConnection)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
            _dapperConnection = dbConnection;
        }
        public async Task<Employee> CreateAsync(Employee employee)
        {
            _employeeManagementDataDbContext.Employees.Add(employee);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return employee;

        }
        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            var getEmployeesQuery = "select * from Employees";
            var result = await _dapperConnection.QueryAsync<EmployeeDto>(getEmployeesQuery);
            return result;

        }
        public async Task<Employee> UpdateAsync(int employeeId, Employee employee)
        {
            var employeToBeUpdate = await GetEmployeeAsync(employeeId);
            employeToBeUpdate.EmployeeId = employee.EmployeeId;
            employeToBeUpdate.FirstName = employee.FirstName;
            employeToBeUpdate.LastName = employee.LastName;
            employeToBeUpdate.EmailId = employee.EmailId;
            employeToBeUpdate.Contact = employee.Contact;
            employeToBeUpdate.Address = employee.Address;
            employeToBeUpdate.Salary = employee.Salary;
            employeToBeUpdate.DepartmentId = employee.DepartmentId;
            employeToBeUpdate.RoleId = employee.RoleId;
            _employeeManagementDataDbContext.Employees.Update(employeToBeUpdate);
            _employeeManagementDataDbContext.SaveChanges();
            return employeToBeUpdate;
        }
        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            var getEmployeeByIdQuery = "select * from Employees where EmployeeId = @employeeId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<Employee>(getEmployeeByIdQuery, new { employeeId });
        }
        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employeeToBeDeleted = await GetEmployeeAsync(employeeId);
            _employeeManagementDataDbContext.Employees.Remove(employeeToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }
    }
}
