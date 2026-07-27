// DTOs/Reports/SlaComplianceTrendPointDto.cs
namespace SupportTicketSystem.Shared.DTOs.Reports.Responses;

/// <summary>
/// Component 11: One entry per day, showing the cumulative or daily SLA compliance
/// percentage over the filter range.
/// </summary>
public class SlaComplianceTrendPointDto
{
    public DateTime Date { get; set; }
    public double DailyCompliancePercentage { get; set; }
    public double CumulativeCompliancePercentage { get; set; }
}