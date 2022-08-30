using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Infrastructure.Repositories;
using EmployeeManagementSystem.Infrastructure.Repositories.EntityFramework;
using EmployeeManagementSystem.Infrastructure.Services;

namespace EmployeeManagementSystemAPI.Extensions
{
    public static  class ServiceCollectionExtension
    {
        public static void RegisterSystemService(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public static void RegisterApplicationService(this IServiceCollection services)
        {
            // Repositories
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();

            services.AddTransient<IEmployeeService, EmployeeService>();

            
            services.AddTransient<IDepartmentService, DepartmentServices>();

            services.AddTransient<ILeaveRepository, LeaveRepository>();
            services.AddTransient<ILeavesService, LeaveService>();

            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRolesService, RoleService>();

        }

    }
}
