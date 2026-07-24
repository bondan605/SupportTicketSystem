using Microsoft.Extensions.DependencyInjection;
using SupportTicketSystem.Client.Clients;
using SupportTicketSystem.Client.Features;
using SupportTicketSystem.Client.Features.Interfaces;

namespace SupportTicketSystem.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddClientServices(this IServiceCollection services, string apiBaseUrl)
    {
        services.AddHttpClient<IAuthClient, AuthClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });

        services.AddHttpClient<ITicketClient, TicketClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });

        services.AddHttpClient<IDashboardClient, DashboardClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });

        services.AddHttpClient<IUserClient, UserClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });

        return services;
    }
}