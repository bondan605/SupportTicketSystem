using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SupportTicketSystem.Client.Features
{
    public class TicketHistoryClient : ITicketHistoryClient
    {
        private readonly HttpClient _httpClient;

        public TicketHistoryClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagedResult<TicketHistoryDto>> GetFilteredHistoriesAsync(
        Guid? ticketId,
        string? action,
        Guid? changedBy,
        string? search,
        DateTime? startDate,
        DateTime? endDate,
        PagedRequest request)
        {
            var url = $"/api/ticket-histories?pageNumber={request.PageNumber}&pageSize={request.PageSize}";

            if (ticketId.HasValue && ticketId.Value != Guid.Empty)
                url += $"&ticketId={ticketId.Value}";

            if (!string.IsNullOrEmpty(action))
                url += $"&action={Uri.EscapeDataString(action)}";

            if (changedBy.HasValue && changedBy.Value != Guid.Empty)
                url += $"&changedBy={changedBy.Value}";

            if (!string.IsNullOrWhiteSpace(search))
                url += $"&search={Uri.EscapeDataString(search)}";

            if (startDate.HasValue)
                url += $"&startDate={startDate.Value:yyyy-MM-dd}";

            if (endDate.HasValue)
                url += $"&endDate={endDate.Value:yyyy-MM-dd}";

            var response = await _httpClient.GetFromJsonAsync<ApiResponse<PagedResult<TicketHistoryDto>>>(url);

            return response?.Data ?? new PagedResult<TicketHistoryDto>();
        }
    }
}