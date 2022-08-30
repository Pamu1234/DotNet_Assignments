using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Infrastructure.Models;
using EmployeeManagementSystem.Infrastructure.Repositories;
using EmployeeManagementSystem.Infrastructure.Repositories.EntityFramework;
using EmployeeManagementSystem.Infrastructure.Services;
using EmployeeManagementSystemAPI.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterSystemService();
builder.Services.RegisterApplicationService();

IConfiguration configuration = builder.Configuration;
builder.Services.AddDbContext<EmployeeManagementDataDbContext>(data => 
{
    data.UseSqlServer(configuration.GetConnectionString("EmployeeDbContext"));
    
});


var app = builder.Build();

app.CreateMiddlewarePipeline();

app.Run();
