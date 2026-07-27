using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Infrastructure.Persistence;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Infrastructure.Repositories
{
    public class TicketHistoryRepository : ITicketHistoryRepository
    {
        private readonly AppDbContext _context;

        public TicketHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TicketHistory>> GetFilteredHistoriesAsync(
    Guid? ticketId,
    string? action,
    Guid? changedBy,
    string? searchString,
    DateTime? startDate,
    DateTime? endDate,
    PagedRequest request)
        {
            var query = _context.TicketHistories
                .Include(th => th.Ticket)
                .Include(th => th.ChangedByUser)
                .AsNoTracking();

            if (ticketId.HasValue && ticketId.Value != Guid.Empty)
                query = query.Where(h => h.TicketId == ticketId.Value);

            if (!string.IsNullOrEmpty(action) && Enum.TryParse<TicketHistoryAction>(action, out var actionEnum))
                query = query.Where(h => h.Action == actionEnum);

            // Filter berdasarkan User ID
            if (changedBy.HasValue && changedBy.Value != Guid.Empty)
                query = query.Where(h => h.ChangedBy == changedBy.Value);

            // Filter berdasarkan Rentang Tanggal (DateRange)
            if (startDate.HasValue)
                query = query.Where(h => h.Timestamp >= startDate.Value.ToUniversalTime());

            if (endDate.HasValue)
                // Ambil sampai akhir hari tersebut (23:59:59)
                query = query.Where(h => h.Timestamp <= endDate.Value.Date.AddDays(1).AddTicks(-1).ToUniversalTime());

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var keyword = searchString.Trim().ToLower();
                query = query.Where(h =>
                    (h.Ticket != null && h.Ticket.TicketNumber.ToLower().Contains(keyword)) ||
                    (h.Ticket != null && h.Ticket.Title.ToLower().Contains(keyword)) ||
                    (h.ChangedByUser != null && h.ChangedByUser.Name.ToLower().Contains(keyword))
                );
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(h => h.Timestamp)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<TicketHistory>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task AddAsync(TicketHistory history)
        {
            await _context.TicketHistories.AddAsync(history);
        }
    }
}