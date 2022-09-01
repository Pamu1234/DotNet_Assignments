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
        public LeaveBalancesController(ILeaveBalanceService leaveBalanceService)
        {
            _leaveBalanceService = leaveBalanceService;
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
            var leaveBlance = new LeaveBalance
            {

                EmployeeId = leaveBalanceVm.EmployeeId,
                LeaveTypeId = leaveBalanceVm.LeaveTypeId,
                Balance = leaveBalanceVm.Balance,
            };
            var result = await _leaveBalanceService.CreateAsync(leaveBlance);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task <ActionResult<IEnumerable<LeaveBalance>>>  Put(int id, [FromBody] LeaveBalanceVm leaveBalanceVm)
        {
            var leaveBlance = new LeaveBalance
            {
                EmployeeId = leaveBalanceVm.EmployeeId,
                LeaveTypeId = leaveBalanceVm.LeaveTypeId,
                Balance = leaveBalanceVm.Balance,
            };
            var result = await _leaveBalanceService.UpdateAsync(id, leaveBlance);
            return Ok(result);
        }

        // DELETE api/<LeaveBalancesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _leaveBalanceService.DeleteLeaveBalanceDataByIdAsync(id);
        }
    }
}
