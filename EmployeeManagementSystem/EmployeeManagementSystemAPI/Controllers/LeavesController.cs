using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeavesController : ControllerBase
    {
        private readonly ILeavesService _leavesService;
        public LeavesController(ILeavesService leavesService)
        {
            _leavesService = leavesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leave>>> Get()
        {
            var result = await _leavesService.GetLeavesAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Role>>  Get(int id)
        {

            return Ok(await _leavesService.GetLeaveDataAsync(id));
        }

        // Insert data
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Role>>> Post([FromBody] LeaveVm leaveVm)
        {
            var leave = new Leave
            {
                LeaveTypeName = leaveVm.LeaveTypeName,
                Description = leaveVm.Description,
                CreatedBy = leaveVm.CreatedBy,
                CreatedDate = leaveVm.CreatedDate,
                UpdatedBy = leaveVm.UpdatedBy,
                UpdatedDate = leaveVm.UpdatedDate
                //RoleName = roleVm.RoleName,
                //CreatedBy = roleVm.CreatedBy,
                //CreatedDate = roleVm.CreatedDate,
                //UpdatedBy = roleVm.UpdatedBy,
                //UpdatedDate = roleVm.UpdatedDate,
            };
            var result = await _leavesService.CreateAsync(leave);
            return Ok(result);
        }

        // Update Data
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Leave>>>  Put(int id, [FromBody] LeaveVm leaveVm )
        {
            var leave = new Leave
            {
                LeaveTypeId = leaveVm.LeaveTypeId,
                LeaveTypeName = leaveVm.LeaveTypeName,
                Description = leaveVm.Description,
                CreatedBy = leaveVm.CreatedBy,
                CreatedDate = leaveVm.CreatedDate,
                UpdatedBy = leaveVm.UpdatedBy,
                UpdatedDate = leaveVm.UpdatedDate

            };
            var result = await _leavesService.UpdateAsync(id, leave);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _leavesService.DeleteLeaveAsync(id);
        }
    }
}
