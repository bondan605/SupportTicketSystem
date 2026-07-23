using SupportTicketSystem.Shared.Dtos.Reports;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Application.Interfaces;

public interface IReportService
{
    Task<PagedResult<ManagerReportItemDto>> GetManagerReportAsync(ManagerReportFilterDto filter);
    Task<DashboardSummaryDto> GetDashboardSummaryAsync();
}