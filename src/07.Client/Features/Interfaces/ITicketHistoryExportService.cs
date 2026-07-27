using SupportTicketSystem.Shared.DTOs.Tickets;

namespace SupportTicketSystem.Client.Features.Interfaces
{
    public interface ITicketHistoryExportService
    {
        Task ExportToCsvAsync(IEnumerable<TicketHistoryDto> histories);
        Task ExportToPdfAsync(IEnumerable<TicketHistoryDto> histories);
    }
}