using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Interfaces;
using SupportTicketSystem.Infrastructure.Persistence;

namespace TicketManagement.Infrastructure.Repositories;

public class ReportRepository : IReportRepository 
{
    private readonly AppDbContext _context;
    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }
    public IQueryable<Ticket> GetFilterableQuery() =>
        _context.Tickets
            .Include(t => t.Assignee)
            .AsNoTracking(); 
}