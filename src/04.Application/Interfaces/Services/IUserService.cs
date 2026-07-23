using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Users;

namespace SupportTicketSystem.Application.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAgentsAsync();
}