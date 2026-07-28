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

        public Task<(byte[] Content, string FileName)> ExportCsvAsync(Guid? ticketId, string? action, Guid? changedBy, string? search, DateTime? startDate, DateTime? endDate)
            => DownloadExportAsync("export-csv", "csv", ticketId, action, changedBy, search, startDate, endDate);

        public Task<(byte[] Content, string FileName)> ExportPdfAsync(Guid? ticketId, string? action, Guid? changedBy, string? search, DateTime? startDate, DateTime? endDate)
            => DownloadExportAsync("export-pdf", "pdf", ticketId, action, changedBy, search, startDate, endDate);

        private async Task<(byte[] Content, string FileName)> DownloadExportAsync(
            string segment,
            string extension,
            Guid? ticketId,
            string? action,
            Guid? changedBy,
            string? search,
            DateTime? startDate,
            DateTime? endDate)
        {
            var queryParts = new List<string>();

            if (ticketId.HasValue && ticketId.Value != Guid.Empty)
                queryParts.Add($"ticketId={ticketId.Value}");

            if (!string.IsNullOrEmpty(action))
                queryParts.Add($"action={Uri.EscapeDataString(action)}");

            if (changedBy.HasValue && changedBy.Value != Guid.Empty)
                queryParts.Add($"changedBy={changedBy.Value}");

            if (!string.IsNullOrWhiteSpace(search))
                queryParts.Add($"search={Uri.EscapeDataString(search)}");

            if (startDate.HasValue)
                queryParts.Add($"startDate={startDate.Value:yyyy-MM-dd}");

            if (endDate.HasValue)
                queryParts.Add($"endDate={endDate.Value:yyyy-MM-dd}");

            var url = $"/api/ticket-histories/{segment}";
            if (queryParts.Count > 0)
                url += $"?{string.Join("&", queryParts)}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var bytes = await response.Content.ReadAsByteArrayAsync();
            var fileName = response.Content.Headers.ContentDisposition?.FileNameStar
                ?? response.Content.Headers.ContentDisposition?.FileName?.Trim('"')
                ?? $"TicketHistories_{DateTime.Now:yyyyMMdd_HHmmss}.{extension}";

            return (bytes, fileName);
        }
    }
}