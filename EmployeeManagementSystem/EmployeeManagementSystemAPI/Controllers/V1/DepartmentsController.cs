using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories.EntityFramework;
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
    [Route("department")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class DepartmentsController : ApiControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentsController> _logger;
        public DepartmentsController(IDepartmentService departmentService,IDepartmentRepository departmentRepository, IMapper mapper, ILogger<DepartmentsController> logger)
        {
            _departmentService = departmentService;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // Insert 
        [MapToApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "Manager,Team Leader")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Department>> Post([FromBody] DepartmentVm departmentVm)
        {
            _logger.LogInformation("Inserting data to department entity.");
            var department = _mapper.Map<DepartmentVm, Department>(departmentVm);
            return Ok(await _departmentService.CreateAsync(department));

        }

        [MapToApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [Authorize(Roles = "Software Developer,Web Developer,Manager,Team Leader,Developer,Admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<DepartmentDto>> Get()
        {
            _logger.LogInformation("Getting list of all departments.");
            var result = await _departmentService.GetDepartmentsAsync();

            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpGet]
        [Authorize(Roles = "Software Developer,Web Developer,Manager,Team Leader,Developer,Admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int id)
        {
            _logger.LogInformation("Getting list of  department by ID:{id},", id);
            var result = await _departmentService.GetDepartmentAsync(id);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        // Update
        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpPut]
        [Authorize(Roles = "HR")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<Department>> Put(int id, [FromBody] DepartmentToBeUpdatedDto departmentToBeUpdatedDto)
        {
            if(id<=0)
            {
                return BadRequest("Id field can't be <= zero OR it doesn't match with model's Id.");
            }
            var departmentData = await _departmentService.UpdateAsync(id, departmentToBeUpdatedDto);
            return Ok(departmentData);

        }


        [Route("employeelist/{deptId}")]
        [HttpGet]
        [Authorize(Roles = "Software Developer,Web Developer,Manager,Team Leader,Developer,Admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<Employee>> GetListOfEmployeesWorkingInSpecificDepartment(int deptId)
        {
            var result = await _departmentService.GetEmpWorkingInDept(deptId);
            if(!result.Any())
            {
                return Ok("Department is not available");
            }
            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpDelete]
        [Authorize(Roles = "Software Developer,Web Developer,Manager,Team Leader,Developer,Admin")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            var existingDepartment = await _departmentRepository.GetDepartmentById(id);
            if(existingDepartment==null)
            {
                 NotFound("Department is not available");
            }
            if(id<=0)
            {
                 BadRequest("Id can't be zero or <=o");
            }
            await _departmentService.DeleteDepartmentAsync(id);
        }
    }
}
