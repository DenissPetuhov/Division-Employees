using Application.Mapping;
using Application.Service;
using Domain.Interface.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {

            services.InitServices();
            

        }
        public static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDivisionService, DivisionService>();
            services.AddHostedService<MigrationsHostedService>();
            services.AddAutoMapper(typeof(DivisionMapping));
            services.AddAutoMapper(typeof(EmployeeMapping));

        }

    }
}

