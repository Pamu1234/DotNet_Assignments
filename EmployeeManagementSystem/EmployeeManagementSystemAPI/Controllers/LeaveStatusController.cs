using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class LeaveStatusController : ApiControllerBase
    {
        private readonly ILeaveStatusService _leaveStatusService;
        private readonly IMapper _mapper;
        private readonly ILogger<LeaveStatusController> _logger;

        public LeaveStatusController(ILeaveStatusService leaveStatusService, IMapper mapper, ILogger<LeaveStatusController> logger)
        {
            _leaveStatusService = leaveStatusService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<IEnumerable<LeaveStatus>>> Post([FromBody] LeaveStatusVm leaveStatusVm)
        {
            _logger.LogInformation("Inserting data to LeaveStatus entity.");
            LeaveStatus leaveStatus = _mapper.Map<LeaveStatusVm, LeaveStatus>(leaveStatusVm);
            return Ok(await _leaveStatusService.CreateAsync(leaveStatus));
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async  Task<ActionResult<IEnumerable<LeaveStatusDto>>> Get()
        {
            _logger.LogInformation("Getting list of all LeaveStatus.");
            return Ok(await _leaveStatusService.GetLeavesStatusAsync());
        }

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task <ActionResult<LeaveStatus>> Get(int id)
        {
            _logger.LogInformation("Getting list of  LeaveStatus by ID:{id},", id);

            var result = await _leaveStatusService.GetLeaveStatusDataByIdAsync(id);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        //[HttpPut("{id}")]
        //public async Task <ActionResult<LeaveStatus>> Put(int id, [FromBody] LeaveStatusVm leaveStatusVm)
        //{
        //    LeaveStatus leaveStatus = _mapper.Map<LeaveStatusVm, LeaveStatus>(leaveStatusVm);
        //    if (id <= 0 || id != leaveStatus.StatusId)
        //    {
        //        _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's Id.");
        //        return BadRequest();
        //    }

        //    return Ok(await _leaveStatusService.UpdateAsync(id, leaveStatus));
        //}

        //[HttpDelete("{id}")]
        //public async Task Delete(int id)
        //{
        //    await _leaveStatusService.DeleteLeaveAsync(id);
        //}
    }
}
