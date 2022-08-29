using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public Task<Employee> CreateAsync(Employee employee)
        {
            return _employeeRepository.CreateAsync(employee);
        }

        public Task DeleteEmployeeAsync(int employeeId)
        {
            return _employeeRepository.DeleteEmployeeAsync(employeeId);
        }

        public Task<Employee> GetEmployeeAsync(int employeeId)
        {
            return _employeeRepository.GetEmployeeAsync(employeeId);
        }

        public Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            return _employeeRepository.GetEmployeesAsync();
        }

        public Task<Employee> UpdateAsync(int employeeId, Employee employee)
        {
            return _employeeRepository.UpdateAsync(employeeId, employee);
        }
    }
}
