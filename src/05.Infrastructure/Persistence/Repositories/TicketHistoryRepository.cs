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

        // --- FUNGSI HELPER UNTUK FILTERING (Dipakai oleh Web & Export) ---
        private IQueryable<TicketHistory> ApplyFilters(
            IQueryable<TicketHistory> query,
            Guid? ticketId,
            string? action,
            Guid? changedBy,
            string? searchString,
            DateTime? startDate,
            DateTime? endDate,
            Guid? scopedToUserId)
        {
            // 1. Scoped User
            if (scopedToUserId.HasValue)
            {
                query = query.Where(h => h.Ticket != null
                    && (h.Ticket.CreatedBy == scopedToUserId.Value || h.Ticket.AssignedTo == scopedToUserId.Value));
            }

            // 2. Ticket ID
            if (ticketId.HasValue && ticketId.Value != Guid.Empty)
            {
                query = query.Where(h => h.TicketId == ticketId.Value);
            }

            // 3. Action
            if (!string.IsNullOrEmpty(action) && Enum.TryParse<TicketHistoryAction>(action, true, out var actionEnum))
            {
                query = query.Where(h => h.Action == actionEnum);
            }

            // 4. Changed By
            if (changedBy.HasValue && changedBy.Value != Guid.Empty)
            {
                query = query.Where(h => h.ChangedBy == changedBy.Value);
            }

            // 5. Date Range (Menggunakan standar presisi UTC dari metode web)
            if (startDate.HasValue)
            {
                query = query.Where(h => h.Timestamp >= startDate.Value.ToUniversalTime());
            }

            if (endDate.HasValue)
            {
                query = query.Where(h => h.Timestamp <= endDate.Value.Date.AddDays(1).AddTicks(-1).ToUniversalTime());
            }

            // 6. Search String (Gabungan komprehensif: TicketNumber, Title, Note, dan User Name)
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var keyword = searchString.Trim().ToLower();
                query = query.Where(h =>
                    (h.Ticket != null && h.Ticket.TicketNumber.ToLower().Contains(keyword)) ||
                    (h.Ticket != null && h.Ticket.Title.ToLower().Contains(keyword)) ||
                    (h.Note != null && h.Note.ToLower().Contains(keyword)) ||
                    (h.ChangedByUser != null && h.ChangedByUser.Name.ToLower().Contains(keyword))
                );
            }

            return query;
        }

        public async Task<PagedResult<TicketHistory>> GetFilteredHistoriesAsync(
            Guid? ticketId,
            string? action,
            Guid? changedBy,
            string? searchString,
            DateTime? startDate,
            DateTime? endDate,
            PagedRequest request,
            Guid? scopedToUserId = null)
        {
            var query = _context.TicketHistories
                .Include(th => th.Ticket)
                .Include(th => th.ChangedByUser)
                .AsNoTracking();

            // Panggil Helper Filter
            query = ApplyFilters(query, ticketId, action, changedBy, searchString, startDate, endDate, scopedToUserId);

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

        public async Task<IEnumerable<TicketHistory>> GetAllForExportAsync(
            Guid? ticketId,
            string? action,
            Guid? changedBy,
            string? searchString,
            DateTime? startDate,
            DateTime? endDate,
            Guid? scopedToUserId = null)
        {
            var query = _context.TicketHistories
                .Include(th => th.Ticket)
                .Include(th => th.ChangedByUser)
                .AsNoTracking();

            // Panggil Helper Filter yang SAMA PERSIS
            query = ApplyFilters(query, ticketId, action, changedBy, searchString, startDate, endDate, scopedToUserId);

            return await query.OrderByDescending(th => th.Timestamp).ToListAsync();
        }
    }
}