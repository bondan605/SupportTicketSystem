using SupportTicketSystem.Application.Interfaces;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Domain.Interfaces;
using SupportTicketSystem.Shared.Dtos.Reports;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Application.Services;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly ITicketRepository _ticketRepository;

    public ReportService(IReportRepository reportRepository, ITicketRepository ticketRepository)
    {
        _reportRepository = reportRepository;
        _ticketRepository = ticketRepository;
    }
    public async Task<PagedResult<ManagerReportItemDto>> GetManagerReportAsync(ManagerReportFilterDto filter)
    {
        var query = _reportRepository.GetFilterableQuery();

        //if (filter.StartDate.HasValue)
        //    query = query.Where(t => t.CreatedDate >= filter.StartDate.Value);

        //if (filter.EndDate.HasValue)
        //    query = query.Where(t => t.CreatedDate <= filter.EndDate.Value);

        //if (!string.IsNullOrWhiteSpace(filter.Status)
        //    && Enum.TryParse<TicketStatus>(filter.Status.Replace(" ", ""), out var status))
        //{
        //    query = query.Where(t => t.Status == status);
        //}

        //if (filter.AssignedToUserId.HasValue)
        //    query = query.Where(t => t.AssignedTo == filter.AssignedToUserId.Value);

        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var term = filter.SearchTerm.Trim();
            query = query.Where(t =>
                t.TicketNumber.Contains(term) ||
                t.CustomerName.Contains(term) ||
                t.Title.Contains(term));
        }

        var totalCount = query.Count();

        var items = query
            .OrderByDescending(t => t.UpdatedAt)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            //.Take(filter.PageSize)
            .Select(t => new ManagerReportItemDto
            {
                TicketId = t.Id,
                TicketNumber = t.TicketNumber,
                CustomerName = t.CustomerName,
                CustomerEmail = t.CustomerEmail,
                Title = t.Title,
                Status = t.Status.ToString(),
                AssignedToUserId = t.AssignedTo,
                AssignedToAgentName = t.Assignee != null ? t.Assignee.Name : null,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToList();

        return new PagedResult<ManagerReportItemDto>
        {
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize,
            Items = items
        };
    }

    public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
    {
        var pagedResult = await _ticketRepository.GetAllAsync(new PagedRequest
        {
            PageNumber = 1,
            PageSize = int.MaxValue
        });

        var tickets = pagedResult.Items.ToList();

        var workload = tickets
            .Where(t => t.AssignedTo.HasValue && t.Assignee != null)
            .GroupBy(t => new { t.AssignedTo, t.Assignee!.Name })
            .Select(g => new AgentWorkloadDto
            {
                UserId = g.Key.AssignedTo!.Value,
                AgentName = g.Key.Name,
                AssignedTicketCount = g.Count()
            })
            .ToList();

        return new DashboardSummaryDto
        {
            TotalTickets = tickets.Count,
            OpenCount = tickets.Count(t => t.Status == TicketStatus.Open),
            InProgressCount = tickets.Count(t => t.Status == TicketStatus.InProgress),
            ResolvedCount = tickets.Count(t => t.Status == TicketStatus.Resolved),
            ClosedCount = tickets.Count(t => t.Status == TicketStatus.Closed),
            WorkloadPerAgent = workload
        };
    }
}