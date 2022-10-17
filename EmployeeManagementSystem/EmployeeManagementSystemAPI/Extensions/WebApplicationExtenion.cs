using EmployeeManagementSystemAPI.Middleware;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;
using System.Text;

namespace EmployeeManagementSystemAPI.Extensions
{
    public static class WebApplicationExtenion
    {
        public static void CreateMiddlewarePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            }
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSerilogRequestLogging();
            app.MapControllers();

        }

        public static byte[] GetByteArray(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
        public static byte[] HexToByte(this string str)
        {
            var returnBytes = new byte[str.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}
