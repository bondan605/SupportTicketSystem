using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Dashboard;
using System.Net.Http.Json;

namespace SupportTicketSystem.Client.Clients;

public class DashboardClient : IDashboardClient
{
    private readonly HttpClient _httpClient;

    public DashboardClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<DashboardSummaryDto>> GetSummaryAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<DashboardSummaryDto>>(ApiRoutes.Dashboard.Summary);

            return response ?? new ApiResponse<DashboardSummaryDto>
            {
                Success = false,
                Message = "No response received from the server."
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<DashboardSummaryDto>
            {
                Success = false,
                Message = $"Client Error: {ex.Message}"
            };
        }
    }
}