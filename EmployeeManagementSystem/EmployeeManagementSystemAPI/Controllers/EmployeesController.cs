using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
          _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            var result = await _employeeService.GetEmployeesAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var result = await _employeeService.GetEmployeeAsync(id);
            return Ok(result);
        }

        // Insert data
        [HttpPost]
        public async Task <ActionResult<IEnumerable<Employee >>>Post([FromBody] EmployeeVm employeeVm)
        {
            var employee = new Employee
            {
                FirstName = employeeVm.FirstName,
                LastName = employeeVm.LastName,
                EmailId = employeeVm.EmailId,
                Contact = employeeVm.Contact,
                Address = employeeVm.Address,
                Salary = employeeVm.Salary,
                DepartmentId = employeeVm.DepartmentId,
                RoleId = employeeVm.RoleId,
            };
            var result = await _employeeService.CreateAsync(employee);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerator <Employee>>> Put(int id, [FromBody] EmployeeVm employeeVm )
        {
            var employee = new Employee
            {
                EmployeeId = employeeVm.EmployeeId,
                FirstName = employeeVm.FirstName,
                LastName = employeeVm.LastName,
                EmailId = employeeVm.EmailId,
                Contact = employeeVm.Contact,
                Address = employeeVm.Address,
                Salary = employeeVm.Salary,
                DepartmentId = employeeVm.DepartmentId,
                RoleId = employeeVm.RoleId,

            };
            var result = await _employeeService.UpdateAsync(id, employee);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
        }
    }
}
