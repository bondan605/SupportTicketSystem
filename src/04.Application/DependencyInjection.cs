using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Application.Services;
using System.Reflection;

namespace SupportTicketSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register Services
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            // Register AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}