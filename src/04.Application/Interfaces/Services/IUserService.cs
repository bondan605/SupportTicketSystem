using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs.Users;

namespace SupportTicketSystem.Application.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUserAsync();
    Task<IEnumerable<UserDto>> GetAllUserByRoleAsync(UserRole role);
    Task<IEnumerable<UserDto>> GetAllAgentsAsync();
}