using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Respositories;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories;
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
    [Route("attendance")]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class AttendancesController : ApiControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ILeaveApplicationRepository _leaveApplicationRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentsController> _logger;

        public AttendancesController(IAttendanceService attendanceService, IEmployeeService employeeService, ILeaveApplicationRepository leaveApplicationRepository, IAttendanceRepository attendanceRepository, IMapper mapper, ILogger<DepartmentsController> logger)
        {
            _attendanceService = attendanceService;
            _employeeService = employeeService;
            _attendanceRepository = attendanceRepository;
            _leaveApplicationRepository = leaveApplicationRepository;
            _mapper = mapper;
            _logger = logger;
        }
         
        [MapToApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "Manager,Team Leader")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Attendance>> Post([FromBody] AttendanceVm attendanceVm)
        {
            _logger.LogInformation("Inserting data to Attendance entity.");
            var attendance = _mapper.Map<AttendanceVm, Attendance>(attendanceVm);
            var result = await _attendanceService.CreateAsync(attendance);
            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [Authorize(Roles = "Manager,Team Leader")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<AttendanceDto>> Get()
        {
            _logger.LogInformation("Getting list of all Attendances.");
            var result = await _attendanceService.GetAttendanceAsync();
            return Ok(result);
        }


        [MapToApiVersion("1.0")]
        [Route("id")]
        [Authorize(Roles = "Manager,Team Leader")]
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

        [Route("{empId}")]
        [HttpGet]
        [Authorize(Roles = "Software Developer,Web Developer,Manager,Team Leader,Developer,Admin")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> GetEmployeeAttendanceByEmployeeId(int empId)
        {
            if (empId < 0)
            {
                return BadRequest();
            }
            _logger.LogInformation("Gettig Employee Attendance by Employee Id: {empId}", empId);
            var employeeData = await _attendanceService.EmployeeAttendanceWithLeaves(empId);
            if (!employeeData.Any())
            {
                return BadRequest($"Employee is not found. EmpId: {empId}");
            }
            return Ok(employeeData);
        }


        [Route("clockin/{id}")]
        [HttpPost]
        [Authorize(Roles = "Software Developer,Web Developer,Manager,Team Leader,Developer,Admin")]
        public async Task<ActionResult> EmployeeClockin(int id)
        {
            var attendance = await _employeeService.EmployeeLogin(id);
            if(attendance == null)
            {
                return BadRequest("You are on leave or already login ");
            }
            var mappedClockIn = _mapper.Map<Attendance, EmployeeClockedInDto>(attendance);
            return Ok(mappedClockIn);
        }

        [Route("clockout/{id}")]
        [HttpPut]
        [Authorize(Roles = "Software Developer,Web Developer,Manager,Team Leader,Developer,Admin")]
        public async Task<ActionResult> EmployeeClockOut(int id)
        {
            var logout = await _employeeService.EmployeeLogout(id);
            var mappedClockOut = _mapper.Map<Attendance, EmployeeClockedOutDto>(logout);
            return Ok(mappedClockOut);
        }

        [Route("regularise")]
        [HttpPost]
        [Authorize(Roles = "Software Developer,Web Developer,Manager,Team Leader,Developer,Admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AttendanceRegularize([FromBody] AttendanceRegularizeVm attendanceRegularizeVm)
        {
            var attendance = await _attendanceService.GetAttendanceDataByDate(attendanceRegularizeVm.RegularizeDate, attendanceRegularizeVm.EmployeeId, attendanceRegularizeVm.TimeOut );
            if (attendance)
            {
                return Ok("Successfully Updated");
            }
            return BadRequest("Employee is not found or Login is not available");
        }

        [Route("addattendance")]
        [HttpPost]
        [Authorize(Roles = "Software Developer,Web Developer,Manager,Team Leader,Developer,Admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult>ForgotLoginAttendanceRegulize([FromBody] ForgotAttendanceRegularizeVm forgotAttendanceRegularizeVm)
        {
            Attendance mapAttendance = _mapper.Map<ForgotAttendanceRegularizeVm, Attendance>(forgotAttendanceRegularizeVm);
            var attendance = await _attendanceService.RegularizeForgotMethod(mapAttendance);
            return Ok(attendance);
        }

      

    }
}
