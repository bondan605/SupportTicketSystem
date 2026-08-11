using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        //Task<IEnumerable<User>> GetAllAsync();
        Task<(IEnumerable<User> Items, int TotalCount)> GetPagedUsersAsync(
            int pageNumber,
            int pageSize,
            string currentUserRole,
            string? searchString,
            UserRole? role,
            bool? status,
            DateTime? startDate,
            DateTime? endDate);
        Task<IEnumerable<User>> GetAllByRoleAsync(UserRole role);
        Task<IEnumerable<User>> GetAllAgentsAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByUsernameAsync(string username);
        Task AddAsync(User user);
        void Update(User user);
        Task<Dictionary<Guid, string>> GetUserNameDictionaryAsync(IEnumerable<Guid> userIds);
    }
}