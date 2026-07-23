using SupportTicketSystem.Domain.Entities;

namespace SupportTicketSystem.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsAsync(Guid id);
    }
}