using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Client.Features.Interfaces
{
    public interface ITicketHistoryClient
    {
        Task<PagedResult<TicketHistoryDto>> GetFilteredHistoriesAsync(Guid? ticketId, string? action, Guid? changedBy, string? search, DateTime? startDate, DateTime? endDate, PagedRequest request);
    }
}