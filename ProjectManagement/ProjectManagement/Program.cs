using ProjectManagement.Infrastructure.Services;
using Serilog;


ProjectManagementDataService employeeDataService = new ProjectManagementDataService();
ServiceValidations serviceValidations = new ServiceValidations();

// Logger

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/ProjectManagement.txt",rollingInterval:RollingInterval.Day)
    .CreateLogger();
Log.Information("Program execution is started");

serviceValidations.MainMenu();

Log.CloseAndFlush();
