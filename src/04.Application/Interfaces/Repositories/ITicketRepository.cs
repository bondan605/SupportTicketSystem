using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Application.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetByIdAsync(Guid id);
        Task<PagedResult<Ticket>> GetAllAsync(PagedRequest request);
        Task<PagedResult<Ticket>> GetTicketsForUserAsync(Guid userId, PagedRequest request);
        Task AddAsync(Ticket ticket);
        void Update(Ticket ticket);
        void Delete(Ticket ticket);

        Task<int> GetNextTicketSequenceAsync();

        Task<PagedResult<Ticket>> GetFilteredTicketsAsync(string? status, Guid? assignedTo, PagedRequest paging);
    }
}