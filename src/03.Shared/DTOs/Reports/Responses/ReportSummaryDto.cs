using SupportTicketSystem.Shared.DTOs.Dashboard;

namespace SupportTicketSystem.Shared.DTOs.Reports.Responses;

/// <summary>
/// The root DTO returned by GET /api/reports/summary, aggregating all report components.
/// </summary>
public class ReportSummaryDto
{
    public TicketOverviewDto Overview { get; set; } = new();
    public List<TicketsPerStatusDto> TicketsPerStatus { get; set; } = new();
    public List<TicketsTrendDto> TicketsTrend { get; set; } = new();
    public List<TicketsPerAssigneeDto> TicketsPerAssignee { get; set; } = new();
    public List<TicketsPerCategoryDto> TicketsPerCategory { get; set; } = new();
    public List<TicketsPerPriorityDto> TicketsPerPriority { get; set; } = new();
    public AverageResponseTimeDto AverageResponseTime { get; set; } = new();
    public SlaComplianceDto SlaCompliance { get; set; } = new();
    public List<RecentClosedTicketDto> RecentClosedTickets { get; set; } = new();
    public List<SlaComplianceTrendPointDto> SlaComplianceTrend { get; set; } = new();
}