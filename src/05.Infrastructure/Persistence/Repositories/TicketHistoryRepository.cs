using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Shared.DTOs.TicketHistories;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Infrastructure.Persistence.Repositories
{
    public class TicketHistoryRepository : ITicketHistoryRepository
    {
        private readonly AppDbContext _context;

        public TicketHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TicketHistory history)
        {
            await _context.TicketHistories.AddAsync(history);
        }

        public async Task<PagedResult<TicketHistoryDto>> GetAllAsync(PagedRequest request)
        {
            var query = _context.TicketHistories
                .Include(x => x.Ticket)
                .Include(x => x.ChangedByUser)
                .OrderByDescending(x => x.Timestamp);

            var total = await query.CountAsync();

            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new TicketHistoryDto
                {
                    Id = x.Id,
                    TicketNumber = x.Ticket!.TicketNumber,
                    CustomerName = x.Ticket.CustomerName,
                    CustomerEmail = x.Ticket.CustomerEmail,
                    PreviousStatus = x.PreviousStatus,
                    NewStatus = x.NewStatus,
                    ChangedBy = x.ChangedBy,
                    ChangedByName = x.ChangedByUser!.Name,
                    Timestamp = x.Timestamp
                })
                .ToListAsync();

            return new PagedResult<TicketHistoryDto>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = total
            };
        }
    }
}
