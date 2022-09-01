using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _roleService;
        public RolesController(IRolesService roleService)
        {
            _roleService = roleService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>>  Get()
        {
         
            return Ok(await _roleService.GetRolesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>>  Get(int id)
        {
            return Ok(await _roleService.GetRoleDataAsync(id));
        }

        // Insert Data
        [HttpPost]
        public async Task <ActionResult<IEnumerable<Role>>> Post([FromBody] RoleVm roleVm)
        {
            var role = new Role
            {
                RoleName = roleVm.RoleName,
                CreatedBy = roleVm.CreatedBy,
                CreatedDate = roleVm.CreatedDate,
                UpdatedBy = roleVm.UpdatedBy,
                UpdatedDate = roleVm.UpdatedDate
            };
            return Ok(await _roleService.CreateAsync(role));
        }

        // Update Data
        [HttpPut("{id}")]
        public async Task <ActionResult<Role>> Put(int id, [FromBody] RoleVm roleVm)
        {
            var role = new Role
            {
                RoleId = roleVm.RoleId,
                RoleName = roleVm.RoleName,
                CreatedBy = roleVm.CreatedBy,
                CreatedDate = roleVm.CreatedDate,
                UpdatedBy = roleVm.UpdatedBy,
                UpdatedDate = roleVm.UpdatedDate
            };
            return Ok(await _roleService.UpdateAsync(id, role));
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _roleService.DeleteRoleAsync(id);
        }
    }
}
