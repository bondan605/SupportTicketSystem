using Microsoft.Extensions.DependencyInjection;
using SupportTicketSystem.Client.Features;

namespace SupportTicketSystem.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddClientServices(this IServiceCollection services, string apiBaseUrl)
    {
        services.AddHttpClient<TicketClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });

        return services;
    }
}