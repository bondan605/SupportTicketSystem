using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Users;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Client.Features.Interfaces;

public interface IUserClient
{
    /// <summary>
    /// Mengambil daftar semua agen pendukung untuk keperluan filter dan penugasan tiket.
    /// </summary>
    //Task<ApiResponse<IEnumerable<UserDto>>> GetAllUserAsync();
    Task<ApiResponse<PagedResult<UserResponseDto>>?> GetAllUsersDetailAsync(
            PagedRequest request,
            string? searchString = null,
            UserRole? role = null,
            bool? status = null,
            DateTime? startDate = null,
            DateTime? endDate = null);

    Task<ApiResponse<IEnumerable<UserDto>>> GetAllUserByRoleAsync(UserRole role);
    Task<ApiResponse<IEnumerable<UserDto>>> GetAllAgentsAsync();

    Task<ApiResponse<UserResponseDto>> CreateUserAsync(CreateUserRequest request);
    Task<ApiResponse<UserResponseDto>> UpdateUserAsync(Guid id, UpdateUserRequest request);
    Task<ApiResponse<UserResponseDto>> GetUserDetailByIdAsync(Guid id);
}