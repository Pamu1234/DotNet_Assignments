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
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class DepartmentsController : ApiControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentsController> _logger;
        public DepartmentsController(IDepartmentService departmentService, IMapper mapper, ILogger<DepartmentsController> logger)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _logger = logger;
        }

        // Insert 
        [MapToApiVersion("1.0")]
        [Route("")]
        [HttpPost]
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
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> Get()
        {
            _logger.LogInformation("Getting list of all departments.");
            var result = await _departmentService.GetDepartmentsAsync();

            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpGet]
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
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<Department>> Put(int id, [FromBody] DepartmentVm departmentVm)
        {
            Department department = _mapper.Map<DepartmentVm, Department>(departmentVm);
            if (id <= 0 || id != department.DepartmentId)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's Id.");
                return BadRequest();
            }

            return Ok(await _departmentService.UpdateAsync(id, department));
        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]


        //public async Task<IEnumerable<Department>> Get
        public async Task Delete(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
        }
    }
}
