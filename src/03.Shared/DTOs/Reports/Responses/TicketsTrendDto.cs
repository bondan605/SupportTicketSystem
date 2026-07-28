namespace SupportTicketSystem.Shared.DTOs.Reports.Responses;

/// <summary>
/// Component 3: One entry per day within the filter range, showing tickets created
/// (by CreatedAt) vs. tickets closed (by ClosedAt) that day.
/// </summary>
public class TicketsTrendDto
{
    public DateTime Date { get; set; }
    public int CreatedCount { get; set; }
    public int ClosedCount { get; set; }
}