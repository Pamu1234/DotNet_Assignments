using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
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
        [HttpPost]
        public async Task<ActionResult<Department>> Post([FromBody] DepartmentVm departmentVm)
        {
            _logger.LogInformation("Inserting data to department entity.");
            Department department = _mapper.Map<DepartmentVm, Department>(departmentVm);
            return Ok(await _departmentService.CreateAsync(department));

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> Get()
        {
            _logger.LogInformation("Getting list of all departments.");
            var result = await _departmentService.GetDepartmentsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Department>>>  Get(int id)
        {
            _logger.LogInformation("Getting list of  department by ID:{id},", id);
            var result = await _departmentService.GetDepartmentAsync(id);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult<Department>> Put(int id, [FromBody] DepartmentVm departmentVm)
        {
            if (id <= 0 || id != departmentVm.Id)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's Id.");
                return BadRequest();
            }

            Department department = _mapper.Map<DepartmentVm, Department>(departmentVm);
            return Ok(await _departmentService.UpdateAsync(id, department));
        }

        [HttpDelete("{id}")]
        public async Task  Delete(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
        }
    }
}
