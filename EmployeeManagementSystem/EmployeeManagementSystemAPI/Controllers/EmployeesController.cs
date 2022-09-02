using AutoMapper;
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
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
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
        public async Task<ActionResult<IEnumerable<Employee>>> Post([FromBody] EmployeeVm employeeVm)
        {
            Employee employee =  _mapper.Map<EmployeeVm, Employee>(employeeVm);
            var result = await _employeeService.CreateAsync(employee);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Put(int id, [FromBody] EmployeeVm employeeVm)
        {
            Employee employee = _mapper.Map<EmployeeVm, Employee>(employeeVm); 
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
