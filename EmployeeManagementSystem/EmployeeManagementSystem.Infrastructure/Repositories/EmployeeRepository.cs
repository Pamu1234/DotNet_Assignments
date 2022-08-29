using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        public EmployeeRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
        }
        public async Task<Employee> CreateAsync(Employee employee)
        {
            _employeeManagementDataDbContext.Employees.Add(employee);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return employee;

        }
        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            var employeeList = await (from employee in _employeeManagementDataDbContext.Employees.Include(d => d.Department).Include(r => r.Role)
                                      select new EmployeeDto()
                                      {
                                          EmployeeId = employee.EmployeeId,
                                          FirstName = employee.FirstName,
                                          LastName = employee.LastName,
                                          EmailId = employee.EmailId,
                                          Contact = employee.Contact,
                                          Address = employee.Address,
                                          Salary = employee.Salary,
                                          DepartmentName = employee.DepartmentId,
                                          RoleId = employee.RoleId,

                                      }).ToListAsync();
            return employeeList;
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
            return await _employeeManagementDataDbContext.Employees.FindAsync(employeeId);
        }
        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employeeToBeDeleted = await GetEmployeeAsync(employeeId);
            _employeeManagementDataDbContext.Employees.Remove(employeeToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }
    }
}
