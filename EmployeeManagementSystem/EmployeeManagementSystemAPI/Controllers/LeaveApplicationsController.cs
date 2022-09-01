using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeaveApplicationsController : ControllerBase
    {
        private readonly ILeaveApplicationService _leaveApplicationService;
        public LeaveApplicationsController(ILeaveApplicationService leaveApplicationService)
        {
            _leaveApplicationService = leaveApplicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveApplication>>> Get()
        {
            var result = await _leaveApplicationService.GetLeaveApplicationAsync();
            return Ok(result);
        }

        // GET api/<LeaveApplicationsController>/5
        [HttpGet("{id}")]
        public async Task <ActionResult<LeaveApplication>> Get(int id)
        {
            var result = await _leaveApplicationService.GetLeaveDataByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<LeaveApplication>> Post([FromBody] LeaveApplicationVm leaveApplicationVm)
        {
            var leaveApplication = new LeaveApplication
            {
                EmployeeId = leaveApplicationVm.EmployeeId,
                LeaveTypeId = leaveApplicationVm.LeaveTypeId,
                StartDate = leaveApplicationVm.StartDate,
                EndDatew=leaveApplicationVm.EndDatew,
                Purpose = leaveApplicationVm.Purpose,
                NoOfDays = leaveApplicationVm.NoOfDays,
                DateOfApplication = leaveApplicationVm.DateOfApplication,
                DateOfApproval = leaveApplicationVm.DateOfApproval,
                StatusId = leaveApplicationVm.StatusId,
                CreatedDate = leaveApplicationVm.CreatedDate,
                UpdatedDate = leaveApplicationVm.UpdatedDate
            };
            var result = await _leaveApplicationService.CreateAsync(leaveApplication);
            return Ok(result);
        }

        // PUT api/<LeaveApplicationsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<LeaveApplication>>> Put(int id, [FromBody] LeaveApplicationVm leaveApplicationVm)
        {
            var leaveApplication = new LeaveApplication
            {
                EmployeeId = leaveApplicationVm.EmployeeId,
                LeaveTypeId = leaveApplicationVm.LeaveTypeId,
                StartDate = leaveApplicationVm.StartDate,
                EndDatew = leaveApplicationVm.EndDatew,
                Purpose = leaveApplicationVm.Purpose,
                NoOfDays = leaveApplicationVm.NoOfDays,
                DateOfApplication = leaveApplicationVm.DateOfApplication,
                DateOfApproval = leaveApplicationVm.DateOfApproval,
                StatusId = leaveApplicationVm.StatusId,
                UpdatedDate = leaveApplicationVm.UpdatedDate
            };
            var result = await _leaveApplicationService.UpdateAsync(id,leaveApplication);
            return Ok(result);
        }

        // DELETE api/<LeaveApplicationsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _leaveApplicationService.DeleteLeaveApplicationAsync(id);
        }
    }
}
