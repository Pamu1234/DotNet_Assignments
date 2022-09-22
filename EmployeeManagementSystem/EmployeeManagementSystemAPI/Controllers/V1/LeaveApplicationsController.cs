using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.Infrastructure.Specs;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiConventionType(typeof(DefaultApiConventions))]
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

        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<LeaveApplication>> Post([FromBody] LeaveApplicationVm leaveApplicationVm)
        {
            _logger.LogInformation("Inserting data to leaveApplication entity.");
            LeaveApplication leaveApplication = _mapper.Map<LeaveApplicationVm, LeaveApplication>(leaveApplicationVm);
            return Ok(await _leaveApplicationService.CreateAsync(leaveApplication));
        }

        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<LeaveApplication>>> Get()
        {
            _logger.LogInformation("Getting list of all leaveApplication entity.");
            var result = await _leaveApplicationService.GetLeaveApplicationAsync();
            return Ok(result);
        }

        [Route("id")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<LeaveApplication>> Get(int id)
        {
            _logger.LogInformation("Getting list of  leaveApplication by ID:{id},", id);
            var result = await _leaveApplicationService.GetLeaveDataByIdAsync(id);
            if (result is null)
                return NotFound();
            return Ok(result);

        }

        [HttpGet("empId")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<LeaveApplication>>> GetEmployeeLeaveApplicationRequest(int empId)
        {
            var leaveApplicationRequest = await _leaveApplicationService.GetEmployeeLeaveRequest(empId);
            return Ok(leaveApplicationRequest);
        }

        [HttpGet("EmployeeLeaves")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IEnumerable<TotalLeavesOfEmployeeDto>> GetEmployeeLeavesData(int empId)
        {
            return await _leaveApplicationService.GetEmployeeLeavesData(empId);
        }
        [Route("id")]
        [HttpDelete()]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            await _leaveApplicationService.DeleteLeaveApplicationAsync(id);
        }


        [Route("id")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<IEnumerable<LeaveApplication>>> Put(int id, [FromBody] UpdateLeaveApplicationRequestDto leaveApplicationApprove)
        {
            return Ok(await _leaveApplicationService.UpdateAsync(id, leaveApplicationApprove));
        }

    }
}
