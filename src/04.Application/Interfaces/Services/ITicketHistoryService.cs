using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Application.Abstractions.Services
{
    public interface ITicketHistoryService
    {
        Task<PagedResult<TicketHistoryDto>> GetFilteredHistoriesAsync(
            Guid? ticketId,
            string? action,
            Guid? changedBy,
            string? search,
            DateTime? startDate,
            DateTime? endDate,
            PagedRequest request,
            Guid? scopedToUserId = null);

        Task<IEnumerable<TicketHistoryDto>> GetAllForExportAsync(
            Guid? ticketId,
            string? action,
            Guid? changedBy,
            string? search,
            DateTime? startDate,
            DateTime? endDate,
            Guid? scopedToUserId = null);
    }
}