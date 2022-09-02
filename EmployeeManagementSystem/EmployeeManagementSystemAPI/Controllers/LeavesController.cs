using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeavesController : ControllerBase
    {
        private readonly ILeavesService _leavesService;
        private readonly IMapper _mapper;
        public LeavesController(ILeavesService leavesService, IMapper mapper)
        {
            _leavesService = leavesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leave>>> Get()
        {
            var result = await _leavesService.GetLeavesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>>  Get(int id)
        {

            return Ok(await _leavesService.GetLeaveDataAsync(id));
        }

        // Insert data
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Role>>> Post([FromBody] LeaveVm leaveVm)
        {
            Leave leave = _mapper.Map<LeaveVm, Leave>(leaveVm);
            return Ok(await _leavesService.CreateAsync(leave));

        }

        // Update Data
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Leave>>>  Put(int id, [FromBody] LeaveVm leaveVm )
        {
            Leave leave = _mapper.Map<LeaveVm, Leave>(leaveVm);

            return Ok( await _leavesService.UpdateAsync(id, leave));
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _leavesService.DeleteLeaveAsync(id);
        }
    }
}
