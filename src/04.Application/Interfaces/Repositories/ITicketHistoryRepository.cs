using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Application.Interfaces.Repositories
{
    public interface ITicketHistoryRepository
    {
        Task<PagedResult<TicketHistory>> GetFilteredHistoriesAsync(Guid? ticketId, string? action, Guid? changedBy, string? searchString, DateTime? startDate,
    DateTime? endDate, PagedRequest request);
        Task AddAsync(TicketHistory history);
    }
}