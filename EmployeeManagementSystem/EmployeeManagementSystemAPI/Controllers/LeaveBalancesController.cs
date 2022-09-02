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
    public class LeaveBalancesController : ControllerBase
    {
        private readonly ILeaveBalanceService _leaveBalanceService;
        private readonly IMapper _mapper;
        public LeaveBalancesController(ILeaveBalanceService leaveBalanceService, IMapper mapper)
        {
            _leaveBalanceService = leaveBalanceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveBalance>>>  Get()
        {
            var result = await _leaveBalanceService.GetLeavesBalanceAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveBalance>> Get(int id)
        {
            var result = await _leaveBalanceService.GetLeaveBalanceDataByIdAsync(id);
            return Ok(result);
        }

        // POST api/<LeaveBalancesController>
        [HttpPost]
        public async Task<ActionResult<LeaveBalance>> Post([FromBody] LeaveBalanceVm leaveBalanceVm)
        {
            LeaveBalance leaveBalance = _mapper.Map<LeaveBalanceVm, LeaveBalance>(leaveBalanceVm);
           return Ok( await _leaveBalanceService.CreateAsync(leaveBalance));

        }

        [HttpPut("{id}")]
        public async Task <ActionResult<IEnumerable<LeaveBalance>>>  Put(int id, [FromBody] LeaveBalanceVm leaveBalanceVm)
        {
            LeaveBalance leaveBalance = _mapper.Map<LeaveBalanceVm, LeaveBalance>(leaveBalanceVm);
            return Ok(await _leaveBalanceService.UpdateAsync(id, leaveBalance));           
        }

        // DELETE api/<LeaveBalancesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _leaveBalanceService.DeleteLeaveBalanceDataByIdAsync(id);
        }
    }
}
