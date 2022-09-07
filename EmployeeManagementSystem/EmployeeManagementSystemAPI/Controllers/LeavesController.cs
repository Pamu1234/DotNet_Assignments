using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers
{

    public class LeavesController : ApiControllerBase
    {
        private readonly ILeavesService _leavesService;
        private readonly IMapper _mapper;
        private readonly ILogger<LeavesController> _logger;
        public LeavesController(ILeavesService leavesService, IMapper mapper, ILogger<LeavesController> logger)
        {
            _leavesService = leavesService;
            _mapper = mapper;
            _logger = logger;
        }

        // Insert data
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Role>>> Post([FromBody] LeaveVm leaveVm)
        {
            _logger.LogInformation("Inserting data to Role entity.");
            Leave leave = _mapper.Map<LeaveVm, Leave>(leaveVm);
            return Ok(await _leavesService.CreateAsync(leave));

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leave>>> Get()
        {
            _logger.LogInformation("Getting list of all Role entity.");
            var result = await _leavesService.GetLeavesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>>  Get(int id)
        {
            _logger.LogInformation("Getting list of  Role by ID:{id},", id);
            var result = await _leavesService.GetLeaveDataAsync(id);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        // Update Data
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Leave>>>  Put(int id, [FromBody] LeaveVm leaveVm )
        {
            Leave leave = _mapper.Map<LeaveVm, Leave>(leaveVm);
            if (id <= 0 || id != leave.LeaveTypeId)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's Id.");
                return BadRequest();
            }

            return Ok( await _leavesService.UpdateAsync(id, leave));
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _leavesService.DeleteLeaveAsync(id);
        }
    }
}
