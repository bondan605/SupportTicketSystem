namespace SupportTicketSystem.Shared.DTOs.Reports.Responses;

/// <summary>
/// Component 1: The top-row overview cards. Counts reflect the actual 4 TicketStatus values.
/// </summary>
public class TicketOverviewDto
{
    public int TotalTickets { get; set; }
    public int OpenCount { get; set; }
    public int InProgressCount { get; set; }
    public int ResolvedCount { get; set; }
    public int ClosedCount { get; set; }
    public int TotalUsers { get; set; }

    /// <summary>
    /// Percentage change vs. the equivalent previous period (e.g. previous 30 days).
    /// Null if no previous-period data is available for comparison.
    /// </summary>
    public double? TotalTicketsChangePercent { get; set; }
}