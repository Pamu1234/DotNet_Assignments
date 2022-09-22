using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.Infrastructure.Specs;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers.V1
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class EmployeesController : ApiControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILeaveApplicationService _leaveApplicationService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentsController> _logger;
        public EmployeesController(IEmployeeService employeeService, ILeaveApplicationService leaveApplicationService, IMapper mapper, ILogger<DepartmentsController> logger)
        {
            _employeeService = employeeService;
            _leaveApplicationService = leaveApplicationService;
            _mapper = mapper;
            _logger = logger;
        }

        // Insert data
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<IEnumerable<Employee>>> Post([FromBody] EmployeeVm employeeVm)
        {
            _logger.LogInformation("Inserting data to Employee entity.");
            Employee employee = _mapper.Map<EmployeeVm, Employee>(employeeVm);
            var result = await _employeeService.CreateAsync(employee);
            return Ok(result);
        }

        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {
            _logger.LogInformation("Getting list of all Employee's.");
            var result = await _employeeService.GetEmployeesAsync();
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [Route("id")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int id)
        {
            _logger.LogInformation("Getting list of  employee by ID:{id},", id);
            var employee = await _employeeService.GetEmployeeAsync(id);
            return Ok(employee);
        }

        [Route("id")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<IEnumerable<Employee>>> Put(int id, [FromBody] EmployeeVm employeeVm)
        {
            Employee employee = _mapper.Map<EmployeeVm, Employee>(employeeVm);
            if (id <= 0 || id != employee.EmployeeId)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's Id.");
                return BadRequest();
            }
            var result = await _employeeService.UpdateAsync(id, employee);
            return Ok(result);
        }

        [Route("id")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            var emloyee = _employeeService.DeleteEmployeeAsync(id);
        }


        [HttpPost("clockIn")]
        public async Task<ActionResult> EmployeeClockin(int id)
        {
            var attendance =await _employeeService.EmployeeLogin(id);
             return Ok(attendance);
        }

        [HttpPost("clockout")]
        public async Task<ActionResult> EmployeeClockOut(int id)
        {
            
            var logout = await _employeeService.EmployeeLogout(id);
            return Ok(logout);
        }
        
    }
}
