using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Service
{
    public class MigrationsHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceProvider;
        private readonly ILogger<MigrationsHostedService> _logger;

        public MigrationsHostedService(IServiceScopeFactory serviceProvider, ILogger<MigrationsHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
            using var scpe = _serviceProvider.CreateScope();
            var context = scpe.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _logger.LogInformation(message: "Start migration={@Migrations}",
                context.Database.GetPendingMigrations().ToList());

            await context.Database.MigrateAsync();

            _logger.LogInformation(message: "End migration={@Migrations}",
                context.Database.GetPendingMigrations().ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}
