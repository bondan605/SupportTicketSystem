using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace SupportTicketSystem.Client.Features;

public class TicketClient : ITicketClient
{
    private readonly HttpClient _httpClient;

    public TicketClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<TicketDto>> GetTicketByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<ApiResponse<TicketDto>>($"{ApiRoutes.Tickets.Base}/{id}") ?? new ApiResponse<TicketDto> { Success = false };
    }

    public async Task<PagedResult<TicketDto>> GetAllTicketsAsync(PagedRequest request)
    {
        var url = $"{ApiRoutes.Tickets.Base}?pageNumber={request.PageNumber}&pageSize={request.PageSize}";
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<PagedResult<TicketDto>>>(url);
        return response?.Data ?? new PagedResult<TicketDto>();
    }

    public async Task<PagedResult<TicketDto>> GetFilteredTicketsAsync(string? status, Guid? assignedTo, PagedRequest request)
    {
        var url = $"{ApiRoutes.Tickets.Report}?pageNumber={request.PageNumber}&pageSize={request.PageSize}";

        if (!string.IsNullOrEmpty(status))
            url += $"&status={Uri.EscapeDataString(status)}";

        if (assignedTo.HasValue)
            url += $"&assignedTo={assignedTo.Value}";

        var response = await _httpClient.GetFromJsonAsync<ApiResponse<PagedResult<TicketDto>>>(url);
        return response?.Data ?? new PagedResult<TicketDto>();
    }

    public async Task<PagedResult<TicketDto>> GetTicketListAsync(string? status, Guid? assignedTo, PagedRequest request, string? priority, string? category, string? search)
    {
        var url = $"{ApiRoutes.Tickets.List}?pageNumber={request.PageNumber}&pageSize={request.PageSize}";

        if (!string.IsNullOrWhiteSpace(status))
            url += $"&status={Uri.EscapeDataString(status)}";

        if (assignedTo.HasValue)
            url += $"&assignedTo={assignedTo.Value}";

        if (!string.IsNullOrWhiteSpace(priority))
            url += $"&priority={Uri.EscapeDataString(priority)}";

        if (!string.IsNullOrWhiteSpace(category))
            url += $"&category={Uri.EscapeDataString(category)}";

        if (!string.IsNullOrWhiteSpace(search))
            url += $"&search={Uri.EscapeDataString(search.Trim())}";

        var response = await _httpClient.GetFromJsonAsync<ApiResponse<PagedResult<TicketDto>>>(url);
        return response?.Data ?? new PagedResult<TicketDto>();
    }

    public async Task<ApiResponse<object>> CreateTicketAsync(CreateTicketDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Tickets.Base, dto);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse<object>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new ApiResponse<object> { Success = false, Message = "Gagal membaca respon server." };
    }

    public async Task<ApiResponse<object>> UpdateTicketAsync(Guid id, UpdateTicketDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"{ApiRoutes.Tickets.Base}/{id}", dto);
        var body = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

        if (!response.IsSuccessStatusCode)
        {
            return body ?? new ApiResponse<object>
            {
                Success = false,
                Message = $"Request failed with status {(int)response.StatusCode} ({response.StatusCode})."
            };
        }

        return body ?? new ApiResponse<object> { Success = false, Message = "Empty response from server." };
    }

    public async Task<ApiResponse<object>> DeleteTicketAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{ApiRoutes.Tickets.Base}/{id}");
        return await response.Content.ReadFromJsonAsync<ApiResponse<object>>() ?? new ApiResponse<object> { Success = false };
    }

    public async Task<ApiResponse<object>> AssignTicketAsync(Guid ticketId, Guid userId)
    {
        var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Tickets.Assign.Replace("{id}", ticketId.ToString()), userId);
        return await response.Content.ReadFromJsonAsync<ApiResponse<object>>() ?? new ApiResponse<object> { Success = false };
    }
}