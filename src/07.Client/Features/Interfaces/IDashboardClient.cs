using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Dashboard;

namespace SupportTicketSystem.Client.Features.Interfaces;

public interface IDashboardClient
{
    Task<ApiResponse<DashboardSummaryDto>> GetSummaryAsync();
}
