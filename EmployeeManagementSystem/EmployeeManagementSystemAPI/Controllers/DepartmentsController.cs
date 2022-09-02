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
        public DepartmentsController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> Get()
        {
            var result = await _departmentService.GetDepartmentsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>>  Get(int id)
        {
            var result = await _departmentService.GetDepartmentAsync(id);
            return Ok(result);
        }

        // Insert 
        [HttpPost]
        public async Task<ActionResult<Department>> Post([FromBody] DepartmentVm departmentVm)
        {
            Department department = _mapper.Map<DepartmentVm, Department>(departmentVm);
            return Ok( await _departmentService.CreateAsync(department));
            
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult<Department>> Put(int id, [FromBody] DepartmentVm departmentVm)
        {
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
