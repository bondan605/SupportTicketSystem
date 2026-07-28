namespace SupportTicketSystem.Shared.DTOs.Reports.Responses;

/// <summary>
/// Component 2: One entry per TicketStatus value, used for the status donut chart.
/// </summary>
public class TicketsPerStatusDto
{
    public string Status { get; set; } = string.Empty; // e.g. "Open", "InProgress"
    public int Count { get; set; }
    public double Percentage { get; set; }
}