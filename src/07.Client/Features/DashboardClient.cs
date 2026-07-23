using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Shared.DTOs.Dashboard;
using System.Net.Http.Json;

namespace SupportTicketSystem.Client.Clients;

public class DashboardClient : IDashboardClient
{
    private readonly HttpClient _httpClient;
    public DashboardClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<DashboardSummaryDto?> GetSummaryAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<DashboardSummaryDto>("api/dashboard/summary");

        if (result is null) throw new InvalidOperationException("Failed to retrieve dashboard summary from the API.");
        return result;
    }
}