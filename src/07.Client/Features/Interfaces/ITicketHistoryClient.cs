using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Client.Features.Interfaces
{
    public interface ITicketHistoryClient
    {
        Task<PagedResult<TicketHistoryDto>> GetFilteredHistoriesAsync(Guid? ticketId, string? action, Guid? changedBy, string? search, DateTime? startDate, DateTime? endDate, PagedRequest request);

        /// <summary>Downloads the filtered history export as CSV bytes via the authenticated
        /// HttpClient - unlike a raw browser redirect, this correctly carries the JWT.</summary>
        Task<(byte[] Content, string FileName)> ExportCsvAsync(Guid? ticketId, string? action, Guid? changedBy, string? search, DateTime? startDate, DateTime? endDate);

        /// <summary>Downloads the filtered history export as PDF bytes via the authenticated HttpClient.</summary>
        Task<(byte[] Content, string FileName)> ExportPdfAsync(Guid? ticketId, string? action, Guid? changedBy, string? search, DateTime? startDate, DateTime? endDate);
    }
}