using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;
        public EmployeeRepository(EmployeemanagementDbContext employeeManagementDataDbContext,IDbConnection dbConnection)
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
