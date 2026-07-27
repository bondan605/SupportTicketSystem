using SupportTicketSystem.Shared.DTOs.Reports.Requests;
using SupportTicketSystem.Shared.DTOs.Reports.Responses;

namespace SupportTicketSystem.Application.Services.Reports;

/// <summary>
/// Orchestrates all report queries and applies caching, so the controller only calls
/// one method to get the full report summary.
/// </summary>
public interface IReportService
{
    Task<ReportSummaryDto> GetReportSummaryAsync(ReportSummaryQueryDto filter);
}