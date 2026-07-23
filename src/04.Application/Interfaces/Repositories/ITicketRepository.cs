using SupportTicketSystem.Domain.Entities;

namespace SupportTicketSystem.Application.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetByIdAsync(Guid id);
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task AddAsync(Ticket ticket);
        void Update(Ticket ticket);
        void Delete(Guid id);

        Task<int> GetNextTicketSequenceAsync();

        Task<IEnumerable<Ticket>> GetFilteredTicketsAsync(string? status, Guid? assignedTo);
    }
}