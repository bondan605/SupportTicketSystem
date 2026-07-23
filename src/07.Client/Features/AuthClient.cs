using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Auth;
using System.Net.Http.Json;

namespace SupportTicketSystem.Client.Features;

public class AuthClient : IAuthClient
{
    private readonly HttpClient _httpClient;

    public AuthClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Auth.Login, request);
        return await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponseDto>>() ?? new ApiResponse<LoginResponseDto> { Success = false, Message = "Communication Error" };
    }
}