using AutoMapper;
using EmployeeManagementSystem.Infrastructure.Data;
using EmployeeManagementSystemAPI.Configurations;
using EmployeeManagementSystemAPI.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Configure and Register Automapper
var config = new MapperConfiguration(config => config.AddProfile(new AutoMapperProfile()));
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton<IMapper>(mapper);
#endregion

IConfiguration configuration = builder.Configuration;
builder.Services.RegisterSystemService(configuration);
builder.Services.RegisterApplicationService();




var app = builder.Build();

app.CreateMiddlewarePipeline();

app.Run();
