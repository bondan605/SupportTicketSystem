using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupportTicketSystem.Infrastructure.Persistence;

namespace SupportTicketSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            // This is also the perfect place to register our Repositories later
            // services.AddScoped<ITicketRepository, TicketRepository>();

            return services;
        }
    }
}