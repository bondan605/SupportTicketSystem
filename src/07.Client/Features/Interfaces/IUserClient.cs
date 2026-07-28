using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Users;

namespace SupportTicketSystem.Client.Features.Interfaces;

public interface IUserClient
{
    /// <summary>
    /// Mengambil daftar semua agen pendukung untuk keperluan filter dan penugasan tiket.
    /// </summary>
    Task<ApiResponse<IEnumerable<UserDto>>> GetAllUserAsync();
    Task<ApiResponse<IEnumerable<UserDto>>> GetAllUserByRoleAsync(UserRole role);
}