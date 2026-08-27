using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Application.Interfaces.Repositories
{
    public interface ITicketHistoryRepository
    {
        Task<PagedResult<TicketHistory>> GetFilteredHistoriesAsync(Guid? ticketId, string? action, Guid? changedBy, string? searchString, DateTime? startDate,
    DateTime? endDate, PagedRequest request, Guid? scopedToUserId = null);
        Task AddAsync(TicketHistory history);
        /// <summary>
        /// Mengambil seluruh data riwayat berdasarkan filter tanpa batasan paginasi (untuk ekspor).
        /// </summary>
        Task<IEnumerable<TicketHistory>> GetAllForExportAsync(
            Guid? ticketId,
            string? action,
            Guid? changedBy,
            string? searchString,
            DateTime? startDate,
            DateTime? endDate,
            Guid? scopedToUserId = null);
    }
}