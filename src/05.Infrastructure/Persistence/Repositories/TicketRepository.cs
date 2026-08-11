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

        public async Task<PagedResult<Ticket>> GetTicketListAsync(string? status, Guid? assignedTo, PagedRequest request, string? priority, string? category, string? search, Guid? scopedToUserId = null)
        {
            var pageNumber = Math.Max(1, request.PageNumber);
            var pageSize = Math.Clamp(request.PageSize, 1, 100);
            var skip = (pageNumber - 1) * pageSize;

            var query = _context.Tickets
                .AsNoTracking()
                .AsQueryable();

            // Non-Manager callers only see tickets they created or are assigned to.
            if (scopedToUserId.HasValue)
            {
                query = query.Where(ticket => ticket.CreatedBy == scopedToUserId.Value || ticket.AssignedTo == scopedToUserId.Value);
            }

            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<TicketStatus>(status, ignoreCase: true, out var statusEnum))
            {
                query = query.Where(ticket => ticket.Status == statusEnum);
            }

            if (assignedTo.HasValue)
            {
                query = query.Where(ticket => ticket.AssignedTo == assignedTo.Value);
            }

            if (!string.IsNullOrWhiteSpace(priority) && Enum.TryParse<TicketPriority>(priority, ignoreCase: true, out var priorityEnum))
            {
                query = query.Where(ticket => ticket.Priority == priorityEnum);
            }

            if (!string.IsNullOrWhiteSpace(category) && Enum.TryParse<TicketCategory>(category, ignoreCase: true, out var categoryEnum))
            {
                query = query.Where(ticket => ticket.Category == categoryEnum);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var keyword = search.Trim();

                query = query.Where(ticket =>
                    ticket.TicketNumber.Contains(keyword) ||
                    ticket.Title.Contains(keyword) ||
                    ticket.CustomerName.Contains(keyword) ||
                    ticket.CustomerEmail.Contains(keyword) ||
                    ticket.Description.Contains(keyword));
            }

            var totalCount = await query.CountAsync();

            IOrderedQueryable<Ticket> orderedQuery;

            if (!string.IsNullOrWhiteSpace(search))
            {
                var keyword = search.Trim();

                orderedQuery = query
                    .OrderByDescending(ticket =>
                        ticket.Title.StartsWith(keyword) ||
                        (ticket.CustomerName != null &&
                         ticket.CustomerName.StartsWith(keyword)))
                    .ThenByDescending(ticket =>
                        ticket.Title.Contains(keyword) ||
                        (ticket.CustomerName != null &&
                         ticket.CustomerName.Contains(keyword)))
                    .ThenByDescending(ticket =>
                        ticket.UpdatedAt ?? ticket.CreatedAt);
            }
            else
            {
                orderedQuery = query
                    .OrderByDescending(ticket =>
                        ticket.UpdatedAt ?? ticket.CreatedAt);
            }

            var items = await orderedQuery
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Ticket>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
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
            var lastTicketNumber = await _context.Tickets
                .OrderByDescending(t => t.TicketNumber)
                .Select(t => t.TicketNumber)
                .FirstOrDefaultAsync();
                
            if (string.IsNullOrEmpty(lastTicketNumber))
            {
                return 1;
            }

            var numericPart = lastTicketNumber.Split('-').LastOrDefault();
            return int.TryParse(numericPart, out var parsed) ? parsed + 1 : 1;
        }
    }
}