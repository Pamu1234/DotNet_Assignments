using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories;
using EmployeeManagementSystemAPI.Infrastructure.Specs;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Authorize]
    [Route("employee")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class EmployeesController : ApiControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILeaveApplicationService _leaveApplicationService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentsController> _logger;
        public EmployeesController(IEmployeeService employeeService, IEmployeeRepository employeeReository, ILeaveApplicationService leaveApplicationService, IMapper mapper, ILogger<DepartmentsController> logger)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeReository;
            _leaveApplicationService = leaveApplicationService;
            _mapper = mapper;
            _logger = logger;
        }

        // Insert data
        [MapToApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Employee>> Post([FromBody] EmployeeVm employeeVm)
        {
            _logger.LogInformation("Inserting data to Employee entity.");
            Employee employee = _mapper.Map<EmployeeVm, Employee>(employeeVm);

            var result = await _employeeService.CreateAsync(employee);
            var employeeDto = _mapper.Map<Employee, EmployeeVm>(result);
            return Ok(employeeDto);
        }

        [MapToApiVersion("1.0")]
        [Route("")]
        [Authorize(Roles = "Software Developer,Web Developer,Manager,Team Leader,Developer,Admin")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<EmployeeDto>> Get()
        {
            _logger.LogInformation("Getting list of all Employee's.");
            var result = await _employeeService.GetEmployeesAsync();
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int id)
        {
            _logger.LogInformation("Getting Data of  employee by ID:{id},", id);
            var employee = await _employeeService.GetEmployeeAsync(id);
            return Ok(employee);
        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpPut]
        [Authorize(Roles = "Manager")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<Employee>> Put(int id, [FromBody] EmployeeVm employeeVm)
        {
            if(id<=0)
            {
                return BadRequest("Id field can't be <= zero OR it doesn't match with model's Id.");
            }
            Employee employee = _mapper.Map<EmployeeVm, Employee>(employeeVm);
            var employeeDataa = await _employeeService.UpdateAsync(id, employee);
            if(employeeDataa is null)
            {
                return BadRequest("Employee is not available");
            }
            
            return Ok(employeeDataa);
        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpDelete]
        [Authorize(Roles = "Manager")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> Delete(int id)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if(existingEmployee !=null)
            {
                 var result = await _employeeRepository.DeleteEmployeeAsync(id);
                if(result != null)
                {
                    return NoContent();
                }
            }
            return BadRequest();
            
        }


        
        
    }
}
