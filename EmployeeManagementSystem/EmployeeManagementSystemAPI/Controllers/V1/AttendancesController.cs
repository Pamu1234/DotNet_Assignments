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
    [Route("attendance")]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class AttendancesController : ApiControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentsController> _logger;

        public AttendancesController(IAttendanceService attendanceService, IMapper mapper, ILogger<DepartmentsController> logger)
        {
            _attendanceService = attendanceService;
            _mapper = mapper;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<IEnumerable<Attendance>>> Post([FromBody] AttendanceVm attendanceVm)
        {
            _logger.LogInformation("Inserting data to Attendance entity.");
            var attendance = _mapper.Map<AttendanceVm, Attendance>(attendanceVm);
            var result = await _attendanceService.CreateAsync(attendance);
            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> Get()
        {
            _logger.LogInformation("Getting list of all Attendances.");
            var result = await _attendanceService.GetAttendanceAsync();
            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int attendanceId)
        {
            _logger.LogInformation("Getting Data of Attendances by ID:{attendanceId},", attendanceId);
            var result = await _attendanceService.GetAttendanceDataByIdAsync(attendanceId);
            return Ok(result);

        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<Attendance>> Put(int attendanceId, [FromBody] AttendanceVm attendanceVm)
        {
            Attendance attendance = _mapper.Map<AttendanceVm, Attendance>(attendanceVm);
            if (attendanceId <= 0 || attendanceId != attendance.AttendanceId)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(attendanceId)), "Id field can't be <= zero OR it doesn't match with model's Id.");
                return BadRequest();
            }
            return Ok(await _attendanceService.UpdateAsync(attendanceId, attendance));
        }

        [MapToApiVersion("1.0")]
        [Route("id")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int attendanceId)
        {
            await  _attendanceService.DeleteAttendanceRecord(attendanceId);

        }

        [Route("getemployeeattendanceby/{empId}")]
        [HttpGet]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult>GetEmployeeAttendanceByEmployeeId(int empId)
        {
            _logger.LogInformation("Gettig Employee Attendance by Employee Id: {empId}", empId);
            var result = await _attendanceService.GetEmployeeAttendanceById(empId);
            return Ok(result);
        }

    }
}
