using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Infrastructure.Persistence;
using SupportTicketSystem.Shared.DTOs.Dashboard;

namespace SupportTicketSystem.Infrastructure.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly AppDbContext _context;
    public DashboardRepository(AppDbContext context) => _context = context;

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        var tickets = await _context.Tickets.AsNoTracking().ToListAsync();

        var todayUtc = DateTime.UtcNow.Date;
        var last7Days = Enumerable.Range(0, 7)
            .Select(offset => todayUtc.AddDays(-offset))
            .OrderBy(d => d)
            .ToList();

        var weeklyTrends = last7Days
            .Select(date => new TicketTrendDto
            {
                DayName = date.ToString("ddd"),
                Count = tickets.Count(t =>
                    t.CreatedAt >= date && t.CreatedAt < date.AddDays(1))
            })
            .ToList();

        return new DashboardSummaryDto
        {
            TotalTickets = tickets.Count,
            OpenTickets = tickets.Count(t => t.Status == TicketStatus.Open),
            InProgressTickets = tickets.Count(t => t.Status == TicketStatus.InProgress),
            ResolvedTickets = tickets.Count(t => t.Status == TicketStatus.Resolved),
            ClosedTickets = tickets.Count(t => t.Status == TicketStatus.Closed),
            UnassignedTickets = tickets.Count(t => t.AssignedTo == null),
            WeeklyTrends = weeklyTrends
        };
    }
}