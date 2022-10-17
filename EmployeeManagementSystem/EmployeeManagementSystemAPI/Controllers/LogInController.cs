using EmployeeManagementSystem.Infrastructure.Repositories;
using EmployeeManagementSystemAPI.Extensions;
using EmployeeManagementSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public LogInController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        

    }
}
