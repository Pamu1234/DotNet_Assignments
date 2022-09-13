using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.Infrastructure.Specs;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers.V1
{
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class LeaveBalancesController : ApiControllerBase
    {
        private readonly ILeaveBalanceService _leaveBalanceService;
        private readonly IMapper _mapper;
        private readonly ILogger<LeaveBalancesController> _logger;
        public LeaveBalancesController(ILeaveBalanceService leaveBalanceService, IMapper mapper, ILogger<LeaveBalancesController> logger)
        {
            _leaveBalanceService = leaveBalanceService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<LeaveBalance>> Post([FromBody] LeaveBalanceVm leaveBalanceVm)
        {
            _logger.LogInformation("Inserting data to LeaveBalance entity.");
            LeaveBalance leaveBalance = _mapper.Map<LeaveBalanceVm, LeaveBalance>(leaveBalanceVm);
            return Ok(await _leaveBalanceService.CreateRangeAsync(leaveBalance));

        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<LeaveBalance>>> Get()
        {
            _logger.LogInformation("Getting list of all LeaveBalance entity.");
            var result = await _leaveBalanceService.GetLeavesBalanceAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<LeaveBalance>> Get(int id)
        {
            _logger.LogInformation("Getting list of  LeaveBalance by ID:{id},", id);
            var result = await _leaveBalanceService.GetLeaveBalanceDataByIdAsync(id);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("leaves/{empId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetEmployeesRemainingleaves(int empId)
        {
            var remainingLeaves = await _leaveBalanceService.GetRemainingLeavesByEmpId(empId);
            return Ok(remainingLeaves);
        }
        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]

        public async Task<ActionResult<IEnumerable<LeaveBalance>>> Put(int id, [FromBody] LeaveBalanceVm leaveBalanceVm)
        {
            LeaveBalance leaveBalance = _mapper.Map<LeaveBalanceVm, LeaveBalance>(leaveBalanceVm);
            if (id <= 0 || id != leaveBalance.LeaveTypeId)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's Id.");
                return BadRequest();
            }

            return Ok(await _leaveBalanceService.UpdateAsync(id, leaveBalance));
        }

        [HttpDelete("{id}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            await _leaveBalanceService.DeleteLeaveBalanceDataByIdAsync(id);
        }
    }
}
