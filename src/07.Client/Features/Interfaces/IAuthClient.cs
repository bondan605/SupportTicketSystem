using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Auth;

namespace SupportTicketSystem.Client.Features.Interfaces;

public interface IAuthClient
{
    Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);
}