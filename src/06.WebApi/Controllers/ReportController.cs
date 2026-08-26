using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Application.Services.Reports;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Reports.Requests;
using SupportTicketSystem.Shared.DTOs.Reports.Responses;

namespace SupportTicketSystem.Api.Controllers;

/// <summary>
/// Exposes ticket report/analytics endpoints for managers.
/// </summary>
[ApiController]
[Route(ApiRoutes.Report.Reports)]
[Authorize(Roles = "SuperAdmin,Manager")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    /// <summary>
    /// Returns the full report summary (all 11 components) for the given date range.
    /// If no range is provided, defaults to the last 30 days.
    /// </summary>
    /// <param name="startDate">Start of the report period (inclusive). Defaults to 30 days before today.</param>
    /// <param name="endDate">End of the report period (inclusive). Defaults to today.</param>
    [HttpGet("summary")]
    [ProducesResponseType(typeof(ApiResponse<ReportSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<ReportSummaryDto>>> GetSummary(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        var effectiveEndDate = (endDate ?? DateTime.Today).Date;
        var effectiveStartDate = (startDate ?? effectiveEndDate.AddDays(-30)).Date;

        if (effectiveStartDate > effectiveEndDate)
        {
            return BadRequest(ApiResponse<object>.FailureResponse("startDate cannot be later than endDate."));
        }

        var filter = new ReportSummaryQueryDto(effectiveStartDate, effectiveEndDate.AddDays(1).AddTicks(-1));

        var summary = await _reportService.GetReportSummaryAsync(filter);

        return Ok(ApiResponse<ReportSummaryDto>.SuccessResponse(summary));
    }
}