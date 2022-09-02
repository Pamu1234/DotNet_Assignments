using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveStatusController : ControllerBase
    {
        private readonly ILeaveStatusService _leaveStatusService;
        private readonly IMapper _mapper;
        public LeaveStatusController(ILeaveStatusService leaveStatusService, IMapper mapper)
        {
            _leaveStatusService = leaveStatusService;
            _mapper = mapper;
        }


        [HttpGet]
        public async  Task<ActionResult<IEnumerable<LeaveStatus>>> Get()
        {
            return Ok(await _leaveStatusService.GetLeavesStatusAsync());
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<LeaveStatus>> Get(int id)
        {
            return Ok(await _leaveStatusService.GetLeaveStatusDataByIdAsync(id));
        }

        // POST api/<LeaveStatusController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<LeaveStatus>>> Post([FromBody] LeaveStatusVm leaveStatusVm)
        {
            LeaveStatus leaveStatus = _mapper.Map<LeaveStatusVm, LeaveStatus>(leaveStatusVm);
            return Ok(await _leaveStatusService.CreateAsync(leaveStatus));
        }

        [HttpPut("{id}")]
        public async Task <ActionResult<LeaveStatus>> Put(int id, [FromBody] LeaveStatusVm leaveStatusVm)
        {
            LeaveStatus leaveStatus = _mapper.Map<LeaveStatusVm, LeaveStatus>(leaveStatusVm);

            return Ok(await _leaveStatusService.UpdateAsync(id, leaveStatus));
        }

        // DELETE api/<LeaveStatusController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _leaveStatusService.DeleteLeaveAsync(id);
        }
    }
}
