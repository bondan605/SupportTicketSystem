using SupportTicketSystem.Application.Interfaces;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Shared.DTOs.Dashboard;

namespace SupportTicketSystem.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _repository;
    public DashboardService(IDashboardRepository repository) => _repository = repository;

    public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
    {
        return await _repository.GetSummaryAsync();
    }
}