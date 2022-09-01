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
        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
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
        public async Task<ActionResult<Employee>> Post([FromBody] DepartmentVm departmentVm)
        {
            var department = new Department { 
                DepartmentName = departmentVm.DepartmentName, Description = departmentVm.Description, CreatedBy = departmentVm.CreatedBy, CreatedDate = departmentVm.CreatedDate, UpdatedBy = departmentVm.UpdatedBy, UpdatedDate = departmentVm.UpdatedDate};
            var result = await _departmentService.CreateAsync(department);
            return Ok(result);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult<Department>> Put(int id, [FromBody] DepartmentVm departmentVm)
        {
            var department = new Department
            {
                DepartmentId = (int)departmentVm.DepartmentId,
                DepartmentName = departmentVm.DepartmentName,
                Description = departmentVm.Description,
                CreatedBy = departmentVm.CreatedBy,
                CreatedDate = departmentVm.CreatedDate,
                UpdatedBy = departmentVm.UpdatedBy,
                UpdatedDate = departmentVm.UpdatedDate
            };
            var result = await _departmentService.UpdateAsync(id, department);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task  Delete(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
        }
    }
}
