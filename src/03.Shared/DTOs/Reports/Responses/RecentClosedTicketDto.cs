namespace SupportTicketSystem.Shared.DTOs.Reports.Responses;

/// <summary>
/// Component 10: A single row in the "Recent Closed Tickets" list.
/// </summary>
public class RecentClosedTicketDto
{
    public string TicketNumber { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime? ClosedAt { get; set; }

    /// <summary>The assignee of the ticket at the time it was closed.</summary>
    public string ClosedBy { get; set; } = string.Empty;
}