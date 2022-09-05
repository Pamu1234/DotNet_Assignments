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
        private readonly ILogger<DepartmentsController> _logger;
        public EmployeesController(IEmployeeService employeeService, IMapper mapper, ILogger<DepartmentsController> logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
        }

        // Insert data
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Employee>>> Post([FromBody] EmployeeVm employeeVm)
        {
            _logger.LogInformation("Inserting data to Employee entity.");
            Employee employee = _mapper.Map<EmployeeVm, Employee>(employeeVm);
            var result = await _employeeService.CreateAsync(employee);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            _logger.LogInformation("Getting list of all Employee's.");
            var result = await _employeeService.GetEmployeesAsync();
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            _logger.LogInformation("Getting list of  department by ID:{id},", id);

            var result = await _employeeService.GetEmployeeAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Put(int id, [FromBody] EmployeeVm employeeVm)
        {
            if (id <= 0 || id != employeeVm.EmployeeId)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's Id.");
                return BadRequest();
            }
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
