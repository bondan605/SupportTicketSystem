using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Dashboard;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SupportTicketSystem.Client.Clients;

public class DashboardClient : IDashboardClient
{
    private readonly HttpClient _httpClient;
    private readonly ITokenProvider _tokenProvider;

    public DashboardClient(HttpClient httpClient, ITokenProvider tokenProvider)
    {
        _httpClient = httpClient;
        _tokenProvider = tokenProvider;
    }

    public async Task<ApiResponse<DashboardSummaryDto>> GetSummaryAsync()
    {
        try
        {
            var token = await _tokenProvider.GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

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