using Microsoft.Extensions.DependencyInjection;
using SupportTicketSystem.Client.Clients;
using SupportTicketSystem.Client.Features;
using SupportTicketSystem.Client.Features.Interfaces;

namespace SupportTicketSystem.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddClientServices(this IServiceCollection services, string apiBaseUrl)
    {
        services.AddTransient<JwtForwardingHandler>();
        services.AddTransient<SessionExpiryHandler>();
        services.AddScoped<ISessionExpiryNotifier, SessionExpiryNotifier>();

        // Login has no token yet, so it only needs the JWT handler (which is a no-op until
        // a token exists) - not the 401-driven session-expiry check.
        services.AddHttpClient<IAuthClient, AuthClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        }).AddHttpMessageHandler<JwtForwardingHandler>();

        services.AddHttpClient<ITicketClient, TicketClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        }).AddHttpMessageHandler<JwtForwardingHandler>()
          .AddHttpMessageHandler<SessionExpiryHandler>();

        services.AddHttpClient<ITicketHistoryClient, TicketHistoryClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        }).AddHttpMessageHandler<JwtForwardingHandler>()
          .AddHttpMessageHandler<SessionExpiryHandler>();

        services.AddHttpClient<IDashboardClient, DashboardClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        }).AddHttpMessageHandler<JwtForwardingHandler>()
          .AddHttpMessageHandler<SessionExpiryHandler>();

        services.AddHttpClient<IUserClient, UserClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        }).AddHttpMessageHandler<JwtForwardingHandler>()
          .AddHttpMessageHandler<SessionExpiryHandler>();

        services.AddHttpClient<IReportClient, ReportClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        }).AddHttpMessageHandler<JwtForwardingHandler>()
          .AddHttpMessageHandler<SessionExpiryHandler>();

        services.AddHttpClient<ITicketHistoryExportService, TicketHistoryExportService>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });

        return services;
    }
}