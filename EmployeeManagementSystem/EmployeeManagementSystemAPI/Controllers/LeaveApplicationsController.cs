using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers
{

    public class LeaveApplicationsController : ApiControllerBase
    {
        private readonly ILeaveApplicationService _leaveApplicationService;
        private readonly IMapper _mapper;
        private readonly ILogger<LeaveApplicationsController> _logger;

        public LeaveApplicationsController(ILeaveApplicationService leaveApplicationService, IMapper mapper, ILogger<LeaveApplicationsController> logger)
        {
            _leaveApplicationService = leaveApplicationService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<LeaveApplication>> Post([FromBody] LeaveApplicationVm leaveApplicationVm)
        {
            _logger.LogInformation("Inserting data to leaveApplication entity.");
            var leaveApplication = _mapper.Map<LeaveApplicationVm, LeaveApplication>(leaveApplicationVm);
            return Ok(await _leaveApplicationService.CreateAsync(leaveApplication));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveApplication>>> Get()
        {
            _logger.LogInformation("Getting list of all leaveApplication entity.");
            var result = await _leaveApplicationService.GetLeaveApplicationAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<LeaveApplication>> Get(int id)
        {
            _logger.LogInformation("Getting list of  leaveApplication by ID:{id},", id);
            var result = await _leaveApplicationService.GetLeaveDataByIdAsync(id);
            if (result is null)
                return NotFound();
            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<LeaveApplication>>> Put(int id, [FromBody] LeaveApplicationVm leaveApplicationVm)
        {
            LeaveApplication leaveApplication = _mapper.Map<LeaveApplicationVm, LeaveApplication>(leaveApplicationVm);
            if (id <= 0 || id != leaveApplication.LeaveTypeId)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's Id.");
                return BadRequest();
            }
            return Ok(await _leaveApplicationService.UpdateAsync(id, leaveApplication));
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _leaveApplicationService.DeleteLeaveApplicationAsync(id);
        }
    }
}
