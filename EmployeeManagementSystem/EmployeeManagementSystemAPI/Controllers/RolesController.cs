﻿using AutoMapper;
using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _roleService;
        private readonly IMapper _mapper;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRolesService roleService, IMapper mapper, ILogger<RolesController> logger)
        {
            _roleService = roleService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Role>>> Post([FromBody] RoleVm roleVm)
        {
            _logger.LogInformation("Inserting data to Role entity.");
            Role role = _mapper.Map<RoleVm, Role>(roleVm);
            return Ok(await _roleService.CreateAsync(role));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>>  Get()
        {

            _logger.LogInformation("Getting list of all Role entity.");
            return Ok(await _roleService.GetRolesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>>  Get(int id)
        {
            _logger.LogInformation("Getting list of  Role by ID:{id},", id);
            return Ok(await _roleService.GetRoleDataAsync(id));
        }

        [HttpPut("{id}")]
        public async Task <ActionResult<Role>> Put(int id, [FromBody] RoleVm roleVm)
        {
            if (id <= 0 || id != roleVm.RoleId)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's Id.");
                return BadRequest();
            }
            Role role = _mapper.Map<RoleVm, Role>(roleVm);
            return Ok(await _roleService.UpdateAsync(id, role));
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _roleService.DeleteRoleAsync(id);
        }
    }
}
