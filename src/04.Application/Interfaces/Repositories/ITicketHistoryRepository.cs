using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Shared.DTOs.TicketHistories;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Application.Interfaces.Repositories
{
    public interface ITicketHistoryRepository
    {
        Task AddAsync(TicketHistory history);
        Task<PagedResult<TicketHistoryDto>> GetAllAsync(PagedRequest request);
    }
}
