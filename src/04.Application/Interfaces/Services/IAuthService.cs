using SupportTicketSystem.Shared.DTOs.Auth;

namespace SupportTicketSystem.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    }
}