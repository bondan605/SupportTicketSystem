using SupportTicketSystem.Shared.DTOs.Dashboard;

namespace SupportTicketSystem.Application.Interfaces.Repositories
{
    public interface IDashboardRepository
    {
        Task<DashboardSummaryDto> GetSummaryAsync();
    }
}
