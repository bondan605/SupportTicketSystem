using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.TicketHistories;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace SupportTicketSystem.Client.Features;

public class TicketClient : ITicketClient
{
    private readonly HttpClient _httpClient;
    private readonly ITokenProvider _tokenProvider;

    public TicketClient(HttpClient httpClient, ITokenProvider tokenProvider)
    {
        _httpClient = httpClient;
        _tokenProvider = tokenProvider;
    }

    private async Task AttachTokenAsync()
    {
        var token = await _tokenProvider.GetTokenAsync();
        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<ApiResponse<TicketDto>> GetTicketByIdAsync(Guid id)
    {
        await AttachTokenAsync();
        return await _httpClient.GetFromJsonAsync<ApiResponse<TicketDto>>($"{ApiRoutes.Tickets.Base}/{id}") ?? new ApiResponse<TicketDto> { Success = false };
    }

    public async Task<PagedResult<TicketDto>> GetAllTicketsAsync(PagedRequest request)
    {
        await AttachTokenAsync();
        var url = $"{ApiRoutes.Tickets.Base}?pageNumber={request.PageNumber}&pageSize={request.PageSize}";
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<PagedResult<TicketDto>>>(url);
        return response?.Data ?? new PagedResult<TicketDto>();
    }

    public async Task<PagedResult<TicketDto>> GetFilteredTicketsAsync(string? status, Guid? assignedTo, PagedRequest request)
    {
        await AttachTokenAsync();
        var url = $"{ApiRoutes.Tickets.Report}?pageNumber={request.PageNumber}&pageSize={request.PageSize}";

        if (!string.IsNullOrEmpty(status))
            url += $"&status={Uri.EscapeDataString(status)}";

        if (assignedTo.HasValue)
            url += $"&assignedTo={assignedTo.Value}";

        var response = await _httpClient.GetFromJsonAsync<ApiResponse<PagedResult<TicketDto>>>(url);
        return response?.Data ?? new PagedResult<TicketDto>();
    }

    public async Task<ApiResponse<object>> CreateTicketAsync(CreateTicketDto dto)
    {
        await AttachTokenAsync();
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Tickets.Base, dto);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse<object>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new ApiResponse<object> { Success = false, Message = "Gagal membaca respon server." };
    }

    public async Task<ApiResponse<object>> UpdateTicketAsync(Guid id, UpdateTicketDto dto)
    {
        await AttachTokenAsync();
        var response = await _httpClient.PutAsJsonAsync($"{ApiRoutes.Tickets.Base}/{id}", dto);
        return await response.Content.ReadFromJsonAsync<ApiResponse<object>>() ?? new ApiResponse<object> { Success = false };
    }

    public async Task<ApiResponse<object>> DeleteTicketAsync(Guid id)
    {
        await AttachTokenAsync();
        var response = await _httpClient.DeleteAsync($"{ApiRoutes.Tickets.Base}/{id}");
        return await response.Content.ReadFromJsonAsync<ApiResponse<object>>() ?? new ApiResponse<object> { Success = false };
    }

    public async Task<ApiResponse<object>> AssignTicketAsync(Guid ticketId, Guid userId)
    {
        await AttachTokenAsync();
        var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Tickets.Assign.Replace("{id}", ticketId.ToString()), userId);
        return await response.Content.ReadFromJsonAsync<ApiResponse<object>>() ?? new ApiResponse<object> { Success = false };
    }

    public async Task<PagedResult<TicketHistoryDto>> GetTicketHistoriesAsync(PagedRequest request)
    {
        var url = $"{ApiRoutes.Tickets.History}?pageNumber={request.PageNumber}&pageSize={request.PageSize}";

        var response = await _httpClient.GetFromJsonAsync<ApiResponse<PagedResult<TicketHistoryDto>>>(url);

        return response?.Data ?? new PagedResult<TicketHistoryDto>();
    }
}