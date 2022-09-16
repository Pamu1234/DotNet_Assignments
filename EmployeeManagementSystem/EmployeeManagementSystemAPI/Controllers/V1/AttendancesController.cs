using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Attendance>>> Post([FromBody] AttendanceVm attendanceVm)
        {
            _logger.LogInformation("Inserting data to Attendance entity.");
            Attendance attendance = _mapper.Map<AttendanceVm, Attendance>(attendanceVm);
            var result = await _attendanceService.CreateAsync(attendance);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> Get()
        {
            _logger.LogInformation("Getting list of all Attendances.");
            var result = await _attendanceService.GetAttendanceAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int attendanceId)
        {
            _logger.LogInformation("Getting Data of Attendances by ID:{attendanceId},", attendanceId);
            var result = await _attendanceService.GetAttendanceDataByIdAsync(attendanceId);
            return Ok(result);

        }

        // PUT api/<AttendancesController>/5
        [HttpPut("{id}")]
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

        // DELETE api/<AttendancesController>/5
        [HttpDelete("{id}")]
        public Task Delete(int attendanceId)
        {
            return _attendanceService.DeleteAttendanceRecord(attendanceId);

        }
    }
}
