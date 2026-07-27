using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Application.Interfaces;
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
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<ITicketHistoryService, TicketHistoryService>();

            // Register AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}