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
    public class LeaveApplicationsController : ControllerBase
    {
        private readonly ILeaveApplicationService _leaveApplicationService;
        private readonly IMapper _mapper;
        public LeaveApplicationsController(ILeaveApplicationService leaveApplicationService, IMapper mapper)
        {
            _leaveApplicationService = leaveApplicationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveApplication>>> Get()
        {
            var result = await _leaveApplicationService.GetLeaveApplicationAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<LeaveApplication>> Get(int id)
        {
            var result = await _leaveApplicationService.GetLeaveDataByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<LeaveApplication>> Post([FromBody] LeaveApplicationVm leaveApplicationVm)
        {
            LeaveApplication leaveApplication = _mapper.Map<LeaveApplicationVm, LeaveApplication>(leaveApplicationVm);
            return Ok(await _leaveApplicationService.CreateAsync(leaveApplication));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<LeaveApplication>>> Put(int id, [FromBody] LeaveApplicationVm leaveApplicationVm)
        {
            LeaveApplication leaveApplication = _mapper.Map<LeaveApplicationVm, LeaveApplication>(leaveApplicationVm);
            return Ok(await _leaveApplicationService.UpdateAsync(id, leaveApplication));
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _leaveApplicationService.DeleteLeaveApplicationAsync(id);
        }
    }
}
