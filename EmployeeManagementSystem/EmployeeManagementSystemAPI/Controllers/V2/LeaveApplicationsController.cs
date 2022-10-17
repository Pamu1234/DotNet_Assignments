using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class LeaveApplicationsController : ApiControllerBase
    {
        private readonly ILeaveApplicationService _leaveApplicationService;
        private readonly IMapper _mapper;
        private readonly ILogger<LeaveApplicationsController> _logger;

        public LeaveApplicationsController(ILeaveApplicationService leaveApplicationService,IMapper mapper, ILogger<LeaveApplicationsController> logger)
        {
            _leaveApplicationService = leaveApplicationService;
            _mapper = mapper;
            _logger = logger;
        }

        [MapToApiVersion("2.0")]
        [Route("leaves/{empId}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IEnumerable<TotalLeavesOfEmployeeDto>> GetEmployeeLeavesData(int empId)
        {
            return await _leaveApplicationService.GetEmployeeLeavesData(empId);
        }
    }
}
