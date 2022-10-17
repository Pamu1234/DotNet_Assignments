using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.Infrastructure.Specs;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Authorize]
    [Route("leaveapplication")]
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

        [MapToApiVersion("1.0")]
        [Route(""),AllowAnonymous]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<LeaveApplication>> Post([FromBody] LeaveApplicationVm leaveApplicationVm)
        {
            _logger.LogInformation("Inserting data to leaveApplication entity.");
            LeaveApplication leaveApplication = _mapper.Map<LeaveApplicationVm, LeaveApplication>(leaveApplicationVm);
            var createLeaveApplication = await _leaveApplicationService.CreateAsync(leaveApplication);
            var returnApplicationData = _mapper.Map<LeaveApplication, LeaveApplicationVm>(createLeaveApplication);
            return Ok(returnApplicationData);
        }

        [MapToApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<LeaveApplication>> Get()
        {
            _logger.LogInformation("Getting list of all leaveApplication entity.");
            var result = await _leaveApplicationService.GetLeaveApplicationAsync();
            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpGet]
        [Authorize(Roles = "Software Developer,Web Developer,Manager,Team Leader,Developer,Admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int id)
        {
            _logger.LogInformation("Getting Data of  leaveApplication by ID:{id},", id);
            var result = await _leaveApplicationService.GetLeaveApplicationAsync(id);
            if (result is null)
                return NotFound();
            return Ok(result);

        }

        [MapToApiVersion("1.0")]
        [Route("leavesrequest/{empId}"),AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<LeaveApplication>>> GetEmployeeLeaveApplicationRequest(int empId)
        {
            if(empId<=0)
            {
                return BadRequest("Id can't be zero or less than zero");
            }
            var leaveApplicationRequest = await _leaveApplicationService.GetLeaveApplicationAsync(EmployeeId:empId);
            if (!leaveApplicationRequest.Any())
            { 
                return NotFound("Employee is not available");
            }
                return Ok(leaveApplicationRequest); 
            
        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpDelete()]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            await _leaveApplicationService.DeleteLeaveApplicationAsync(id);
        }


        [Route("id")]
        [HttpPut]
        [Authorize(Roles = "Manager,Team Leader")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<LeaveApplication>> Put(int id, [FromBody] UpdateLeaveApplicationRequestDto leaveApplicationApprove)
        {
            return Ok(await _leaveApplicationService.UpdateAsync(id, leaveApplicationApprove));
        }

    }
}
