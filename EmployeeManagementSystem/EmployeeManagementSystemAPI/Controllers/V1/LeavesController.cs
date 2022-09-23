using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.Infrastructure.Specs;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Route("leaves")]
    [ApiConventionType(typeof(DefaultApiConventions))]
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
        [MapToApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<IEnumerable<Role>>> Post([FromBody] LeaveVm leaveVm)
        {
            _logger.LogInformation("Inserting data to Role entity.");
            Leaves leave = _mapper.Map<LeaveVm, Leaves>(leaveVm);
            return Ok(await _leavesService.CreateAsync(leave));

        }

        [MapToApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<Leaves>>> Get()
        {
            _logger.LogInformation("Getting list of all Role entity.");
            var result = await _leavesService.GetLeavesAsync();
            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<Role>> Get(int id)
        {
            _logger.LogInformation("Getting list of  Role by ID:{id},", id);
            var result = await _leavesService.GetLeaveDataAsync(id);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        // Update Data
        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<IEnumerable<Leaves>>> Put(int id, [FromBody] LeaveVm leaveVm)
        {
            Leaves leave = _mapper.Map<LeaveVm, Leaves>(leaveVm);
            if (id <= 0 || id != leave.LeaveTypeId)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's Id.");
                return BadRequest();
            }

            return Ok(await _leavesService.UpdateAsync(id, leave));
        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            await _leavesService.DeleteLeaveAsync(id);
        }
    }
}
