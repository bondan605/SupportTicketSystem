using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Infrastructure.Persistence;
using SupportTicketSystem.Shared.Models;
using System.Net.NetworkInformation;
using static SupportTicketSystem.Shared.Constants.ApiRoutes;

namespace SupportTicketSystem.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Ticket>> GetFilteredTicketsAsync(string? status, Guid? assignedTo, PagedRequest request)
        {
            var query = _context.Tickets.AsNoTracking();

            // Filtering Logic
            if (!string.IsNullOrEmpty(status))
            {
                if (Enum.TryParse<TicketStatus>(status, out var statusEnum))
                {
                    query = query.Where(t => t.Status == statusEnum);
                }
            }

            if (assignedTo.HasValue)
            {
                query = query.Where(t => t.AssignedTo == assignedTo);
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<Ticket>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Ticket?> GetByIdAsync(Guid id) => await _context.Tickets.FindAsync(id);

        public async Task<PagedResult<Ticket>> GetAllAsync(PagedRequest request) 
        {
            var query = _context.Tickets.AsNoTracking();

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<Ticket>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<PagedResult<Ticket>> GetTicketsForUserAsync(Guid userId, PagedRequest request)
        {
            var query = _context.Tickets
                .Where(t => t.CreatedBy == userId || t.AssignedTo == userId)
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<Ticket>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task AddAsync(Ticket ticket) => await _context.Tickets.AddAsync(ticket);

        public void Update(Ticket ticket) => _context.Tickets.Update(ticket);

        public void Delete(Ticket ticket) => _context.Tickets.Remove(ticket);

        public async Task<int> GetNextTicketSequenceAsync()
        {
            return await _context.Tickets.CountAsync() + 1;
        }
    }
}