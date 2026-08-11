using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Infrastructure.Persistence;
using SupportTicketSystem.Shared.DTOs.Reports.Requests;
using SupportTicketSystem.Shared.DTOs.Reports.Responses;

namespace SupportTicketSystem.Infrastructure.Repositories;

/// <inheritdoc cref="IReportRepository"/>
public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>Component 1. Ticket Overview.</summary>
    public async Task<TicketOverviewDto> GetTicketOverviewAsync(ReportSummaryQueryDto filter)
    {
        var ticketsInRange = _context.Tickets
            .Where(t => t.CreatedAt >= filter.StartDate && t.CreatedAt <= filter.EndDate);

        var statusCounts = await ticketsInRange
            .GroupBy(t => t.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync();

        var totalUsers = await _context.Users.CountAsync(u => u.IsActive);

        return new TicketOverviewDto
        {
            TotalTickets = statusCounts.Sum(s => s.Count),
            OpenCount = statusCounts.FirstOrDefault(s => s.Status == TicketStatus.Open)?.Count ?? 0,
            InProgressCount = statusCounts.FirstOrDefault(s => s.Status == TicketStatus.InProgress)?.Count ?? 0,
            ResolvedCount = statusCounts.FirstOrDefault(s => s.Status == TicketStatus.Resolved)?.Count ?? 0,
            ClosedCount = statusCounts.FirstOrDefault(s => s.Status == TicketStatus.Closed)?.Count ?? 0,
            TotalUsers = totalUsers,
            TotalTicketsChangePercent = null // need previous periode and current
        };
    }

    /// <summary>Component 2.Tickets Per Status.</summary>
    public async Task<List<TicketsPerStatusDto>> GetTicketsPerStatusAsync(ReportSummaryQueryDto filter)
    {
        var counts = await _context.Tickets
            .Where(t => t.CreatedAt >= filter.StartDate && t.CreatedAt <= filter.EndDate)
            .GroupBy(t => t.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync();

        var total = counts.Sum(c => c.Count);

        return counts.Select(c => new TicketsPerStatusDto
        {
            Status = c.Status.ToString(),
            Count = c.Count,
            Percentage = total == 0 ? 0 : Math.Round(c.Count * 100.0 / total, 1)
        }).ToList();
    }

    /// <summary>Component 3. Tickets Trend</summary>
    public async Task<List<TicketsTrendDto>> GetTicketsTrendAsync(ReportSummaryQueryDto filter)
    {
        var createdPerDay = await _context.Tickets
            .Where(t => t.CreatedAt >= filter.StartDate && t.CreatedAt <= filter.EndDate)
            .GroupBy(t => t.CreatedAt.Date)
            .Select(g => new { Date = g.Key, Count = g.Count() })
            .ToListAsync();

        var closedPerDay = await _context.Tickets
            .Where(t => t.ClosedAt != null && t.ClosedAt >= filter.StartDate && t.ClosedAt <= filter.EndDate)
            .GroupBy(t => t.ClosedAt!.Value.Date)
            .Select(g => new { Date = g.Key, Count = g.Count() })
            .ToListAsync();

        var allDates = Enumerable.Range(0, (filter.EndDate.Date - filter.StartDate.Date).Days + 1)
            .Select(offset => filter.StartDate.Date.AddDays(offset));

        return allDates.Select(date => new TicketsTrendDto
        {
            Date = date,
            CreatedCount = createdPerDay.FirstOrDefault(c => c.Date == date)?.Count ?? 0,
            ClosedCount = closedPerDay.FirstOrDefault(c => c.Date == date)?.Count ?? 0
        }).ToList();
    }

    /// <summary>Component 4. Tickets Per Assignee.</summary>
    public async Task<List<TicketsPerAssigneeDto>> GetTicketsPerAssigneeAsync(ReportSummaryQueryDto filter, int topN = 6)
    {
        var ticketsInRange = _context.Tickets
            .Where(t => t.CreatedAt >= filter.StartDate && t.CreatedAt <= filter.EndDate);

        var assigned = await ticketsInRange
            .Where(t => t.AssignedTo != null)
            .GroupBy(t => new { t.AssignedTo, t.Assignee!.Name })
            .Select(g => new TicketsPerAssigneeDto
            {
                AssigneeId = g.Key.AssignedTo,
                AssigneeName = g.Key.Name,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(topN)
            .ToListAsync();

        var unassignedCount = await ticketsInRange.CountAsync(t => t.AssignedTo == null);

        if (unassignedCount > 0)
        {
            assigned.Add(new TicketsPerAssigneeDto
            {
                AssigneeId = null,
                AssigneeName = "Unassigned",
                Count = unassignedCount
            });
        }

        return assigned;
    }

    /// <summary>Components 5 &amp; 9. Tickets Per Category.</summary>
    public async Task<List<TicketsPerCategoryDto>> GetTicketsPerCategoryAsync(ReportSummaryQueryDto filter)
    {
        var counts = await _context.Tickets
            .Where(t => t.CreatedAt >= filter.StartDate && t.CreatedAt <= filter.EndDate)
            .GroupBy(t => t.Category)
            .Select(g => new { Category = g.Key, Count = g.Count() })
            .ToListAsync();

        var total = counts.Sum(c => c.Count);

        return counts.Select(c => new TicketsPerCategoryDto
        {
            Category = c.Category.ToString(),
            Count = c.Count,
            Percentage = total == 0 ? 0 : Math.Round(c.Count * 100.0 / total, 1)
        }).ToList();
    }

    /// <summary>Component 6. Tickets Per Priority.</summary>
    public async Task<List<TicketsPerPriorityDto>> GetTicketsPerPriorityAsync(ReportSummaryQueryDto filter)
    {
        var counts = await _context.Tickets
            .Where(t => t.CreatedAt >= filter.StartDate && t.CreatedAt <= filter.EndDate)
            .GroupBy(t => t.Priority)
            .Select(g => new { Priority = g.Key, Count = g.Count() })
            .ToListAsync();

        var total = counts.Sum(c => c.Count);

        return counts.Select(c => new TicketsPerPriorityDto
        {
            Priority = c.Priority.ToString(),
            Count = c.Count,
            Percentage = total == 0 ? 0 : Math.Round(c.Count * 100.0 / total, 1)
        }).ToList();
    }

    /// <summary>Component 7. Average Response Time.</summary>
    public async Task<AverageResponseTimeDto> GetAverageResponseTimeAsync(ReportSummaryQueryDto filter)
    {
        var firstInProgressByTicket = _context.TicketHistories
            .Where(h => h.Action == TicketHistoryAction.StatusChanged
                        && h.NewValue == nameof(TicketStatus.InProgress))
            .GroupBy(h => h.TicketId)
            .Select(g => new { TicketId = g.Key, FirstInProgressAt = g.Min(h => h.Timestamp) });

        var responseMinutes = await _context.Tickets
            .Where(t => t.CreatedAt >= filter.StartDate && t.CreatedAt <= filter.EndDate)
            .Join(firstInProgressByTicket, t => t.Id, h => h.TicketId, (t, h) => new { t.CreatedAt, h.FirstInProgressAt })
            .Where(i => i.FirstInProgressAt >= i.CreatedAt)
            .Select(x => EF.Functions.DateDiffMinute(x.CreatedAt, x.FirstInProgressAt))
            .ToListAsync();

        return new AverageResponseTimeDto
        {
            AverageResponseMinutes = responseMinutes.Count == 0 ? 0 : responseMinutes.Average(),
            ChangePercent = null
        };
    }

    /// <summary>Component 8. SlaCompliance.</summary>
    public async Task<SlaComplianceDto> GetSlaComplianceAsync(ReportSummaryQueryDto filter)
    {
        var evaluable = _context.Tickets
            .Where(t => t.Status == TicketStatus.Closed
                        && t.ClosedAt != null
                        && t.ClosedAt >= filter.StartDate && t.ClosedAt <= filter.EndDate
                        && t.EstimatedDueDate != null);

        var total = await evaluable.CountAsync();
        var compliant = await evaluable.CountAsync(t => t.ClosedAt <= t.EstimatedDueDate);

        return new SlaComplianceDto
        {
            CompliancePercentage = total == 0 ? 0 : Math.Round(compliant * 100.0 / total, 1),
            EvaluatedTicketCount = total,
            ChangePercent = null // pending decision, see reminder note
        };
    }

    /// <summary>Component 10. Recent Closed Tickets.</summary>
    public async Task<List<RecentClosedTicketDto>> GetRecentClosedTicketsAsync(ReportSummaryQueryDto filter, int count = 5)
    {
        return await _context.Tickets
            .Where(t => t.Status == TicketStatus.Closed
                        && t.ClosedAt != null
                        && t.ClosedAt >= filter.StartDate && t.ClosedAt <= filter.EndDate)
            .OrderByDescending(t => t.ClosedAt)
            .Take(count)
            .Select(t => new RecentClosedTicketDto
            {
                TicketNumber = t.TicketNumber,
                Title = t.Title,
                ClosedAt = t.ClosedAt,
                ClosedBy = t.Assignee != null ? t.Assignee.Name : "Unassigned"
            })
            .ToListAsync();
    }

    /// <summary>Component 11. Sla Compliance Trend.</summary>
    public async Task<List<SlaComplianceTrendPointDto>> GetSlaComplianceTrendAsync(ReportSummaryQueryDto filter)
    {
        var evaluableTickets = await _context.Tickets
            .Where(t => t.Status == TicketStatus.Closed
                        && t.ClosedAt != null
                        && t.ClosedAt >= filter.StartDate && t.ClosedAt <= filter.EndDate
                        && t.EstimatedDueDate != null)
            .Select(t => new { ClosedDate = t.ClosedAt!.Value.Date, IsCompliant = t.ClosedAt <= t.EstimatedDueDate })
            .ToListAsync();

        var allDates = Enumerable.Range(0, (filter.EndDate.Date - filter.StartDate.Date).Days + 1)
            .Select(offset => filter.StartDate.Date.AddDays(offset));

        var result = new List<SlaComplianceTrendPointDto>();
        var cumulativeTotal = 0;
        var cumulativeCompliant = 0;

        foreach (var date in allDates)
        {
            var dayTickets = evaluableTickets.Where(t => t.ClosedDate == date).ToList();
            var dayTotal = dayTickets.Count;
            var dayCompliant = dayTickets.Count(t => t.IsCompliant);

            cumulativeTotal += dayTotal;
            cumulativeCompliant += dayCompliant;

            result.Add(new SlaComplianceTrendPointDto
            {
                Date = date,
                DailyCompliancePercentage = dayTotal == 0 ? 0 : Math.Round(dayCompliant * 100.0 / dayTotal, 1),
                CumulativeCompliancePercentage = cumulativeTotal == 0 ? 0 : Math.Round(cumulativeCompliant * 100.0 / cumulativeTotal, 1)
            });
        }

        return result;
    }
}