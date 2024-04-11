using Microsoft.EntityFrameworkCore;
using Domain.Entity;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace DAL
{
    internal class ApplicationDbCotext : DbContext
    {
        public ApplicationDbCotext(DbContextOptions<ApplicationDbCotext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
