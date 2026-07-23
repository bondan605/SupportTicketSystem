using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Dashboard;
using System.Net.Http.Json;

namespace SupportTicketSystem.Client.Features;

public class DashboardClient : IDashboardClient
{
    private readonly HttpClient _httpClient;
    private readonly bool _useDummy = true; // Set ke false jika API sudah siap

    public DashboardClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<DashboardSummaryDto>> GetSummaryAsync()
    {
        if (_useDummy)
        {
            await Task.Delay(500); // Simulasi latency jaringan
            return new ApiResponse<DashboardSummaryDto>
            {
                Success = true,
                Data = new DashboardSummaryDto
                {
                    TotalTickets = 150,
                    OpenTickets = 45,
                    InProgressTickets = 30,
                    ResolvedTickets = 50,
                    ClosedTickets = 25,
                    UnassignedTickets = 12,
                    WeeklyTrends = new List<TicketTrendDto>
                    {
                        new() { DayName = "Mon", Count = 10 },
                        new() { DayName = "Tue", Count = 25 },
                        new() { DayName = "Wed", Count = 15 },
                        new() { DayName = "Thu", Count = 30 },
                        new() { DayName = "Fri", Count = 20 }
                    }
                }
            };
        }

        // Implementasi asli ketika API siap
        return await _httpClient.GetFromJsonAsync<ApiResponse<DashboardSummaryDto>>(ApiRoutes.Dashboard.Summary)
               ?? new ApiResponse<DashboardSummaryDto> { Success = false };
    }
}