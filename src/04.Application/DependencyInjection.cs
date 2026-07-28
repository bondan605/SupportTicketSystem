using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Application.Interfaces;
using SupportTicketSystem.Application.Services;
using SupportTicketSystem.Application.Services.Reports;
using System.Reflection;

namespace SupportTicketSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketHistoryService, TicketHistoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IReportService, ReportService>();

            // Register AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}