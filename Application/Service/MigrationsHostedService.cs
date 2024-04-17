using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.Service
{
    public class MigrationsHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceProvider;
        public MigrationsHostedService(IServiceScopeFactory serviceProvider)
        {
            _serviceProvider = serviceProvider;
    
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();

        }
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}
