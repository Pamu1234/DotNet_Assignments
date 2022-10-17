using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AttendanceController : ApiControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly ILogger<AttendanceController> _logger;
        public AttendanceController(IAttendanceService attendanceService, ILogger<AttendanceController> logger)
        {
            _attendanceService = attendanceService;
            _logger = logger;
        }

    }
}
