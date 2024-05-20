using DAL.Repositories;
using Domain.Entity;
using Domain.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SlqServer");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("DAL"));
            });
            services.InitRepositories();
        }
        private static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepositories<Division>, BaseRepository<Division>>();
            services.AddScoped<IBaseRepositories<Employee>, BaseRepository<Employee>>();
        }
    }
}

