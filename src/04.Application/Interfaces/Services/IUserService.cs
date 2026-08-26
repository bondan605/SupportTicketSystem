using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs.Users;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Application.Services;

public interface IUserService
{
    //Task<IEnumerable<UserDto>> GetAllUserAsync();
    Task<PagedResult<UserResponseDto>> GetAllUsersDetailAsync(
        string currentUserRole,
        PagedRequest request,
        string? searchString,
        UserRole? role,
        bool? status,
        DateTime? startDate,
        DateTime? endDate);
    Task<IEnumerable<UserDto>> GetAllUserByRoleAsync(UserRole role);
    Task<IEnumerable<UserDto>> GetAllAgentsAsync();
    Task<UserResponseDto> CreateUserAsync(CreateUserRequest request);
    Task<UserResponseDto> UpdateUserAsync(Guid id, UpdateUserRequest request, string currentUserRole);
    Task<UserResponseDto?> GetUserDetailByIdAsync(Guid id, string currentUserRole);
    Task<Dictionary<Guid, string>> GetUserNameDictionaryAsync(IEnumerable<Guid> userIds);
}