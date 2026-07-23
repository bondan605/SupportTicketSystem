using SupportTicketSystem.Shared.DTOs.Dashboard;

namespace SupportTicketSystem.Application.Interfaces;

public interface IDashboardService
{
    /// <summary>
    /// Mengambil ringkasan statistik tiket untuk tampilan dashboard manager.
    /// </summary>
    Task<DashboardSummaryDto> GetDashboardSummaryAsync();
}