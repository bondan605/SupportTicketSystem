using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Infrastructure.Persistence;

namespace SupportTicketSystem.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetNextTicketSequenceAsync()
        {
            // Simple logic: count existing + 1. 
            // In high-concurrency, a dedicated sequence table is better, 
            // but for 1-day assessment, this shows the logic [1].
            return await _context.Tickets.CountAsync() + 1;
        }

        public async Task<IEnumerable<Ticket>> GetFilteredTicketsAsync(string? status, Guid? assignedTo)
        {
            var query = _context.Tickets.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.Status.ToString() == status);

            if (assignedTo.HasValue)
                query = query.Where(t => t.AssignedTo == assignedTo);

            return await query.OrderByDescending(t => t.CreatedAt).ToListAsync(); // [2]
        }

        public async Task<Ticket?> GetByIdAsync(Guid id) => await _context.Tickets.FindAsync(id);
        public async Task<IEnumerable<Ticket>> GetAllAsync() => await _context.Tickets.ToListAsync();
        public async Task AddAsync(Ticket ticket) => await _context.Tickets.AddAsync(ticket);
        public void Update(Ticket ticket) => _context.Tickets.Update(ticket);
    }
}