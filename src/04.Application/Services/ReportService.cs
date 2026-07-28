using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Shared.DTOs.Reports.Requests;
using SupportTicketSystem.Shared.DTOs.Reports.Responses;

namespace SupportTicketSystem.Application.Services.Reports;

/// <inheritdoc cref="IReportService"/>
public class ReportService : IReportService
{
    private readonly IValidator<ReportSummaryQueryDto> _queryValidator;
    private readonly IReportRepository _repository;
    private readonly IMemoryCache _cache;

    /// <summary>How long a report summary stays cached before being recomputed.</summary>
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

    public ReportService(IValidator<ReportSummaryQueryDto> queryValidator, IReportRepository repository, IMemoryCache cache)
    {
        _queryValidator = queryValidator;
        _repository = repository;
        _cache = cache;
    }

    public async Task<ReportSummaryDto> GetReportSummaryAsync(ReportSummaryQueryDto filter)
    {
        await _queryValidator.ValidateAndThrowAsync(filter);

        var cacheKey = BuildCacheKey(filter);

        if (_cache.TryGetValue(cacheKey, out ReportSummaryDto? cachedSummary) && cachedSummary != null)
        {
            return cachedSummary;
        }

        // NOTE: awaited sequentially, not with Task.WhenAll — DbContext (scoped) is not
        // thread-safe and cannot run multiple concurrent operations on the same instance.
        var summary = new ReportSummaryDto
        {
            Overview = await _repository.GetTicketOverviewAsync(filter),
            TicketsPerStatus = await _repository.GetTicketsPerStatusAsync(filter),
            TicketsTrend = await _repository.GetTicketsTrendAsync(filter),
            TicketsPerAssignee = await _repository.GetTicketsPerAssigneeAsync(filter),
            TicketsPerCategory = await _repository.GetTicketsPerCategoryAsync(filter),
            TicketsPerPriority = await _repository.GetTicketsPerPriorityAsync(filter),
            AverageResponseTime = await _repository.GetAverageResponseTimeAsync(filter),
            SlaCompliance = await _repository.GetSlaComplianceAsync(filter),
            RecentClosedTickets = await _repository.GetRecentClosedTicketsAsync(filter),
            SlaComplianceTrend = await _repository.GetSlaComplianceTrendAsync(filter)
        };

        _cache.Set(cacheKey, summary, CacheDuration);

        return summary;
    }

    /// <summary>
    /// Builds a cache key scoped to the requested date range, so different filter
    /// selections don't collide or serve stale data from a different period.
    /// </summary>
    private static string BuildCacheKey(ReportSummaryQueryDto filter)
        => $"report-summary:{filter.StartDate:yyyyMMdd}:{filter.EndDate:yyyyMMdd}";
}