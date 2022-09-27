using AutoMapper;
using EmployeeManagementSystemAPI.Configurations;
using EmployeeManagementSystemAPI.Extensions;
using EmployeeManagementSystemAPI.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Configure and Register Automapper
var config = new MapperConfiguration(config => config.AddProfile(new AutoMapperProfile()));
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton<IMapper>(mapper);
#endregion

Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)));

IConfiguration configuration = builder.Configuration;
builder.Services.RegisterSystemService();
builder.Services.RegisterApplicationService(configuration);


var app = builder.Build();

app.CreateMiddlewarePipeline();

app.Run();
