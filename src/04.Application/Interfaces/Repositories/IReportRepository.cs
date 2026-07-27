using SupportTicketSystem.Shared.DTOs.Reports.Requests;
using SupportTicketSystem.Shared.DTOs.Reports.Responses;

namespace SupportTicketSystem.Application.Interfaces.Repositories;

/// <summary>
/// Read-only repository for report/analytics queries. Each method runs one aggregate
/// query independently so callers (the service layer) can execute them concurrently
/// via Task.WhenAll.
/// </summary>
public interface IReportRepository
{
    Task<TicketOverviewDto> GetTicketOverviewAsync(ReportSummaryQueryDto filter);
    Task<List<TicketsPerStatusDto>> GetTicketsPerStatusAsync(ReportSummaryQueryDto filter);
    Task<List<TicketsTrendDto>> GetTicketsTrendAsync(ReportSummaryQueryDto filter);
    Task<List<TicketsPerAssigneeDto>> GetTicketsPerAssigneeAsync(ReportSummaryQueryDto filter, int topN = 6);
    Task<List<TicketsPerCategoryDto>> GetTicketsPerCategoryAsync(ReportSummaryQueryDto filter);
    Task<List<TicketsPerPriorityDto>> GetTicketsPerPriorityAsync(ReportSummaryQueryDto filter);
    Task<AverageResponseTimeDto> GetAverageResponseTimeAsync(ReportSummaryQueryDto filter);
    Task<SlaComplianceDto> GetSlaComplianceAsync(ReportSummaryQueryDto filter);
    Task<List<RecentClosedTicketDto>> GetRecentClosedTicketsAsync(ReportSummaryQueryDto filter, int count = 5);
    Task<List<SlaComplianceTrendPointDto>> GetSlaComplianceTrendAsync(ReportSummaryQueryDto filter);
}