using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Infrastructure.Data;
using EmployeeManagementSystem.Infrastructure.Repositories;
using EmployeeManagementSystem.Infrastructure.Repositories.EntityFramework;
using EmployeeManagementSystem.Infrastructure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagementSystemAPI.Extensions
{
    public static  class ServiceCollectionExtension
    {
        public static void RegisterSystemService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmployeeManagementDataDbContext>(data =>
            {
                data.UseSqlServer(configuration.GetConnectionString("EmployeeDbContext"));
            });

            // Dapper
            services.AddTransient<IDbConnection>(db => new SqlConnection(
                                configuration.GetConnectionString("EmployeeDbContext")));

            // Add services to the container.
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            
        }
        public static void RegisterApplicationService(this IServiceCollection services, IConfiguration configuration)
        {

            // Repositories
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<ILeaveApplicationRepository, LeaveApplicationRepository>();
            services.AddTransient<ILeaveBalanceRepository, LeaveBalanceRepository>();
            services.AddTransient<ILeaveRepository, LeaveRepository>();            
            services.AddTransient<ILeaveStatusRepository, LeaveStatusRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            // Services
            services.AddTransient<IDepartmentService, DepartmentServices>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ILeaveApplicationService, LeaveApplicationServices>();
            services.AddTransient<ILeaveBalanceService, LeaveBalanceServices>();
            services.AddTransient<ILeavesService, LeaveService>();
            services.AddTransient<ILeaveStatusService, LeaveStatusServices>();
            services.AddTransient<IRolesService, RoleService>();

            

        }



    }
}
